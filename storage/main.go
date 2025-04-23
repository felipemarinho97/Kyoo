package main

import (
	"context"
	"fmt"
	"io"
	"log"
	"net/http"
	"net/url"
	"os"
	"strconv"
	"strings"
	"time"

	"github.com/gin-gonic/gin"
	"github.com/go-redis/redis/v8"
	"github.com/minio/minio-go/v7"
	"github.com/minio/minio-go/v7/pkg/credentials"
)

// Config holds configuration parameters read from env variables.
type Config struct {
	S3Endpoint    string
	S3AccessKey   string
	S3SecretKey   string
	S3UseSSL      bool
	S3Bucket      string
	RedisAddr     string
	RedisPassword string
	RedisDB       int
	// PresignExpiry is the expiration duration for the pre-signed URL.
	PresignExpiry time.Duration
	// CacheTTL is how long we keep the existence flag in Redis.
	ExistenceCacheTTL time.Duration
	// URLCacheTTL is how long the pre-signed URL is cached.
	URLCacheTTL time.Duration
}

// Global clients.
var (
	minioClient *minio.Client
	redisClient *redis.Client
	cfg         Config
	ctx         = context.Background()
)

// getEnv retrieves an environment variable or returns a fallback.
func getEnv(key, fallback string) string {
	if val, ok := os.LookupEnv(key); ok {
		return val
	}
	return fallback
}

// initConfig reads configuration from environment variables.
func initConfig() Config {
	useSSL, err := strconv.ParseBool(getEnv("S3_USE_SSL", "false"))
	if err != nil {
		useSSL = false
	}

	redisDB, err := strconv.Atoi(getEnv("REDIS_DB", "0"))
	if err != nil {
		redisDB = 0
	}

	// Presigned URL expiry (in seconds)
	presignExpirySeconds, err := strconv.Atoi(getEnv("PRESIGN_EXPIRY_SECONDS", "300"))
	if err != nil {
		presignExpirySeconds = 300
	}

	// Cache TTL for existence flag (in seconds)
	existenceTTLSeconds, err := strconv.Atoi(getEnv("EXISTENCE_CACHE_TTL_SECONDS", "86400"))
	if err != nil {
		existenceTTLSeconds = 86400
	}

	// Cache TTL for pre-signed URL (in seconds)
	urlTTLSeconds, err := strconv.Atoi(getEnv("URL_CACHE_TTL_SECONDS", "240"))
	if err != nil {
		urlTTLSeconds = 240
	}

	return Config{
		S3Endpoint:        getEnv("S3_ENDPOINT", "localhost:9000"),
		S3AccessKey:       getEnv("S3_ACCESS_KEY", "minioadmin"),
		S3SecretKey:       getEnv("S3_SECRET_KEY", "minioadmin"),
		S3UseSSL:          useSSL,
		S3Bucket:          getEnv("S3_BUCKET", "storage"),
		RedisAddr:         getEnv("REDIS_ADDR", "localhost:6379"),
		RedisPassword:     getEnv("REDIS_PASSWORD", ""),
		RedisDB:           redisDB,
		PresignExpiry:     time.Duration(presignExpirySeconds) * time.Second,
		ExistenceCacheTTL: time.Duration(existenceTTLSeconds) * time.Second,
		URLCacheTTL:       time.Duration(urlTTLSeconds) * time.Second,
	}
}

// initMinio initializes the MinIO client and ensures the bucket exists.
func initMinio(cfg Config) (*minio.Client, error) {
	client, err := minio.New(cfg.S3Endpoint, &minio.Options{
		Creds:  credentials.NewStaticV4(cfg.S3AccessKey, cfg.S3SecretKey, ""),
		Secure: cfg.S3UseSSL,
	})
	if err != nil {
		return nil, err
	}

	// Check if the bucket exists; if not, create it.
	exists, errBucketExists := client.BucketExists(ctx, cfg.S3Bucket)
	if errBucketExists != nil {
		return nil, errBucketExists
	}
	if !exists {
		err = client.MakeBucket(ctx, cfg.S3Bucket, minio.MakeBucketOptions{})
		if err != nil {
			return nil, err
		}
		log.Printf("Bucket %s created\n", cfg.S3Bucket)
	} else {
		log.Printf("Bucket %s already exists\n", cfg.S3Bucket)
	}

	return client, nil
}

// initRedis initializes the Redis client.
func initRedis(cfg Config) *redis.Client {
	rdb := redis.NewClient(&redis.Options{
		Addr:     cfg.RedisAddr,
		Password: cfg.RedisPassword,
		DB:       cfg.RedisDB,
	})
	return rdb
}

// setExistenceCache sets the existence flag in Redis.
func setExistenceCache(key string, exists bool) {
	redisKey := "exists:" + key
	val := "0"
	if exists {
		val = "1"
	}
	err := redisClient.Set(ctx, redisKey, val, cfg.ExistenceCacheTTL).Err()
	if err != nil {
		log.Printf("Failed to set existence cache for %s: %v", key, err)
	}
}

// checkExistenceCache checks Redis for the existence flag.
func checkExistenceCache(key string) (bool, bool) {
	redisKey := "exists:" + key
	val, err := redisClient.Get(ctx, redisKey).Result()
	if err == redis.Nil {
		// Key not found.
		return false, false
	} else if err != nil {
		log.Printf("Redis error on key %s: %v", key, err)
		return false, false
	}
	return val == "1", true
}

// cachePresignedURL stores the generated pre-signed URL in Redis.
func cachePresignedURL(key, url string) {
	redisKey := "url:" + key
	err := redisClient.Set(ctx, redisKey, url, cfg.URLCacheTTL).Err()
	if err != nil {
		log.Printf("Failed to cache presigned URL for %s: %v", key, err)
	}
}

// getCachedPresignedURL retrieves the cached pre-signed URL.
func getCachedPresignedURL(key string) (string, bool) {
	redisKey := "url:" + key
	val, err := redisClient.Get(ctx, redisKey).Result()
	if err == redis.Nil {
		return "", false
	} else if err != nil {
		log.Printf("Redis error on key %s: %v", key, err)
		return "", false
	}
	return val, true
}

// generatePresignedURL generates a new pre-signed URL for an object.
func generatePresignedURL(key string) (string, error) {
	reqParams := make(url.Values)
	// You can add custom request parameters here if needed.
	presignedURL, err := minioClient.PresignedGetObject(ctx, cfg.S3Bucket, key, cfg.PresignExpiry, reqParams)
	if err != nil {
		return "", err
	}
	return presignedURL.String(), nil
}

// urlValues is a helper type alias to pass request params (minio expects url.Values).
type urlValues map[string][]string

func (v urlValues) Encode() string {
	// Minimal implementation.
	var s []string
	for key, values := range v {
		for _, value := range values {
			s = append(s, fmt.Sprintf("%s=%s", key, value))
		}
	}
	return strings.Join(s, "&")
}

func main() {
	// Initialize configuration.
	cfg = initConfig()

	// Initialize MinIO client.
	var err error
	minioClient, err = initMinio(cfg)
	if err != nil {
		log.Fatalf("Failed to initialize S3 client: %v", err)
	}

	// Initialize Redis client.
	redisClient = initRedis(cfg)
	// Test Redis connection.
	_, err = redisClient.Ping(ctx).Result()
	if err != nil {
		log.Fatalf("Failed to connect to Redis: %v", err)
	}

	// Use Gin as the HTTP router.
	router := gin.Default()

	// Upload an object.
	// Example: POST /item?key=myobject.txt
	router.POST("/item", func(c *gin.Context) {
		key := c.Query("key")
		if key == "" {
			c.JSON(http.StatusBadRequest, gin.H{"error": "Missing key parameter"})
			return
		}

		// If the object already exists, just pretend it was uploaded successfully.
		if exists, found := checkExistenceCache(key); found && exists {
			c.JSON(http.StatusOK, gin.H{"message": "Object already exists", "key": key})
			return
		}

		// Read the file from the request body.
		// (In production you may want to support multipart uploads.)
		data, err := io.ReadAll(c.Request.Body)
		if err != nil {
			c.JSON(http.StatusBadRequest, gin.H{"error": "Failed to read request body"})
			return
		}
		if len(data) == 0 {
			c.JSON(http.StatusBadRequest, gin.H{"error": "Empty body"})
			return
		}

		// Upload the object to the bucket.
		_, err = minioClient.PutObject(ctx, cfg.S3Bucket, key,
			strings.NewReader(string(data)), int64(len(data)),
			minio.PutObjectOptions{ContentType: "application/octet-stream"})
		if err != nil {
			c.JSON(http.StatusInternalServerError, gin.H{"error": fmt.Sprintf("Upload error: %v", err)})
			return
		}

		// Set existence cache.
		setExistenceCache(key, true)
		// Remove any existing cached URL.
		redisClient.Del(ctx, "url:"+key)

		c.JSON(http.StatusOK, gin.H{"message": "Object uploaded", "key": key})
	})

	// Retrieve an object (respond with redirect to presigned URL).
	// Example: GET /item/myobject.txt
	router.GET("/item/:key", func(c *gin.Context) {
		key := c.Param("key")
		if key == "" {
			c.JSON(http.StatusBadRequest, gin.H{"error": "Missing key"})
			return
		}

		// First, check Redis for existence.
		exists, found := checkExistenceCache(key)
		if !found || !exists {
			// If not found in cache, we assume it doesn't exist.
			c.JSON(http.StatusNotFound, gin.H{"error": "Object does not exist"})
			return
		}

		// Next, try to retrieve a cached pre-signed URL.
		if url, ok := getCachedPresignedURL(key); ok {
			c.JSON(http.StatusOK, gin.H{"url": url})
			return
		}

		// If not cached, generate a new pre-signed URL.
		url, err := generatePresignedURL(key)
		if err != nil {
			c.JSON(http.StatusInternalServerError, gin.H{"error": fmt.Sprintf("Failed to generate presigned URL: %v", err)})
			return
		}
		// Cache the URL.
		cachePresignedURL(key, url)

		c.JSON(http.StatusOK, gin.H{"url": url})
	})

	// List objects.
	// Example: GET /list?prefix=folder/subfolder/
	router.GET("/list", func(c *gin.Context) {
		prefix := c.Query("prefix")

		objectCh := minioClient.ListObjects(ctx, cfg.S3Bucket, minio.ListObjectsOptions{
			Prefix:    prefix,
			Recursive: true,
		})

		var objects []string
		for object := range objectCh {
			if object.Err != nil {
				c.JSON(http.StatusInternalServerError, gin.H{"error": object.Err.Error()})
				return
			}
			objects = append(objects, object.Key)
			// Optionally, update the existence cache for these objects.
			setExistenceCache(object.Key, true)
		}
		c.JSON(http.StatusOK, gin.H{"objects": objects})
	})

	// Delete an object (or folder by prefix).
	// Example: DELETE /item/myobject.txt
	router.DELETE("/item/:key", func(c *gin.Context) {
		key := c.Param("key")
		if key == "" {
			c.JSON(http.StatusBadRequest, gin.H{"error": "Missing key"})
			return
		}

		// Delete object from bucket.
		err := minioClient.RemoveObject(ctx, cfg.S3Bucket, key, minio.RemoveObjectOptions{})
		if err != nil {
			c.JSON(http.StatusInternalServerError, gin.H{"error": fmt.Sprintf("Delete error: %v", err)})
			return
		}

		// Remove from Redis caches.
		redisClient.Del(ctx, "exists:"+key)
		redisClient.Del(ctx, "url:"+key)

		c.JSON(http.StatusOK, gin.H{"message": "Object deleted", "key": key})
	})

	// Generate a presigned URL explicitly.
	// Example: GET /presign/myobject.txt
	router.GET("/presign/:key", func(c *gin.Context) {
		key := c.Param("key")
		if key == "" {
			c.JSON(http.StatusBadRequest, gin.H{"error": "Missing key"})
			return
		}

		// Check existence cache first.
		exists, found := checkExistenceCache(key)
		if !found || !exists {
			c.JSON(http.StatusNotFound, gin.H{"error": "Object does not exist"})
			return
		}

		// Check for a cached URL.
		if url, ok := getCachedPresignedURL(key); ok {
			c.JSON(http.StatusOK, gin.H{"url": url})
			return
		}

		// Generate a new presigned URL.
		url, err := generatePresignedURL(key)
		if err != nil {
			c.JSON(http.StatusInternalServerError, gin.H{"error": fmt.Sprintf("Failed to generate presigned URL: %v", err)})
			return
		}
		cachePresignedURL(key, url)
		c.JSON(http.StatusOK, gin.H{"url": url})
	})

	// Start the server.
	port := getEnv("PORT", "8080")
	log.Printf("Starting storage microservice on port %s", port)
	if err := router.Run(":" + port); err != nil {
		log.Fatalf("Failed to run server: %v", err)
	}
}

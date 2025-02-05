package storage

import (
	"bytes"
	"encoding/json"
	"fmt"
	"net/http"
	"net/url"
	"time"
)

// Client represents a client to the storage service API.
type Client struct {
	BaseURL    string
	HTTPClient *http.Client
}

// NewClient creates a new Client with the given base URL.
func NewClient(baseURL string) *Client {
	return &Client{
		BaseURL: baseURL,
		HTTPClient: &http.Client{
			Timeout: 5000 * time.Millisecond,
			// Do not follow redirects
			CheckRedirect: func(req *http.Request, via []*http.Request) error {
				return http.ErrUseLastResponse
			},
		},
	}
}

// UploadObject uploads a file to the storage service.
func (c *Client) UploadObject(key string, data []byte) error {
	reqURL := fmt.Sprintf("%s/item?key=%s", c.BaseURL, url.QueryEscape(key))
	req, err := http.NewRequest("POST", reqURL, bytes.NewBuffer(data))
	if err != nil {
		return err
	}

	resp, err := c.HTTPClient.Do(req)
	if err != nil {
		return err
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusOK {
		return fmt.Errorf("upload failed with status: %s", resp.Status)
	}

	return nil
}

// GetObjectURL retrieves a pre-signed URL for the object.
func (c *Client) GetObjectURL(key string) (string, error) {
	reqURL := fmt.Sprintf("%s/item/%s", c.BaseURL, url.QueryEscape(key))
	resp, err := c.HTTPClient.Get(reqURL)
	if err != nil {
		return "", err
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusFound {
		return "", fmt.Errorf("failed to get object URL with status: %s", resp.Status)
	}

	location := resp.Header.Get("Location")
	if location == "" {
		return "", fmt.Errorf("no Location header in response")
	}

	return location, nil
}

// ListObjects lists objects under the given prefix.
func (c *Client) ListObjects(prefix string) ([]string, error) {
	reqURL := fmt.Sprintf("%s/list?prefix=%s", c.BaseURL, url.QueryEscape(prefix))
	resp, err := c.HTTPClient.Get(reqURL)
	if err != nil {
		return nil, err
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusOK {
		return nil, fmt.Errorf("list objects failed with status: %s", resp.Status)
	}

	var result struct {
		Objects []string `json:"objects"`
	}
	if err := json.NewDecoder(resp.Body).Decode(&result); err != nil {
		return nil, err
	}

	return result.Objects, nil
}

// DeleteObject deletes an object from the storage service.
func (c *Client) DeleteObject(key string) error {
	reqURL := fmt.Sprintf("%s/item/%s", c.BaseURL, url.QueryEscape(key))
	req, err := http.NewRequest("DELETE", reqURL, nil)
	if err != nil {
		return err
	}

	resp, err := c.HTTPClient.Do(req)
	if err != nil {
		return err
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusOK {
		return fmt.Errorf("delete failed with status: %s", resp.Status)
	}

	return nil
}

// GeneratePresignedURL generates a pre-signed URL for the object.
func (c *Client) GeneratePresignedURL(key string) (string, error) {
	reqURL := fmt.Sprintf("%s/presign/%s", c.BaseURL, url.QueryEscape(key))
	resp, err := c.HTTPClient.Get(reqURL)
	if err != nil {
		return "", err
	}
	defer resp.Body.Close()

	if resp.StatusCode != http.StatusOK {
		return "", fmt.Errorf("generate presigned URL failed with status: %s", resp.Status)
	}

	var result struct {
		URL string `json:"url"`
	}
	if err := json.NewDecoder(resp.Body).Decode(&result); err != nil {
		return "", err
	}

	return result.URL, nil
}

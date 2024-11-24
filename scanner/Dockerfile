FROM python:3.12-alpine
WORKDIR /app

COPY ./requirements.txt .
RUN apk add --no-cache git
RUN pip3 install --no-cache-dir -r ./requirements.txt
COPY . .
ENTRYPOINT ["python3", "-m"]
CMD ["scanner"]

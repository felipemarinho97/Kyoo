FROM python:3.12-slim
WORKDIR /app

COPY ./requirements.txt .
RUN pip3 install --no-cache-dir -r ./requirements.txt
COPY . .
ENTRYPOINT ["python3", "-m"]
CMD ["scanner"]

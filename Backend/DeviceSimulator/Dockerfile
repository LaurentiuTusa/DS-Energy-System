# Use the official Python image
FROM python:3.8-slim

# Set the working directory inside the container
WORKDIR /app

# Copy the necessary files into the container
COPY . .

# Install dependencies
RUN pip install --no-cache-dir pika

# Expose the port if your application uses any
# EXPOSE 8080

# Command to run your application
CMD ["python", "main.py"]
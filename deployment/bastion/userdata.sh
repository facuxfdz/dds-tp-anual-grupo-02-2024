#!/bin/bash
# Install MySQL
sudo yum update -y
sudo yum install -y mysql

# Install Python3
sudo yum install -y python3
sudo pip3 install --upgrade pip

# Install Python dependencies
sudo pip3 install numpy pika argparse

# Place the RabbitMQ producer script
sudo mkdir -p /opt/rabbitmq

sudo cat << 'EOF' > /opt/rabbitmq/rabbitmq_producer.py
#!/usr/bin/env python3
import os
import pika
import numpy as np
import argparse
import time
from datetime import datetime

# Parse command-line arguments
def parse_args():
    parser = argparse.ArgumentParser(description="RabbitMQ Producer for Sensor Data")
    parser.add_argument('--host', type=str, default='localhost', help="RabbitMQ server host")
    parser.add_argument('--username', type=str, default='guest', help="RabbitMQ username (default: guest)")
    parser.add_argument('--password', type=str, default='guest', help="RabbitMQ password (default: guest)")
    parser.add_argument('--duration', type=int, required=True, help="Duration to send messages (in seconds)")
    parser.add_argument('--sensor', action='append', required=True, help="Sensor id and queue name, in the format <id>:<queue_name>")
    parser.add_argument('--mean', type=float, default=25.0, help="Mean temperature value (default is 25)")
    return parser.parse_args()

# Function to generate right-skewed log-normal data
def generate_temperatures(mean, size):
    # Log-normal distribution parameters (mean and sigma)
    sigma = 0.02  # Set a reasonable standard deviation to get skewed values
    mu = np.log(mean) - (sigma ** 2) / 2
    
    # Generate log-normal data
    log_normal_data = np.random.lognormal(mu, sigma, size)
    
    return log_normal_data

# Main function to send messages
def main():
    # Parse arguments
    args = parse_args()
    
    # Environment variables
    sample_size = int(os.getenv("SAMPLE_SIZE", "100"))  # Default sample size

    # Establish RabbitMQ connection
    credentials = pika.PlainCredentials(args.username, args.password)
    connection = pika.BlockingConnection(pika.ConnectionParameters(host=args.host, credentials=credentials))
    channel = connection.channel()

    # Create a dictionary for sensor-queue mappings
    sensor_queues = {}
    for sensor in args.sensor:
        sensor_id, queue_name = sensor.split(":")
        sensor_queues[sensor_id] = queue_name
        # Declare the queue for each sensor
        channel.queue_declare(queue=queue_name, durable=False)

    # Generate temperatures and send them for the given duration
    end_time = time.time() + args.duration
    while time.time() < end_time:
        for sensor_id, queue_name in sensor_queues.items():
            temperatures = generate_temperatures(args.mean, sample_size)
            messages = [{"SensorId": sensor_id, "Fecha": datetime.utcnow().isoformat(), "Temperatura": f"{temp:.2f}"} for temp in temperatures]
            
            for message in messages:
                channel.basic_publish(
                    exchange="",
                    routing_key=queue_name,
                    body=str(message),
                    properties=pika.BasicProperties(delivery_mode=2)  # Persistent message
                )
                print(f"Sent: {message}")
        
        # Sleep for a short period to avoid too rapid message sending
        time.sleep(1)

    # Close connection after the duration ends
    connection.close()

if __name__ == "__main__":
    main()

EOF

sudo chmod +x /opt/rabbitmq/rabbitmq_producer.py

# Place the requiremnts file
sudo cat << 'EOF' > /opt/rabbitmq/requirements.txt
numpy
pika
argparse
EOF

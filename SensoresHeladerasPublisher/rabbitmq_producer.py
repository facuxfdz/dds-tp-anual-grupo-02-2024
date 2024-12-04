import os
import pika
import numpy as np

# RabbitMQ connection
connection = pika.BlockingConnection(pika.ConnectionParameters(host="localhost"))
channel = connection.channel()
channel.queue_declare(queue="temperatura", durable=False)

# Environment variables
mean_temperature = float(os.getenv("MEAN_TEMPERATURE", "25.0"))  # Default mean
sample_size = int(os.getenv("SAMPLE_SIZE", "100"))

# Generate right-skewed Gaussian data
def generate_temperatures(mean, size):
    normal_data = np.random.normal(loc=mean, scale=5, size=size)
    right_skewed_data = np.exp(normal_data / 10)  # Add right-skew
    return right_skewed_data

# Send messages
temperatures = generate_temperatures(mean_temperature, sample_size)
messages = [{"SensorId": "f1f60edf-b97b-4190-a11a-e330dcbfc449", "Fecha": "2024-12-03T12:00:00Z", "Temperatura": f"{temp:.2f}"} for temp in temperatures]

for message in messages:
    channel.basic_publish(
        exchange="",
        routing_key="temperatura",
        body=str(message),
        properties=pika.BasicProperties(delivery_mode=2)  # Persistent message
    )
    print(f"Sent: {message}")

connection.close()

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
$(cat ${GITHUB_ACTION_PATH}/SensoresHeladerasPublisher/rabbitmq_producer.py)
EOF
sudo chmod +x /opt/rabbitmq/rabbitmq_producer.py

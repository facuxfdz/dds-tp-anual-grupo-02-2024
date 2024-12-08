# Usar una imagen base de Python
FROM python:3.9-slim

# Configurar el directorio de trabajo dentro del contenedor
WORKDIR /app

# Copiar requirements.txt y rabbitmq_producer.py al contenedor
COPY requirements.txt .
COPY rabbitmq_producer.py .

# Instalar las dependencias
RUN pip install --no-cache-dir -r requirements.txt

# Mantener el contenedor en ejecución indefinidamente
CMD ["tail", "-f", "/dev/null"]

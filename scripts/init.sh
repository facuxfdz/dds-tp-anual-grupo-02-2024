#!/bin/bash
set -e  # Exit immediately if any command fails

echo "Starting initialization..."

# Determine the environment and connection string
if [ "$ASPNETCORE_ENVIRONMENT" = "Production" ]; then
    echo "Fetching production connection string..."
    connectionString=$(dotnet AccesoAlimentario.Web.dll get-connection-string)
else
    echo "Using development connection details..."
    connectionString="server=${DB_SERVER:-localhost};database=${DB_NAME:-MyDB};user=${DB_USER:-root};password=${DB_PASSWORD:-password};"
fi

# Apply migrations using the EF bundle
echo "Applying migrations..."
./efbundle --connection "$connectionString"

# Start the application
echo "Starting the application..."
exec dotnet AccesoAlimentario.Web.dll

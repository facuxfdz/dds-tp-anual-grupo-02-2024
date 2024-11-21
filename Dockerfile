# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY ["AccesoAlimentario.Web/AccesoAlimentario.Web.csproj", "AccesoAlimentario.Web/"]
COPY ["AccesoAlimentario.Core/AccesoAlimentario.Core.csproj", "AccesoAlimentario.Core/"]
COPY ["AccesoAlimentario.Operations/AccesoAlimentario.Operations.csproj", "AccesoAlimentario.Operations/"]
RUN dotnet restore "AccesoAlimentario.Web/AccesoAlimentario.Web.csproj"

# Copy the rest of the code and build the application
COPY . .
WORKDIR "/src/AccesoAlimentario.Web"
RUN dotnet build "AccesoAlimentario.Web.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet publish "AccesoAlimentario.Web.csproj" -c Release -o /app/publish

# Create the migration bundle
FROM publish AS migrations
WORKDIR /app/publish
RUN dotnet ef migrations bundle --project ../AccesoAlimentario.Core/AccesoAlimentario.Core.csproj \
    --startup-project ../AccesoAlimentario.Web/AccesoAlimentario.Web.csproj \
    --context AccesoAlimentario.Core.DAL.AppDbContext --output ./efbundle

# Final runtime image
FROM base AS final
WORKDIR /app

# Copy the published app and the EF bundle
COPY --from=migrations /app/publish .

# Copy the initialization script into the image
COPY scripts/init.sh /app/init.sh
RUN chmod +x /app/init.sh

ENV ASPNETCORE_URLS=http://+:8085

# Run the initialization script as the entrypoint
ENTRYPOINT ["/app/init.sh"]

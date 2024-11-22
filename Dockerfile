FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Usa la imagen de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos de proyecto y restaura las dependencias
COPY ["AccesoAlimentario.Web/AccesoAlimentario.Web.csproj", "AccesoAlimentario.Web/"]
COPY ["AccesoAlimentario.Core/AccesoAlimentario.Core.csproj", "AccesoAlimentario.Core/"]
COPY ["AccesoAlimentario.Operations/AccesoAlimentario.Operations.csproj", "AccesoAlimentario.Operations/"]
RUN dotnet restore "AccesoAlimentario.Web/AccesoAlimentario.Web.csproj"

# Copia el resto del código y compila la aplicación
COPY . .
WORKDIR "/src/AccesoAlimentario.Web"
RUN dotnet build "AccesoAlimentario.Web.csproj" -c Release -o /app/build

# Publica la aplicación
FROM build AS publish
RUN dotnet publish "AccesoAlimentario.Web.csproj" -c Release -o /app/publish

# Usa la imagen base para ejecutar la aplicación
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8085

ENTRYPOINT ["dotnet", "AccesoAlimentario.Web.dll"]
# SDK utilizado para compilar y publicar
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env

WORKDIR /src

# Copiar toda la solución porque la WebApi referencia otros proyectos
COPY . .

# Ir al proyecto WebApi
WORKDIR /src/Servicio/Pacogroup.Ecommerce.Servicio.WebApi

# Restaurar dependencias
RUN dotnet restore Pacogroup.Ecommerce.Services.WebApi.csproj

# Publicar en Release
RUN dotnet publish Pacogroup.Ecommerce.Services.WebApi.csproj \
    -c Release \
    -o /app/out \
    --no-restore


# Imagen final: solo runtime ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

# Copiar únicamente los archivos publicados
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Pacogroup.Ecommerce.Services.WebApi.dll"]
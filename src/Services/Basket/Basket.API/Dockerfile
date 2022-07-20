FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Download PwC certificates.
WORKDIR /opt/scripts
COPY ["Services/Catalog/Catalog.API/scripts/get_pwc_certs.sh", "."]
RUN set -e; \
    apt-get update; \
    apt-get install -y curl; \
    ./get_pwc_certs.sh

# Install PwC certificates.
RUN set -e; \
    update-ca-certificates; \
    useradd -m appuser;
    
WORKDIR /src
COPY ["Services/Basket/Basket.API/Basket.API.csproj", "Services/Basket/Basket.API/"]
RUN dotnet restore "Services/Basket/Basket.API/Basket.API.csproj"
COPY . .
WORKDIR "/src/Services/Basket/Basket.API"
RUN dotnet build "Basket.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]

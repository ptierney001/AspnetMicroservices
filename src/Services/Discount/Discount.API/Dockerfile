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
COPY ["Services/Discount/Discount.API/Discount.API.csproj", "Services/Discount/Discount.API/"]
RUN dotnet restore "Services/Discount/Discount.API/Discount.API.csproj"
COPY . .
WORKDIR "/src/Services/Discount/Discount.API"
RUN dotnet build "Discount.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Discount.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Discount.API.dll"]

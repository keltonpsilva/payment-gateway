FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /build
COPY . .
RUN dotnet restore
RUN dotnet publish /build/src/PaymentGateway.WebAPI --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "PaymentGateway.WebAPI.dll"]

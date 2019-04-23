FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Gateway/Gateway.Api/Gateway.Api.csproj Gateway/Gateway.Api/
RUN dotnet restore Gateway/Gateway.Api/Gateway.Api.csproj
COPY . .
WORKDIR /src/Gateway/Gateway.Api
RUN dotnet build Gateway.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Gateway.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Gateway.Api.dll"]

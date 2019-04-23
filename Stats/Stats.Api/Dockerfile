FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Stats/Stats.Api/Stats.Api.csproj", "Stats/Stats.Api/"]
COPY ["BlogSolution.Framework/BlogSolution.Framework.csproj", "BlogSolution.Framework/"]
COPY ["Stats/Stats.Domain/Stats.Domain.csproj", "Stats/Stats.Domain/"]
COPY ["Stats/Stats.Application/Stats.Application.csproj", "Stats/Stats.Application/"]
RUN dotnet restore "Stats/Stats.Api/Stats.Api.csproj"
COPY . .
WORKDIR "/src/Stats/Stats.Api"
RUN dotnet build "Stats.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Stats.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Stats.Api.dll"]
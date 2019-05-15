FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Stats/Stats.Api/Stats.Api.csproj", "Stats/Stats.Api/"]
COPY ["Framework/BlogSolution.EventBusRabbitMQ/BlogSolution.EventBusRabbitMQ.csproj", "Framework/BlogSolution.EventBusRabbitMQ/"]
COPY ["Framework/BlogSolution.EventBus/BlogSolution.EventBus.csproj", "Framework/BlogSolution.EventBus/"]
COPY ["Framework/BlogSolution.Shared/BlogSolution.Shared.csproj", "Framework/BlogSolution.Shared/"]
COPY ["Framework/BlogSolution.Mvc/BlogSolution.Mvc.csproj", "Framework/BlogSolution.Mvc/"]
COPY ["Framework/BlogSolution.Types/BlogSolution.Types.csproj", "Framework/BlogSolution.Types/"]
COPY ["Stats/Stats.Domain/Stats.Domain.csproj", "Stats/Stats.Domain/"]
COPY ["Framework/BlogSolution.Mongo/BlogSolution.Mongo.csproj", "Framework/BlogSolution.Mongo/"]
COPY ["Framework/BlogSolution.Authentication/BlogSolution.Authentication.csproj", "Framework/BlogSolution.Authentication/"]
COPY ["Framework/BlogSolution.Consul/BlogSolution.Consul.csproj", "Framework/BlogSolution.Consul/"]
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
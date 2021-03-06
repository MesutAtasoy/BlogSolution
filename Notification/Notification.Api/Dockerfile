FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Notification/Notification.Api/Notification.Api.csproj", "Notification/Notification.Api/"]
COPY ["Framework/BlogSolution.EventBusRabbitMQ/BlogSolution.EventBusRabbitMQ.csproj", "Framework/BlogSolution.EventBusRabbitMQ/"]
COPY ["Framework/BlogSolution.EventBus/BlogSolution.EventBus.csproj", "Framework/BlogSolution.EventBus/"]
COPY ["Framework/BlogSolution.Shared/BlogSolution.Shared.csproj", "Framework/BlogSolution.Shared/"]
COPY ["Framework/BlogSolution.Mvc/BlogSolution.Mvc.csproj", "Framework/BlogSolution.Mvc/"]
COPY ["Framework/BlogSolution.Types/BlogSolution.Types.csproj", "Framework/BlogSolution.Types/"]
COPY ["Notification/Notification.Application/Notification.Application.csproj", "Notification/Notification.Application/"]
COPY ["Framework/BlogSolution.Logging/BlogSolution.Logging.csproj", "Framework/BlogSolution.Logging/"]

RUN dotnet restore "Notification/Notification.Api/Notification.Api.csproj"
COPY . .
WORKDIR "/src/Notification/Notification.Api"
RUN dotnet build "Notification.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Notification.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Notification.Api.dll"]
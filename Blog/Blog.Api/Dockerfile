FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Blog/Blog.Api/Blog.Api.csproj", "Blog/Blog.Api/"]
COPY ["Framework/BlogSolution.Context/BlogSolution.Context.csproj", "Framework/BlogSolution.Context/"]
COPY ["Blog/Blog.Persistance/Blog.Persistance.csproj", "Blog/Blog.Persistance/"]
COPY ["Blog/Blog.Domain/Blog.Domain.csproj", "Blog/Blog.Domain/"]
COPY ["Framework/BlogSolution.Types/BlogSolution.Types.csproj", "Framework/BlogSolution.Types/"]
COPY ["Framework/BlogSolution.Shared/BlogSolution.Shared.csproj", "Framework/BlogSolution.Shared/"]
COPY ["Framework/BlogSolution.EventBusRabbitMQ/BlogSolution.EventBusRabbitMQ.csproj", "Framework/BlogSolution.EventBusRabbitMQ/"]
COPY ["Framework/BlogSolution.EventBus/BlogSolution.EventBus.csproj", "Framework/BlogSolution.EventBus/"]
COPY ["Blog/Blog.Application/Blog.Application.csproj", "Blog/Blog.Application/"]
COPY ["Framework/BlogSolution.Mvc/BlogSolution.Mvc.csproj", "Framework/BlogSolution.Mvc/"]
COPY ["Framework/BlogSolution.Authentication/BlogSolution.Authentication.csproj", "Framework/BlogSolution.Authentication/"]
COPY ["Framework/BlogSolution.Consul/BlogSolution.Consul.csproj", "Framework/BlogSolution.Consul/"]
RUN dotnet restore "Blog/Blog.Api/Blog.Api.csproj"
COPY . .
WORKDIR "/src/Blog/Blog.Api"
RUN dotnet build "Blog.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Blog.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Blog.Api.dll"]
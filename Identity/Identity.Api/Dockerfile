FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Identity/Identity.Api/Identity.Api.csproj", "Identity/Identity.Api/"]
COPY ["Identity/Identity.Application/Identity.Application.csproj", "Identity/Identity.Application/"]
COPY ["Identity/Identity.Persistance/Identity.Persistance.csproj", "Identity/Identity.Persistance/"]
COPY ["Identity/Identity.Domain/Identity.Domain.csproj", "Identity/Identity.Domain/"]
COPY ["Framework/BlogSolution.Types/BlogSolution.Types.csproj", "Framework/BlogSolution.Types/"]
COPY ["Framework/BlogSolution.Shared/BlogSolution.Shared.csproj", "Framework/BlogSolution.Shared/"]
COPY ["Framework/BlogSolution.Authentication/BlogSolution.Authentication.csproj", "Framework/BlogSolution.Authentication/"]
COPY ["Framework/BlogSolution.Context/BlogSolution.Context.csproj", "Framework/BlogSolution.Context/"]
COPY ["Framework/BlogSolution.Mvc/BlogSolution.Mvc.csproj", "Framework/BlogSolution.Mvc/"]
RUN dotnet restore "Identity/Identity.Api/Identity.Api.csproj"
COPY . .
WORKDIR "/src/Identity/Identity.Api"
RUN dotnet build "Identity.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Identity.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Identity.Api.dll"]
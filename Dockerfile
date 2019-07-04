FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Source/Translation.Client.Web/Translation.Client.Web.csproj", "Source/Translation.Client.Web/"]
COPY ["Source/Translation.Data/Translation.Data.csproj", "Source/Translation.Data/"]
COPY ["Source/Translation.Common/Translation.Common.csproj", "Source/Translation.Common/"]
COPY ["Source/Translation.Service/Translation.Service.csproj", "Source/Translation.Service/"]
RUN dotnet restore "Source/Translation.Client.Web/Translation.Client.Web.csproj"
COPY . .
WORKDIR "/src/Source/Translation.Client.Web"
RUN dotnet build "Translation.Client.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Translation.Client.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY docker-entrypoint.sh /usr/local/bin/
RUN chmod 755 /usr/local/bin/docker-entrypoint.sh
ENTRYPOINT ["docker-entrypoint.sh"]
CMD ["dotnet", "Translation.Client.Web.dll"]

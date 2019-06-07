FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Source/Translation.Client.Web/Translation.Client.Web.csproj", "Source/Translation.Client.Web/"]
RUN dotnet restore "Source/Translation.Client.Web/Translation.Client.Web.csproj"
COPY . .
WORKDIR "/src/Source/Translation.Client.Web"
RUN dotnet build "Translation.Client.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Translation.Client.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Translation.Client.Web.dll"]
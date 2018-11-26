FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Source/Cheviri.Client.Web/Cheviri.Client.Web.csproj", "Source/Cheviri.Client.Web/"]
RUN dotnet restore "Source/Cheviri.Client.Web/Cheviri.Client.Web.csproj"
COPY . .
WORKDIR "/src/Source/Cheviri.Client.Web"
RUN dotnet build "Cheviri.Client.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Cheviri.Client.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Cheviri.Client.Web.dll"]
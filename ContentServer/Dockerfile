FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ContentServer/ContentServer.csproj", "ContentServer/"]
RUN dotnet restore "ContentServer/ContentServer.csproj"
COPY . .
WORKDIR "/src/ContentServer"
RUN dotnet build "ContentServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContentServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContentServer.dll"]

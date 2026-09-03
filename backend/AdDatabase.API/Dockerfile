FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY AdDatabase.sln .
COPY AdDatabase.BLL/AdDatabase.BLL.csproj AdDatabase.BLL/
COPY AdDatabase.DAL/AdDatabase.DAL.csproj AdDatabase.DAL/
COPY AdDatabase.API/AdDatabase.API.csproj AdDatabase.API/
RUN dotnet restore
COPY . .
RUN dotnet publish AdDatabase.API/AdDatabase.API.csproj -c Release -o /app/publish --no-restore
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
RUN mkdir -p /app/wwwroot/uploads && chown -R $APP_UID:$APP_UID /app/wwwroot
USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["dotnet","AdDatabase.API.dll"]

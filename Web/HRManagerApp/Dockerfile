# Базовый образ для ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Строим образ на основе SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копируем файлы проектов
COPY ["HRManagerApp/HRManagerApp.csproj", "HRManagerApp/"]
COPY ["CommonLibrary/CommonLibrary.csproj", "CommonLibrary/"]

# Восстановление зависимостей
RUN dotnet restore "HRManagerApp/HRManagerApp.csproj"

# Копируем весь исходный код
COPY . .

# Строим проект
WORKDIR "/src/HRManagerApp"
RUN dotnet build "HRManagerApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Публикуем проект
WORKDIR /src
FROM build AS publish
RUN dotnet publish "/src/HRManagerApp/HRManagerApp.csproj" -c Release -o /app/publish --no-build --verbosity detailed


# Финальный образ для работы
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HRManagerApp.dll"]

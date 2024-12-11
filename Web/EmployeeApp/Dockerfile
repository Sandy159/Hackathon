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
COPY ["EmployeeApp/EmployeeApp.csproj", "EmployeeApp/"]
COPY ["CommonLibrary/CommonLibrary.csproj", "CommonLibrary/"]

# Восстановление зависимостей
RUN dotnet restore "EmployeeApp/EmployeeApp.csproj"

# Копируем весь исходный код
COPY . .

# Строим проект
WORKDIR "/src/EmployeeApp"
RUN dotnet build "EmployeeApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Публикуем проект
FROM build AS publish
RUN dotnet publish "EmployeeApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false -v detailed

# Финальный образ для работы
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmployeeApp.dll"]

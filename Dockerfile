# Базовий образ для ASP.NET Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Образ для SDK для побудови застосунку
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Копіюємо файл проекту та відновлюємо залежності
COPY ["QuickBooking/*.csproj", "./"]
RUN dotnet restore

# Копіюємо весь проєкт та збираємо
COPY ["QuickBooking/", "./"]
RUN dotnet build -c Release -o /app/build

# Публікуємо застосунок
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Створення фінального образу
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Вказуємо точку входу для запуску застосунку
ENTRYPOINT ["dotnet", "QuickBooking.dll"]

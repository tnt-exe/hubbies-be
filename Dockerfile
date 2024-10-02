FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Hubbies.Web/Hubbies.Web.csproj", "src/Hubbies.Web/"]
COPY ["src/Hubbies.Application/Hubbies.Application.csproj", "src/Hubbies.Application/"]
COPY ["src/Hubbies.Domain/Hubbies.Domain.csproj", "src/Hubbies.Domain/"]
COPY ["src/Hubbies.Infrastructure/Hubbies.Infrastructure.csproj", "src/Hubbies.Infrastructure/"]
RUN dotnet restore "./src/Hubbies.Web/Hubbies.Web.csproj"
COPY . .
WORKDIR "/src/src/Hubbies.Web"
RUN dotnet build "./Hubbies.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Hubbies.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hubbies.Web.dll"]
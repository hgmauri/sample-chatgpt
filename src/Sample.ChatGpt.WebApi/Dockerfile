FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Sample.ChatGpt.WebApi/Sample.ChatGpt.WebApi.csproj", "src/Sample.ChatGpt.WebApi/"]
COPY ["src/Sample.ChatGpt.WebApi.Core/Sample.ChatGpt.WebApi.Core.csproj", "src/Sample.ChatGpt.WebApi.Core/"]
RUN dotnet restore "src/Sample.ChatGpt.WebApi/Sample.ChatGpt.WebApi.csproj"
COPY . .
WORKDIR "/src/src/Sample.ChatGpt.WebApi"
RUN dotnet build "Sample.ChatGpt.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sample.ChatGpt.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sample.ChatGpt.WebApi.dll"]
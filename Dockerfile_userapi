FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY ["UserApi/UserApi.csproj", "UserApi/"]
RUN dotnet restore "UserApi/UserApi.csproj"
COPY . .
WORKDIR "/src/UserApi"
RUN dotnet build "UserApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UserApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserApi.dll"]
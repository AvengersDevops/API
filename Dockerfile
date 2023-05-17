FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AvengersAPI/AvengersAPI.csproj", "AvengersAPI/"]
RUN dotnet restore "AvengersAPI/AvengersAPI.csproj"
COPY . .
WORKDIR "/src/AvengersAPI"
RUN dotnet build "AvengersAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AvengersAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AvengersAPI.dll"]

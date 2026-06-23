FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app
COPY API_UTVM.csproj .
RUN dotnet restore
COPY . .

RUN dotnet build API_UTVM.csproj -c Release -o /app/build

RUN dotnet publish API_UTVM.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "API_UTVM.dll"]
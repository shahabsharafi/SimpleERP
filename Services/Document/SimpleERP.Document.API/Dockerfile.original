FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Services/Document/SimpleERP.Document.API/SimpleERP.Document.API.csproj", "Services/Document/SimpleERP.Document.API/"]
RUN dotnet restore "Services/Document/SimpleERP.Document.API/SimpleERP.Document.API.csproj"
COPY . .
WORKDIR "/src/Services/Document/SimpleERP.Document.API"
RUN dotnet build "SimpleERP.Document.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SimpleERP.Document.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SimpleERP.Document.API.dll"]
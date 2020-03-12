FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["UI/Web/SimpleERP.UI.Web.csproj", "UI/UI.Web/"]
RUN dotnet restore "UI/Web/SimpleERP.UI.Web.csproj"
COPY . .
WORKDIR "/src/UI/Web"
RUN dotnet build "SimpleERP.UI.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SimpleERP.UI.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

RUN npm install

ENTRYPOINT ["dotnet", "SimpleERP.UI.Web.dll"]
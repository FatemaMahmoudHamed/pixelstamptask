#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PixelStamp.Portal/PixelStamp.Portal.csproj", "PixelStamp.Portal/"]
COPY ["PixelStamp.ServiceInterface/PixelStamp.ServiceInterface.csproj", "PixelStamp.ServiceInterface/"]
COPY ["PixelStamp.Infrastructure/PixelStamp.Infrastructure.csproj", "PixelStamp.Infrastructure/"]
COPY ["PixelStamp.Core/PixelStamp.Core.csproj", "PixelStamp.Core/"]
COPY ["PixelStamp.Common/PixelStamp.Common.csproj", "PixelStamp.Common/"]
RUN dotnet restore "PixelStamp.Portal/PixelStamp.Portal.csproj"
COPY . .
WORKDIR "/src/PixelStamp.Portal"
RUN dotnet build "PixelStamp.Portal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PixelStamp.Portal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PixelStamp.Portal.dll"]
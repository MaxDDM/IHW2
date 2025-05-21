FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY APIGateway/APIGateway.csproj APIGateway/
COPY FileStoringService/FileStoringService.csproj FileStoringService/
COPY FileAnalisysService/FileAnalisysService.csproj FileAnalisysService/

RUN dotnet restore APIGateway/APIGateway.csproj
RUN dotnet restore FileStoringService/FileStoringService.csproj
RUN dotnet restore FileAnalisysService/FileAnalisysService.csproj

COPY APIGateway/ APIGateway/
COPY FileStoringService/ FileStoringService/
COPY FileAnalisysService/ FileAnalisysService/


RUN dotnet publish APIGateway/APIGateway.csproj -c Release -o out/APIGateway
RUN dotnet publish FileStoringService/FileStoringService.csproj -c Release -o out/FileStoringService
RUN dotnet publish FileAnalisysService/FileAnalisysService.csproj -c Release -o out/FileAnalisysService

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app

COPY --from=build /app/out/APIGateway /app/APIGateway
COPY --from=build /app/out/FileStoringService /app/FileStoringService
COPY --from=build /app/out/FileAnalisysService /app/FileAnalisysService


CMD ["sh", "-c", "dotnet /app/APIGateway/APIGateway.dll & dotnet /app/FileStoringService/FileStoringService.dll & dotnet /app/FileAnalisysService/FileAnalisysService.dll; wait"]

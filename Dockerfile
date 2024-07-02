FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build


WORKDIR /src
COPY ["src/Mofleet.Web.Host/Mofleet.Web.Host.csproj", "src/Mofleet.Web.Host/"]
COPY ["src/Mofleet.Web.Core/Mofleet.Web.Core.csproj", "src/Mofleet.Web.Core/"]
COPY ["src/Mofleet.Application/Mofleet.Application.csproj", "src/Mofleet.Application/"]
COPY ["src/Mofleet.Core/Mofleet.Core.csproj", "src/Mofleet.Core/"]
COPY ["src/Mofleet.EntityFrameworkCore/Mofleet.EntityFrameworkCore.csproj", "src/Mofleet.EntityFrameworkCore/"]
WORKDIR "/src/src/Mofleet.Web.Host"

RUN dotnet restore 


WORKDIR /src
COPY ["src/Mofleet.Web.Host", "src/Mofleet.Web.Host"]
COPY ["src/Mofleet.Web.Core", "src/Mofleet.Web.Core"]
COPY ["src/Mofleet.Application", "src/Mofleet.Application"]
COPY ["src/Mofleet.Core", "src/Mofleet.Core"]
COPY ["src/Mofleet.EntityFrameworkCore", "src/Mofleet.EntityFrameworkCore"]
WORKDIR "/src/src/Mofleet.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "Mofleet.Web.Host.dll"]

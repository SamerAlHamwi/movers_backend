FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Install dependencies
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
        libc6-dev \
        libgdiplus \
        fontconfig && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Copy project files
COPY . .

# Restore packages and build application
RUN dotnet restore
RUN dotnet build --no-restore --configuration Release

# Publish application
RUN dotnet publish --no-build --configuration Release --output /app

# Create final image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

# Install dependencies
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
        libc6-dev \
        libgdiplus \
        fontconfig && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Copy application files
COPY --from=build /app .

# Set environment variables for System.Drawing
ENV ASPNETCORE_ENVIRONMENT="Development"
ENV ASPNETCORE_URLS="http://+:5000"
ENV LANG=C.UTF-8
ENV LC_ALL=C.UTF-8

# Set up symlink for libgdiplus
RUN ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll

# Expose port 5000
EXPOSE 5000

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Mofleet.Web.Host.dll"]

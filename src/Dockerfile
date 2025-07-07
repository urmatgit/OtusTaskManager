# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0.411 AS build
ARG BUILD_CONFIGURATION=Release
# Set proxy environment variables (replace with your actual proxy settings)
ARG HTTP_PROXY=http://erkimbaev:Urmatbek821@proxy.karavay.spb.ru:8080
ARG HTTPS_PROXY=http://erkimbaev:Urmatbek821@proxy.karavay.spb.ru:8080
ENV HTTP_PROXY=${HTTP_PROXY}
ENV HTTPS_PROXY=${HTTPS_PROXY}
ENV http_proxy=${HTTP_PROXY}
ENV https_proxy=${HTTPS_PROXY}
ENV NUGET_HTTP_PROXY=${HTTP_PROXY}
ENV NUGET_HTTPS_PROXY=${HTTPS_PROXY}
ENV NO_PROXY=localhost,127.0.0.1

# Configure NuGet to work with proxy
RUN echo "Configuring NuGet proxy settings..." && \
    mkdir -p /root/.nuget/NuGet && \
    echo "<?xml version=\"1.0\" encoding=\"utf-8\"?>" > /root/.nuget/NuGet/NuGet.Config && \
    echo "<configuration>" >> /root/.nuget/NuGet/NuGet.Config && \
    echo "  <config>" >> /root/.nuget/NuGet/NuGet.Config && \
    echo "    <add key=\"http_proxy\" value=\"${HTTP_PROXY}\" />" >> /root/.nuget/NuGet/NuGet.Config && \
    echo "    <add key=\"https_proxy\" value=\"${HTTPS_PROXY}\" />" >> /root/.nuget/NuGet/NuGet.Config && \
    echo "  </config>" >> /root/.nuget/NuGet/NuGet.Config && \
    echo "</configuration>" >> /root/.nuget/NuGet/NuGet.Config
RUN apt-get update && \
    apt-get install -y ca-certificates && \
    update-ca-certificates    
RUN curl -v https://api.nuget.org/v3/index.json || echo "Proxy connectivity check failed"

RUN dotnet workload uninstall --all && dotnet workload install --skip-manifest-update
WORKDIR /src
#COPY ["*.sln", "."]

COPY ["./UserService.DataAccess/*.csproj", "UserService.DataAccess/"]
RUN dotnet restore "UserService.DataAccess/UserService.DataAccess.csproj" --verbosity detailed

COPY ["./UserService.Business/*.csproj", "UserService.Business/"]
RUN dotnet restore "UserService.Business/UserService.Business.csproj"

COPY ["./UserService.Api/*.csproj", "UserService.Api/"]
RUN dotnet restore "UserService.Api/UserService.Api.csproj"
# Restore NuGet packages with proxy support
#RUN dotnet restore --interactive

COPY . .
# Build the solution
#RUN dotnet build -c Release --no-restore

WORKDIR "/UserService.Api"
RUN dotnet build "./UserService.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/build #--no-restore

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UserService.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false --no-restore #--no-build

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Api.dll"]
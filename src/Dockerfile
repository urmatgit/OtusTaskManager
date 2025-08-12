# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7080
EXPOSE 7081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0.411 AS build
ARG BUILD_CONFIGURATION=Release
 


WORKDIR /src
#COPY ["*.sln", "."]

COPY ["./ReferenceLibs/","ReferenceLibs/"] 
COPY ["./UserService.DataAccess/", "UserService.DataAccess/"]
COPY NuGet.Config .
RUN  dotnet restore "UserService.DataAccess/UserService.DataAccess.csproj"  --configfile NuGet.Config

COPY ["./UserService.Business/", "UserService.Business/"]
RUN dotnet restore "UserService.Business/UserService.Business.csproj"

COPY ["./UserService.Api/", "UserService.Api/"]
RUN dotnet restore "UserService.Api/UserService.Api.csproj"
# Restore NuGet packages with proxy support
#RUN dotnet restore --interactive

COPY . .
# Build the solution
#RUN dotnet build -c Release --no-restore

WORKDIR "./UserService.Api"
RUN dotnet build "./UserService.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/build  

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UserService.Api.csproj" -c %BUILD_CONFIGURATION% -o /app/publish /p:UseAppHost=false --no-restore #--no-build

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Api.dll"]
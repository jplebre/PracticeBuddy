# Dotnet Run for dev environments
# Lightweight and "watchable"
FROM mcr.microsoft.com/dotnet/sdk:9.0.301 AS development
COPY . /source
WORKDIR /source/src/PracticeBuddy.API
CMD dotnet run --no-launch-profile

#######

# Multi-layered image building, for faster building and ligther containers

# NOTE: `dotnet publish` needs to target project. If targetting solution, attempts to publish everything to same folder and fails.
# Leverage a cache mount to /root/.nuget/packages so that subsequent builds don't have to re-download packages.
# If TARGETARCH is "amd64", replace it with "x64" - "x64" is .NET's canonical name for this and "amd64" doesn't work in .NET 6.0.

# Create stage to build application
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0.301 AS build
ARG TARGETARCH

COPY src/ /source/src/
COPY tests/ /source/tests/
COPY *.sln /source/
COPY *.props /source/
WORKDIR /source

# Build the application.
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish ./src/PracticeBuddy.API/PracticeBuddy.API.csproj --arch "$(echo "$TARGETARCH" | sed 's/amd64/x64/g')" --use-current-runtime --self-contained true -o /app

# Create a stage to run the application in, with minimal runtime dependencies
# PORT: The default port is now 8080. Use ASPNETCORE_HTTP_PORTS to change. 
#    - Note that if you change port, you can't run as non-user
# More Info: https://learn.microsoft.com/en-us/dotnet/core/compatibility/containers/8.0/aspnet-port#reason-for-change
FROM mcr.microsoft.com/dotnet/aspnet:latest AS final
EXPOSE 8080
WORKDIR /app
COPY --from=build /app ./
# Run as non-root user (useful for k8s)
USER $APP_UID
ENTRYPOINT ["dotnet", "PracticeBuddy.API.dll"]
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This arg supports swapping out the base image for the final stage to support using the token proxy and gdb when debugging from VS
ARG FINAL_BASE_IMAGE=mcr.microsoft.com/dotnet/runtime-deps:8.0

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# Install clang/zlib1g-dev dependencies for publishing to native
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    clang zlib1g-dev
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VisualStudioCredentialAOTExample/VisualStudioCredentialAOTExample.csproj", "VisualStudioCredentialAOTExample/"]
RUN dotnet restore "./VisualStudioCredentialAOTExample/VisualStudioCredentialAOTExample.csproj"
COPY . .
WORKDIR "/src/VisualStudioCredentialAOTExample"
RUN dotnet build "./VisualStudioCredentialAOTExample.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VisualStudioCredentialAOTExample.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=true

# this stage is used to support debugging with gdb and the token proxy
FROM base as aotdebug
USER root
RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    gdb
USER app

FROM $FINAL_BASE_IMAGE AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["./VisualStudioCredentialAOTExample"]
FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-jammy

RUN apt-get update \
# Install .NET SDK 8.0
    && apt-get update && apt-get -y install dotnet-sdk-8.0 \
# Install .NET Runtime 7.0
    && apt-get update && apt-get -y install aspnetcore-runtime-7.0 \
# Install .NET Runtime 6.0
    && apt-get update && apt-get -y install aspnetcore-runtime-6.0 \
# Reports .NET info
    && dotnet --info \
# SSL client
    && apt-get -y install curl openssh-client \
# Install docker CLI
    && curl -k -L "https://download.docker.com/linux/debian/dists/bullseye/pool/stable/amd64/docker-ce-cli_20.10.23~3-0~debian-bullseye_amd64.deb" --output ./docker-ce-cli.deb \
    && dpkg -i ./docker-ce-cli.deb \
    && rm ./docker-ce-cli.deb \
# Install docker compose plugin
    && curl -k -L "https://download.docker.com/linux/debian/dists/bullseye/pool/stable/amd64/docker-compose-plugin_2.16.0-1~debian.11~bullseye_amd64.deb" --output ./docker-compose-plugin.deb \
    && dpkg -i ./docker-compose-plugin.deb \
    && rm ./docker-compose-plugin.deb \
# Install docker compose tool
    && curl -k -L "https://github.com/docker/compose/releases/download/v2.16.0/docker-compose-$(uname -s)-$(uname -m)" -o /usr/bin/docker-compose \
    && chmod +x /usr/bin/docker-compose
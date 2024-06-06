FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-jammy

# Docker    
RUN apt-get update \
    && apt-get -y install ca-certificates curl \
    && install -m 0755 -d /etc/apt/keyrings \
    && curl -fsSL https://download.docker.com/linux/ubuntu/gpg -o /etc/apt/keyrings/docker.asc \
    && chmod a+r /etc/apt/keyrings/docker.asc \
    && echo \
      "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/ubuntu \
      $(. /etc/os-release && echo "$VERSION_CODENAME") stable" | \
      tee /etc/apt/sources.list.d/docker.list > /dev/null \
    && apt-get update && apt-get -y install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin

# .NET
RUN curl -LO https://dot.net/v1/dotnet-install.sh --output ./dotnet-install.sh \
    && chmod +x ./dotnet-install.sh \
    && ./dotnet-install.sh --channel 8.0 \
    && ./dotnet-install.sh --channel 7.0 --runtime aspnetcore \
    && ./dotnet-install.sh --channel 6.0 --runtime aspnetcore \
    && rm ./dotnet-install.sh

ENV DOTNET_ROOT=/root/.dotnet
ENV PATH=$PATH:$DOTNET_ROOT:$DOTNET_ROOT/tools
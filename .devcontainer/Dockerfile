FROM mcr.microsoft.com/vscode/devcontainers/universal

RUN cd /tmp && wget https://download.visualstudio.microsoft.com/download/pr/dc930bff-ef3d-4f6f-8799-6eb60390f5b4/1efee2a8ea0180c94aff8f15eb3af981/dotnet-sdk-6.0.300-linux-x64.tar.gz
RUN mkdir -p $HOME/dotnet && tar zxf /tmp/dotnet-sdk-6.0.300-linux-x64.tar.gz -C $HOME/dotnet 
RUN rm /tmp/dotnet-sdk-6.0.300-linux-x64.tar.gz

ENV DOTNET_ROOT="$HOME/dotnet"
ENV PATH="$PATH:$HOME/dotnet"

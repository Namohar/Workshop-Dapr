FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY bin/Debug/net6.0/ App/
WORKDIR /App
# Install prerequisites
# RUN apt-get update && apt-get install -y curl
ENTRYPOINT ["dotnet", "Workshop.dll"]
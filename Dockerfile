FROM mcr.microsoft.com/dotnet/aspnet:8.0.1 AS base
WORKDIR /app
EXPOSE 8080
RUN apt update
RUN apt install -y redis
COPY ./dist/backend/NetAdmin.BizServer.Host/bin/Release/net8.0/publish .
ENTRYPOINT redis-server --daemonize yes && dotnet NetAdmin.BizServer.Host.dll -is
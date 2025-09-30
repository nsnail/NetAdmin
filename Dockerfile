FROM mcr.microsoft.com/dotnet/aspnet:9.0.9 AS base
WORKDIR /app
EXPOSE 8080
RUN apt update
RUN apt install -y redis
COPY ./dist/backend/YourSolution.AdmServer.Host/bin/Release/net9.0/publish .
ENTRYPOINT redis-server --daemonize yes && dotnet YourSolution.AdmServer.Host.dll -is
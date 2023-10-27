# 使用官方 ASP.NET Core 运行时作为基础镜像
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# 使用官方 .NET SDK 作为构建镜像
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 复制所有源代码并生成发布
COPY . .
WORKDIR "/src/src/backend/NetAdmin.BizServer.Host"
FROM build AS publish
RUN dotnet publish "NetAdmin.BizServer.Host.csproj" -c Release -o /app/publish

# 将应用程序发布到基础镜像
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetAdmin.BizServer.Host.dll"]
# NetAdmin

通用后台权限管理系统、快速开发框架（基于C#12/.NET9、Vue3/Vite、Element Plus等现代技术构建，具有十分整洁、优雅的编码规范）

[![.NET](https://github.com/nsnail/NetAdmin/actions/workflows/nightly-build.yml/badge.svg)](https://github.com/nsnail/NetAdmin/actions/workflows/nightly-build.yml)
[![MIT](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/nsnail/NetAdmin/blob/main/LICENSE)
[![FreeSql](https://img.shields.io/badge/FreeSql-3.x-orange.svg)](https://github.com/nsnail/NetAdmin/blob/main/LICENSE)

## 在线预览

https://na.tools92.top

## 一键运行

```shell
docker run -p 8080:8080 nsnail/netadmin
# 需翻墙
```

## 构建步骤

```shell
# 1. 检查 dotnet sdk 版本 >=9.0.0
dotnet --list-sdks
# 下载 dotnet：https://dotnet.microsoft.com/zh-cn/download/dotnet

# 2. 克隆代码仓库
git clone https://github.com/nsnail/NetAdmin.git && cd ./NetAdmin

# 3. 检查本机 redis 运行状态
redis-cli dbsize
# 下载 redis for windows：https://github.com/redis-windows/redis-windows/releases
# 下载 redis for linux/mac：https://redis.io/download

# 4. 运行后端 WebApi
dotnet run --project ./src/backend/NetAdmin.AdmServer.Host/NetAdmin.AdmServer.Host.csproj --urls http://[::]:5010 -is
# 浏览器打开 http://localhost:5010 ，将看到Swagger（Knife4jUI）界面

# 5. 检查 nodejs 版本 >=20
node -v
# 下载 nodejs：https://nodejs.org/en/download

# 6. 安装 npm 依赖包
cd ./src/frontend/admin && npm install

# 7. 运行前端项目
npm run dev
# 浏览器打开 http://localhost:5020 ，将看到管理界面（默认用户名：root，密码：1234qwer）
```

## 文件目录树

```
+---.template.config         # dotnet 项目模板配置目录
+---assets                   # 项目资源文件目录
+---build                    # 构建相关的工程文件目录
+---dist                     # 编译生成的二进制文件目录
+---docs                     # 项目开发文档目录
+---refs                     # 引用的第三方包的仓库目录
+---scripts                  # 各种工具脚本文件目录
+---src                      # 项目源码文件目录
```

## 后端项目架构

```mermaid
flowchart TD
H["NetAdmin.Host\n公共主机层\n（.Net自托管主机程序）\n（输入输出格式化）\n（数据校验、鉴权）\n（...所有HTTP管道过滤器中间件）"] --> C["NetAdmin.Cache\n公共缓存层\n（基于Redis或MemoryCache的缓存策略实现）"]
C --> A["NetAdmin.Application\n公共业务逻辑层\n（内部服务增删改查）\n（外部服务增删改查）\n（...所有业务用例的计算与组合逻辑的模块化）"]
A --> D["NetAdmin.Domain\n数据实体层\n（数据库关系实体映射）\n（DTO数据传输对象）\n（...所有数据模型的抽象与封装）"]
D --> I["NetAdmin.Infrastructure\n基础设施层\n（第三方组件和Nuget包引用）\n（公共构建和程序运行配置）\n（公共常量枚举异常定义）\n（全球化化和多语言）\n（...所有公共Utility工具）"]

XH["NetAdmin.XXX.Host\n（WebApi）"]-->H
XS["NetAdmin.XXXService\n（常驻内存服务）"]-->H
XS["NetAdmin.XXXService\n（常驻内存服务）"]-->XC
XC["NetAdmin.XXX.Cache\n（缓存层实例）"]-->C
XA["NetAdmin.XXX.Application\n（业务逻辑层实例）"]-->A

XH-->XC
XC-->XA
```

## 引用的开源代码 / 特别鸣谢

| 语言         | 集成领域          | 开源库                                                                                                                                                                                                                   |
|------------|---------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| C#         | Web基础框架       | [ASP.NET Core](https://github.com/dotnet/aspnetcore)                                                                                                                                                                  |
| C#         | 数据库关系映射       | [FreeSql](https://github.com/dotnetcore/FreeSql)                                                                                                                                                                      |
| C#         | 代码质量检查        | [Roslynator.Analyzers](https://github.com/josefpihrt/roslynator) \| [SonarAnalyzer.CSharp](https://github.com/SonarSource/sonar-dotnet) \| [StyleCop.Analyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers) |
| C#         | 单元测试框架        | [xunit](https://github.com/xunit/xunit)  \| [coverlet.collector](https://github.com/coverlet-coverage/coverlet)                                                                                                       |
| C#         | 分布式锁          | [RedLock.net](https://github.com/samcook/RedLock.net)                                                                                                                                                                 |
| C#         | 控制台终端界面库      | [Spectre.Console](https://github.com/spectreconsole/spectre.console)                                                                                                                                                  |
| C#         | 扩展函数库         | [NSExt](https://github.com/nsnail/ns-ext.git)                                                                                                                                                                         |
| C#         | 图形处理库         | [SixLabors.ImageSharp](https://github.com/SixLabors/ImageSharp)                                                                                                                                                       |
| C#         | 文件对象存储        | [Minio](https://github.com/minio/minio-dotnet)                                                                                                                                                                        |
| C#         | 性能监控采集        | [prometheus-net.AspNetCore](https://github.com/prometheus-net/prometheus-net)                                                                                                                                         |
| C#         | 雪花ID生成器       | [Yitter.IdGenerator](https://github.com/yitter/idgenerator)                                                                                                                                                           |
| C#         | 自动化版本管理       | [MinVer](https://github.com/adamralph/minver)                                                                                                                                                                         |
| C#         | JavaScript引擎  | [MsieJavaScriptEngine](https://github.com/Taritsyn/MsieJavaScriptEngine)                                                                                                                                              |
| C#         | WebApi图形界面    | [IGeekFan.AspNetCore.Knife4jUI](https://github.com/luoyunchong/IGeekFan.AspNetCore.Knife4jUI)                                                                                                                         |
| TypeScript | SPA基础框架       | [Vue](https://github.com/vuejs/core)                                                                                                                                                                                  |
| TypeScript | 前端构建工具        | [Vite](https://github.com/vitejs/vite)                                                                                                                                                                                |
| TypeScript | UI控件库         | [Element Plus](https://github.com/element-plus/element-plus)                                                                                                                                                          |
| TypeScript | CSS解析器        | [Sass](https://github.com/sass/sass)                                                                                                                                                                                  |
| TypeScript | 图表和数据可视化      | [ECharts](https://github.com/apache/echarts)                                                                                                                                                                          |
| JavaScript | 后台管理界面        | [SCUI](https://gitee.com/lolicode/scui)                                                                                                                                                                               |
| JavaScript | HTTP请求库       | [Axios](https://github.com/axios/axios)                                                                                                                                                                               |
| JavaScript | JavaScript解析器 | [Terser](https://github.com/terser/terser)                                                                                                                                                                            |
| JavaScript | 代码质量检查        | [ESLint](https://github.com/eslint/eslint)                                                                                                                                                                            |
| JavaScript | 代码格式化工具       | [Prettier](https://github.com/prettier/prettier)                                                                                                                                                                      |
| JavaScript | 标准加密库         | [crypto-js](https://github.com/brix/crypto-js)                                                                                                                                                                        |
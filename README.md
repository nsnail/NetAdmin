# NetAdmin

## Git Commits 语义

- `FEA` 新增特性
- `REF` 项目重构
- `FIX` 缺陷修复
- `PER` 性能优化
- `RVT` 还原变更
- `FMT` 格式整理
- `DOC` 文档变更
- `TST` 单元测试
- `BLD` 工程构建


## 构建指南
1. 后端
   1. 检查dotnet-sdk版本>=7.0.0
   ```
   dotnet --list-sdks
   ```
   2. 克隆代码仓库
   ```
   git clone https://github.com/nsnail/NetAdmin.git
   cd ./NetAdmin
   ```
   3. 确保本机redis处于运行状态
   ```
   redis-cli
   ```
   4. 运行后端WebApi
   ```
   dotnet run --project ./src/backend/NetAdmin.BizServer.Host/NetAdmin.BizServer.Host.csproj --urls http://[::]:65010
   ```
   5. 体验WebApi程序
   ```
   浏览器打开 http://localhost:65010 ，将看到Swagger（Knife4jUI）界面
   ```
2. 前端
    1. 检查nodejs版本>=20
    ```
    node -v
    ```
    2. 安装npm依赖包
    ```
    cd ./src/frontend/admin
    npm install
    ```
    3. 运行前端项目
    ```
    npm run dev
    ```
    4. 体验前端程序
    ```
    浏览器打开 http://localhost:65020 ，将看到管理界面（默认用户名：root，密码：1234qwer）
    ```

## 项目文件目录树描述
```
+---.template.config     dotnet 项目模板配置目录
+---assets               程序运行需要的资源文件目录
+---dist                 项目编译与分发的二进制文件目录
+---refs                 引用的第三方项目源文件目录
+---src                  项目源文件目录
|   +---backend          后端程序源文件目录
|   \---frontend         前端程序源文件目录
\---tools                构建相关的工具目录
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
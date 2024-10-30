#!/bin/bash

# 检查是否提供了 URL 参数
if [ -z "$1" ]; then
    echo "Usage: $0 <url>"
    exit 1
fi

# 获取外部传入的 URL 参数
URL="$1"

# 初始化返回值和时间限制
response=""
start_time=$(date +%s)
time_limit=600  # 10分钟的秒数

# 循环检查 API 返回值
while [ "$response" != "1" ]; do
    # 等待一段时间再进行下一次检查，避免频繁请求
    sleep 1

    # 使用 curl 请求 URL，并捕获返回值，忽略 SSL 证书错误
    response=$(curl -sk "$URL")

    # 打印返回值 (可选)
    echo "$1: $response"

    # 检查时间是否超过限制
    current_time=$(date +%s)
    elapsed_time=$((current_time - start_time))

    if [ "$elapsed_time" -ge "$time_limit" ]; then
        echo "Time limit exceeded. Continuing with the script..."
        break
    fi
done

# 无论是因为返回值为 "1" 还是超时，继续执行后续脚本
echo "Continuing with the script..."
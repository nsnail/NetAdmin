using NSExt.Extensions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace NetAdmin.Infrastructure.Utils;

/// <summary>
///     验证码图片工具类
/// </summary>
public static class CaptchaImageHelper
{
    /// <summary>
    ///     创建一个缺口滑块验证码图片
    /// </summary>
    /// <param name="bgPath">背景图路径</param>
    /// <param name="tempPath">滑块模板小图路径</param>
    /// <param name="bgIndexScope">背景图随机序号范围（1-x）</param>
    /// <param name="tempIndexScope">模板图随机序号范围（1-x）</param>
    /// <param name="sliderSize">滑块尺寸</param>
    /// <returns> 背景图（base64），滑块图（base64），缺口坐标 </returns>
    #pragma warning disable SA1414
    #pragma warning disable CodeLinesAnalyzer
    public static async Task<(string BackgroundImage, string SliderImage, Point OffsetSaw)> CreateSawSliderImage(
            string bgPath, string tempPath, (int, int) bgIndexScope, (int, int) tempIndexScope, Size sliderSize)
        #pragma warning restore SA1414
    {
        // 底图
        using var backgroundImage
            = await Image.LoadAsync<Rgba32>($"{bgPath}/{new[] { bgIndexScope.Item1, bgIndexScope.Item2 }.Rand()}.jpg");

        // 深色模板图
        var templateIndex = new[] { tempIndexScope.Item1, tempIndexScope.Item2 }.Rand();

        using var darkTemplateImage = await Image.LoadAsync<Rgba32>($@"{tempPath}/{templateIndex}/dark.png");

        // 透明模板图
        using var transparentTemplateImage
            = await Image.LoadAsync<Rgba32>($@"{tempPath}/{templateIndex}/transparent.png");

        // 调整模板图大小
        darkTemplateImage.Mutate(x => { x.Resize(sliderSize); });
        transparentTemplateImage.Mutate(x => { x.Resize(sliderSize); });

        // 新建拼图
        using var blockImage = new Image<Rgba32>(sliderSize.Width, sliderSize.Height);

        // 新建滑块拼图
        using var sliderBlockImage = new Image<Rgba32>(sliderSize.Width, backgroundImage.Height);

        // 随机生成拼图坐标
        var offsetRand = GeneratePoint(backgroundImage.Width, backgroundImage.Height, sliderSize.Width
              ,                                               sliderSize.Height);

        // 根据深色模板图计算轮廓形状
        var blockShape = CalcBlockShape(darkTemplateImage);

        // 生成拼图
        blockImage.Mutate(x => {
            // ReSharper disable once AccessToDisposedClosure
            x.Clip(blockShape, p => p.DrawImage(backgroundImage, new Point(-offsetRand.X, -offsetRand.Y), 1));
        });

        // 拼图叠加透明模板图层
        //  ReSharper disable once AccessToDisposedClosure
        blockImage.Mutate(x => x.DrawImage(transparentTemplateImage, new Point(0, 0), 1));

        // 生成滑块拼图
        //  ReSharper disable once AccessToDisposedClosure
        sliderBlockImage.Mutate(x => x.DrawImage(blockImage, new Point(0, offsetRand.Y), 1));

        var opacity = (float)(new[] { 70, 100 }.Rand() * 0.01);

        // 底图叠加深色模板图
        //  ReSharper disable once AccessToDisposedClosure
        backgroundImage.Mutate(x => x.DrawImage(darkTemplateImage, new Point(offsetRand.X, offsetRand.Y), opacity));

        // 生成干扰图坐标
        var interferencePoint = GenerateInterferencePoint(backgroundImage.Width, backgroundImage.Height
                                                        , sliderSize.Width, sliderSize.Height, offsetRand.X
                                                        , offsetRand.Y);

        // 底图叠加深色干扰模板图
        // ReSharper disable once AccessToDisposedClosure
        backgroundImage.Mutate(x => x.DrawImage(darkTemplateImage, new Point(interferencePoint.X, interferencePoint.Y)
                                              , opacity));
        return (backgroundImage.ToBase64String(PngFormat.Instance), sliderBlockImage.ToBase64String(PngFormat.Instance)
              , offsetRand);
    }
    #pragma warning restore CodeLinesAnalyzer

    private static ComplexPolygon CalcBlockShape(Image<Rgba32> templateDarkImage)
    {
        var temp     = 0;
        var pathList = new List<IPath>();
        templateDarkImage.ProcessPixelRows(accessor => {
            for (var y = 0; y < templateDarkImage.Height; y++) {
                var rowSpan = accessor.GetRowSpan(y);
                for (var x = 0; x < rowSpan.Length; x++) {
                    ref var pixel = ref rowSpan[x];
                    if (pixel.A != 0) {
                        temp = temp switch { 0 => x, _ => temp };
                    }
                    else {
                        if (temp == 0) {
                            continue;
                        }

                        pathList.Add(new RectangularPolygon(temp, y, x - temp, 1));
                        temp = 0;
                    }
                }
            }
        });

        return new ComplexPolygon(new PathCollection(pathList));
    }

    /// <summary>
    ///     随机生成干扰图坐标
    /// </summary>
    private static Point GenerateInterferencePoint(int originalWidth,  int originalHeight, int templateWidth
                                                 , int templateHeight, int blockX,         int blockY)
    {
        var x =

            // 在原扣图右边插入干扰图
            originalWidth - blockX - 5 > templateWidth * 2
                ? GetRandomInt(blockX + templateWidth + 5, originalWidth - templateWidth)
                :

                // 在原扣图左边插入干扰图
                GetRandomInt(100, blockX - templateWidth - 5);

        var y =

            // 在原扣图下边插入干扰图
            originalHeight - blockY - 5 > templateHeight * 2
                ? GetRandomInt(blockY + templateHeight + 5, originalHeight - templateHeight)
                :

                // 在原扣图上边插入干扰图
                GetRandomInt(5, blockY - templateHeight - 5);

        return new Point(x, y);
    }

    /// <summary>
    ///     随机生成拼图坐标
    /// </summary>
    private static Point GeneratePoint(int originalWidth, int originalHeight, int templateWidth, int templateHeight)
    {
        var widthDifference  = originalWidth  - templateWidth;
        var heightDifference = originalHeight - templateHeight;
        var x = widthDifference switch {
                    <= 0 => 5, _ => new[] { 0, originalWidth - templateWidth - 100 }.Rand() + 100
                };

        var y = heightDifference switch { <= 0 => 5, _ => new[] { 0, originalHeight - templateHeight - 5 }.Rand() + 5 };

        return new Point(x, y);
    }

    /// <summary>
    ///     随机范围内数字
    /// </summary>
    private static int GetRandomInt(int startNum, int endNum)
    {
        return (endNum > startNum ? new[] { 0, endNum - startNum }.Rand() : 0) + startNum;
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;
using SixLabors.ImageSharp.Formats;
using SkyDCore.Mathematics;
using SixLabors.Fonts;
using SixLabors.Shapes;
using System.Numerics;

namespace SkyDCore.Drawing
{

    /// <summary>
    /// 绘图辅助类
    /// </summary>
    public static class SkyDCoreDrawingAssist
    {

        /// <summary>
        /// 转换为图像
        /// </summary>
        /// <param name="byteArray">图像数据字节数组</param>
        /// <returns>图像</returns>
        public static Image<Rgba32> ConvertToImage(this byte[] byteArray)
        {
            return Image.Load(byteArray);
        }

        /// <summary>
        /// base64编码的文本转为图像
        /// </summary>
        /// <param name="basestr">base64字符串</param>
        /// <returns>图像</returns>
        public static Image<Rgba32> ConvertBase64StringToImage(string base64String)
        {
            return base64String.ConvertBase64StringToByteArray().ConvertToImage();
        }

        /// <summary>
        /// 转换为图像格式的字节数组
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="encoder">编码器，留空则使用默认的 SixLabors.ImageSharp.Formats.Png.PngEncoder 类输出PNG格式图像</param>
        /// <returns>字节数组</returns>
        public static byte[] ToByteArray<TPixel>(this Image<TPixel> img, IImageEncoder encoder = null) where TPixel : struct, IPixel<TPixel>
        {
            MemoryStream ms = new MemoryStream();
            byte[] imagedata = null;
            img.Save(ms, encoder ?? new SixLabors.ImageSharp.Formats.Png.PngEncoder());
            imagedata = ms.GetBuffer();
            ms.Close();
            return imagedata;
        }

        /// <summary>
        /// 转换为JPEG图像格式的字节数组
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="quality">JPEG编码质量，取值0-100，通常使用75</param>
        /// <returns>字节数组</returns>
        public static byte[] ToByteArray<TPixel>(this Image<TPixel> img, int quality) where TPixel : struct, IPixel<TPixel>
        {
            MemoryStream ms = new MemoryStream();
            byte[] imagedata = null;
            img.SaveAsJpeg(ms, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder() { Quality = quality });
            imagedata = ms.GetBuffer();
            ms.Close();
            return imagedata;
        }

        /// <summary>
        /// 从文件读取图像，不会持续占用文件，原文件可被删改
        /// </summary>
        /// <param name="filePath">图像文件路径</param>
        /// <param name="decoder">解码器，例如SixLabors.ImageSharp.Formats.Png.PngDecoder，留空则自动适配</param>
        /// <returns>图像</returns>
        public static Image<Rgba32> LoadImageFromFile(string filePath, IImageDecoder decoder = null)
        {
            return decoder == null ? Image.Load(filePath) : Image.Load(filePath, decoder);
        }

        /// <summary>
        /// 将图像保存为JPEG格式文件
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="filePath">存储路径</param>
        /// <param name="quality">JPEG编码质量，取值0-100，通常使用75</param>
        public static void SaveAsJpegToFile<TPixel>(this Image<TPixel> img, string filePath, int quality) where TPixel : struct, IPixel<TPixel>
        {
            img.Save(filePath, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder() { Quality = quality });
        }

        /// <summary>
        /// 将图像保存为JPEG格式文件
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="stream">可写入的流</param>
        /// <param name="quality">JPEG编码质量，取值0-100，通常使用75</param>
        public static void SaveAsJpegToStream<TPixel>(this Image<TPixel> img, Stream stream, int quality) where TPixel : struct, IPixel<TPixel>
        {
            img.Save(stream, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder() { Quality = quality });
        }

        /// <summary>
        /// 将图像保存为PNG格式文件
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="filePath">存储路径</param>
        public static void SaveAsPngToFile<TPixel>(this Image<TPixel> img, string filePath) where TPixel : struct, IPixel<TPixel>
        {
            img.Save(filePath, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
        }

        /// <summary>
        /// 将图像保存为PNG格式文件
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="stream">可写入的流</param>
        public static void SaveAsPngToStream<TPixel>(this Image<TPixel> img, Stream stream) where TPixel : struct, IPixel<TPixel>
        {
            img.Save(stream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
        }


        /// <summary>
        /// 根据Exif信息自动旋转
        /// </summary>
        /// <param name="context">图像修改上下文</param>
        /// <returns>图像修改上下文</returns>
        public static IImageProcessingContext<TPixel> AutoRotateByExif<TPixel>(this IImageProcessingContext<TPixel> context) where TPixel : struct, IPixel<TPixel>
        {
            return context.AutoOrient();
        }

        /// <summary>
        /// 根据Exif信息自动旋转
        /// </summary>
        /// <param name="img">图像</param>
        public static void AutoRotateByExif<TPixel>(this Image<TPixel> img) where TPixel : struct, IPixel<TPixel>
        {
            img.Mutate(q => q.AutoRotateByExif());
        }

        /// <summary>
        /// 将图像放大或缩小
        /// </summary>
        /// <param name="context">图像修改上下文</param>
        /// <param name="targetWidth">指定的宽度，当mode为ResizeMode.Pad时，设为0则忽略，而以令一个边为准</param>
        /// <param name="targetHeight">指定的高度，当mode为ResizeMode.Pad时，设为0则忽略，而以令一个边为准</param>
        /// <param name="mode">
        ///     <para>缩放时采用的处理方式：</para>
        ///     <para>BoxPad-缩小时双边长度限制，放大时不放大原图，输出的图像尺寸即为目标尺寸，可能会留空，可通过backgroundColor参数指定空的部分的背景颜色；</para>
        ///     <para>Crop-短边适配，长边裁剪；</para>
        ///     <para>Max-以长边为准进行适配；</para>
        ///     <para>Min-以短边为准进行适配；</para>
        ///     <para>Pad-缩小时类似BoxPad，但如果某边传入的目标值为0则以另一边为准进行适配，放大时会填充至少一个方向到目标尺寸，输出的图像尺寸即为目标尺寸，可能会留空，可通过backgroundColor参数指定空的部分的背景颜色；</para>
        ///     <para>Stretch-拉伸画面到目标尺寸</para>
        /// </param>
        /// <param name="anchor">当画面比图像大时，图像的停靠方位，适用于mode为ResizeMode.Pad或ResizeMode.BoxPad的情况</param>
        /// <param name="backgroundColor">背景颜色，留空则为默认颜色，适用于mode为ResizeMode.Pad或ResizeMode.BoxPad的情况</param>
        /// <returns>图像修改上下文</returns>
        public static IImageProcessingContext<TPixel> Scale<TPixel>(this IImageProcessingContext<TPixel> context, int targetWidth, int targetHeight, ResizeMode mode = ResizeMode.Pad, AnchorPositionMode anchor = AnchorPositionMode.Center, TPixel? backgroundColor = null) where TPixel : struct, IPixel<TPixel>
        {
            context.Resize(new ResizeOptions { Mode = mode, Position = anchor, Size = new SixLabors.Primitives.Size { Width = targetWidth, Height = targetHeight } });
            if (backgroundColor != null) context.BackgroundColor(backgroundColor.Value);
            return context;
        }

        /// <summary>
        /// 将图像放大或缩小
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="targetWidth">指定的宽度，当mode为ResizeMode.Pad时，设为0则忽略，而以令一个边为准</param>
        /// <param name="targetHeight">指定的高度，当mode为ResizeMode.Pad时，设为0则忽略，而以令一个边为准</param>
        /// <param name="mode">
        ///     <para>缩放时采用的处理方式：</para>
        ///     <para>BoxPad-缩小时双边长度限制，放大时不放大原图，输出的图像尺寸即为目标尺寸，可能会留空，可通过backgroundColor参数指定空的部分的背景颜色；</para>
        ///     <para>Crop-短边适配，长边裁剪；</para>
        ///     <para>Max-以长边为准进行适配；</para>
        ///     <para>Min-以短边为准进行适配；</para>
        ///     <para>Pad-缩小时类似BoxPad，但如果某边传入的目标值为0则以另一边为准进行适配，放大时会填充至少一个方向到目标尺寸，输出的图像尺寸即为目标尺寸，可能会留空，可通过backgroundColor参数指定空的部分的背景颜色；</para>
        ///     <para>Stretch-拉伸画面到目标尺寸</para>
        /// </param>
        /// <param name="anchor">当画面比图像大时，图像的停靠方位，适用于mode为ResizeMode.Pad或ResizeMode.BoxPad的情况</param>
        /// <param name="backgroundColor">背景颜色，留空则为默认颜色，适用于mode为ResizeMode.Pad或ResizeMode.BoxPad的情况</param>
        public static void Scale<TPixel>(this Image<TPixel> img, int targetWidth, int targetHeight, ResizeMode mode = ResizeMode.Pad, AnchorPositionMode anchor = AnchorPositionMode.Center, TPixel? backgroundColor = null) where TPixel : struct, IPixel<TPixel>
        {
            img.Mutate(q => q.Scale(targetWidth, targetHeight, mode, anchor, backgroundColor));
        }

        /// <summary>
        /// 横向拼接多个图片，组成一个大图
        /// </summary>
        /// <param name="targetHeight">输出图片的高度</param>
        /// <param name="imageDistance">图像之间的距离</param>
        /// <param name="edgeDistance">图像与画框的边距</param>
        /// <param name="backgroundColor">背景颜色</param>
        /// <param name="imageFilePathArray">待拼接图像文件路径数组</param>
        /// <returns>拼接好的图片</returns>
        public static Image<Rgba32> HorizontalSpliceImages(int targetHeight, int imageDistance, int edgeDistance, Rgba32 backgroundColor, params string[] imageFilePathArray)
        {
            Image<Rgba32> fullImg = null;
            for (int i = 0; i < imageFilePathArray.Length; i++)
            {
                using (var img = Image.Load(imageFilePathArray[i]))
                {
                    if (i == 0)
                    {
                        img.Mutate(t => t.AutoRotateByExif().Scale(0, targetHeight - edgeDistance * 2, ResizeMode.Pad, AnchorPositionMode.Center));
                        fullImg = new Image<Rgba32>(Configuration.Default, img.Width + edgeDistance * 2, targetHeight, backgroundColor);
                        fullImg.Mutate(q => q.DrawImage(new GraphicsOptions { Antialias = true }, img, new SixLabors.Primitives.Point(edgeDistance, edgeDistance)));
                    }
                    else
                    {
                        img.Mutate(t => t.AutoRotateByExif().Scale(0, targetHeight - edgeDistance * 2, ResizeMode.Pad, AnchorPositionMode.Center));
                        fullImg.Mutate(q =>
                        {
                            var w = fullImg.Width;
                            q.Scale(fullImg.Width + imageDistance + img.Width, targetHeight, ResizeMode.BoxPad, AnchorPositionMode.Left, backgroundColor);
                            q.DrawImage(new GraphicsOptions { Antialias = true }, img, new SixLabors.Primitives.Point(w - edgeDistance + imageDistance, edgeDistance));
                        });
                    }
                }
            }
            return fullImg;
        }

        /// <summary>
        /// 纵向拼接多个图片，组成一个大图
        /// </summary>
        /// <param name="targetWidth">输出图片的宽度</param>
        /// <param name="imageDistance">图像之间的距离</param>
        /// <param name="edgeDistance">图像与画框的边距</param>
        /// <param name="backgroundColor">背景颜色</param>
        /// <param name="imageFilePathArray">待拼接图像文件路径数组</param>
        /// <returns>拼接好的图片</returns>
        public static Image<Rgba32> VerticalSpliceImages(int targetWidth, int imageDistance, int edgeDistance, Rgba32 backgroundColor, params string[] imageFilePathArray)
        {
            Image<Rgba32> fullImg = null;
            for (int i = 0; i < imageFilePathArray.Length; i++)
            {
                using (var img = Image.Load(imageFilePathArray[i]))
                {
                    if (i == 0)
                    {
                        img.Mutate(t => t.AutoRotateByExif().Scale(targetWidth - edgeDistance * 2, 0, ResizeMode.Pad, AnchorPositionMode.Center));
                        fullImg = new Image<Rgba32>(Configuration.Default, targetWidth, img.Height + edgeDistance * 2, backgroundColor);
                        fullImg.Mutate(q => q.DrawImage(new GraphicsOptions { Antialias = true }, img, new SixLabors.Primitives.Point(edgeDistance, edgeDistance)));
                    }
                    else
                    {
                        img.Mutate(t => t.AutoRotateByExif().Scale(targetWidth - edgeDistance * 2, 0, ResizeMode.Pad, AnchorPositionMode.Center));
                        fullImg.Mutate(q =>
                        {
                            var h = fullImg.Height;
                            q.Scale(targetWidth, fullImg.Height + imageDistance + img.Height, ResizeMode.BoxPad, AnchorPositionMode.Top);
                            q.DrawImage(new GraphicsOptions { Antialias = true }, img, new SixLabors.Primitives.Point(edgeDistance, h - edgeDistance + imageDistance));
                            q.BackgroundColor(backgroundColor);
                        });
                    }
                }
            }
            return fullImg;
        }

        /// <summary>
        /// 根据位置剪裁
        /// </summary>
        /// <param name="context">图像修改上下文</param>
        /// <param name="targetWidth">目标宽度</param>
        /// <param name="targetHeight">目标高度</param>
        /// <param name="anchor">位置锚点</param>
        /// <param name="anchorEdgeOffset">锚点边缘偏移量</param>
        /// <returns>图像修改上下文</returns>
        public static IImageProcessingContext<TPixel> CropByAnchorPosition<TPixel>(this IImageProcessingContext<TPixel> context, int targetWidth, int targetHeight, AnchorPositionMode anchor, int anchorEdgeOffset) where TPixel : struct, IPixel<TPixel>
        {
            var size = context.GetCurrentSize();
            var imageWidth = size.Width;
            var imageHeight = size.Height;
            int x = anchorEdgeOffset;
            int y = anchorEdgeOffset;
            switch (anchor)
            {
                case AnchorPositionMode.Center:
                    x = imageWidth / 2 - targetWidth / 2;
                    y = imageHeight / 2 - targetHeight / 2;
                    break;
                case AnchorPositionMode.Top:
                    x = imageWidth / 2 - targetWidth / 2;
                    break;
                case AnchorPositionMode.Bottom:
                    x = imageWidth / 2 - targetWidth / 2;
                    y = imageHeight - targetHeight - anchorEdgeOffset;
                    break;
                case AnchorPositionMode.Left:
                    y = imageHeight / 2 - targetHeight / 2;
                    break;
                case AnchorPositionMode.Right:
                    x = imageWidth - targetWidth - anchorEdgeOffset;
                    y = imageHeight / 2 - targetHeight / 2;
                    break;
                case AnchorPositionMode.TopLeft:
                    break;
                case AnchorPositionMode.TopRight:
                    x = imageWidth - targetWidth - anchorEdgeOffset;
                    break;
                case AnchorPositionMode.BottomRight:
                    x = imageWidth - targetWidth - anchorEdgeOffset;
                    y = imageHeight - targetHeight - anchorEdgeOffset;
                    break;
                case AnchorPositionMode.BottomLeft:
                    y = imageHeight - targetHeight - anchorEdgeOffset;
                    break;
                default:
                    break;
            }
            var rect = new SixLabors.Primitives.Rectangle(x, y, targetWidth, targetHeight);
            context.Crop(rect);
            return context;
        }

        /// <summary>
        /// 根据位置剪裁
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="targetWidth">目标宽度</param>
        /// <param name="targetHeight">目标高度</param>
        /// <param name="anchor">位置锚点</param>
        /// <param name="anchorEdgeOffset">锚点边缘偏移量</param>
        public static void CropByAnchorPosition<TPixel>(this Image<TPixel> img, int targetWidth, int targetHeight, AnchorPositionMode anchor, int anchorEdgeOffset) where TPixel : struct, IPixel<TPixel>
        {
            img.Mutate(q => q.CropByAnchorPosition(targetWidth, targetHeight, anchor, anchorEdgeOffset));
        }

        /// <summary>
        /// 居中剪裁图像为小于或等于指定尺寸的最邻近的符合比例的尺寸，比如300*201将会返回300*200，因其符合3:2的比例。
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="horizontalMaxRatioValue">最大的比例值</param>
        /// <param name="verticalMaxRatioValue">最大的比例值</param>
        /// <param name="maxRatioValueProduct">纵横比例值的最大乘积</param>
        /// <returns>剪裁后的图像</returns>
        public static void CropToApproximateRatioSize<TPixel>(this Image<TPixel> img, int horizontalMaxRatioValue, int verticalMaxRatioValue, int maxRatioValueProduct) where TPixel : struct, IPixel<TPixel>
        {
            var size = CalculateApproximateRatioSize(img.Width, img.Height, horizontalMaxRatioValue, verticalMaxRatioValue, maxRatioValueProduct);
            img.CropByAnchorPosition(size.Width, size.Height, AnchorPositionMode.Center, 0);
        }


        /// <summary>
        /// 居中剪裁图像为指定比例
        /// </summary>
        /// <param name="context">图像修改上下文</param>
        /// <param name="horizontalRatioValue">横向比例</param>
        /// <param name="verticalRatioValue">纵向比例</param>
        /// <returns>图像修改上下文</returns>
        public static IImageProcessingContext<TPixel> CropToRatioSize<TPixel>(this IImageProcessingContext<TPixel> context, int horizontalRatioValue, int verticalRatioValue) where TPixel : struct, IPixel<TPixel>
        {
            var size = context.GetCurrentSize();
            var w = size.Width;
            var h = (size.Width * 1.0 / horizontalRatioValue * verticalRatioValue).FloorToInt();
            if (h > size.Height)
            {
                w = (size.Height * 1.0 / verticalRatioValue * horizontalRatioValue).FloorToInt();
                h = size.Height;
            }
            context.CropByAnchorPosition(w, h, AnchorPositionMode.Center, 0);
            return context;
        }

        /// <summary>
        /// 居中剪裁图像为指定比例
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="horizontalRatioValue">横向比例</param>
        /// <param name="verticalRatioValue">纵向比例</param>
        public static void CropToRatioSize<TPixel>(this Image<TPixel> img, int horizontalRatioValue, int verticalRatioValue) where TPixel : struct, IPixel<TPixel>
        {
            img.Mutate(q => q.CropToRatioSize(horizontalRatioValue, verticalRatioValue));
        }

        /// <summary>
        /// 为图像添加图像水印
        /// </summary>
        /// <param name="context">图像修改上下文</param>
        /// <param name="watermarkImage">水印图像</param>
        /// <param name="blendMode">混合类型</param>
        /// <param name="blendPercentage">混合百分比，约等于不透明度，取值范围为0到1之间</param>
        /// <param name="anchor">相对于源图像的方位锚点</param>
        /// <param name="horizontalEdgeOffset">左侧或右侧的边距</param>
        /// <param name="verticalEdgeOffset">上方或下方的边距</param>
        /// <returns>图像修改上下文</returns>
        public static IImageProcessingContext<TPixel> AddWatermarkImage<TPixel>(this IImageProcessingContext<TPixel> context, Image<TPixel> watermarkImage, AnchorPositionMode anchor, int horizontalEdgeOffset, int verticalEdgeOffset, PixelBlenderMode blendMode = PixelBlenderMode.Normal, float blendPercentage = 1) where TPixel : struct, IPixel<TPixel>
        {
            var size = context.GetCurrentSize();
            var x = horizontalEdgeOffset;
            var y = verticalEdgeOffset;
            switch (anchor)
            {
                case AnchorPositionMode.Center:
                    x = size.Width / 2 - watermarkImage.Width / 2;
                    y = size.Height / 2 - watermarkImage.Height / 2;
                    break;
                case AnchorPositionMode.Top:
                    x = size.Width / 2 - watermarkImage.Width / 2;
                    break;
                case AnchorPositionMode.Bottom:
                    x = size.Width / 2 - watermarkImage.Width / 2;
                    y = size.Height - verticalEdgeOffset - watermarkImage.Height;
                    break;
                case AnchorPositionMode.Left:
                    y = size.Height / 2 - watermarkImage.Height / 2;
                    break;
                case AnchorPositionMode.Right:
                    x = size.Width - horizontalEdgeOffset - watermarkImage.Width;
                    y = size.Height / 2 - watermarkImage.Height / 2;
                    break;
                case AnchorPositionMode.TopLeft:
                    break;
                case AnchorPositionMode.TopRight:
                    x = size.Width - horizontalEdgeOffset - watermarkImage.Width;
                    break;
                case AnchorPositionMode.BottomRight:
                    x = size.Width - horizontalEdgeOffset - watermarkImage.Width;
                    y = size.Height - verticalEdgeOffset - watermarkImage.Height;
                    break;
                case AnchorPositionMode.BottomLeft:
                    y = size.Height - verticalEdgeOffset - watermarkImage.Height;
                    break;
                default:
                    break;
            }
            context.DrawImage(new GraphicsOptions(true) { BlendPercentage = blendPercentage, BlenderMode = blendMode }, watermarkImage, new SixLabors.Primitives.Point(x, y));
            return context;
        }

        /// <summary>
        /// 为图像添加图像水印
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="watermarkImage">水印图像</param>
        /// <param name="blendMode">混合类型</param>
        /// <param name="blendPercentage">混合百分比，约等于不透明度，取值范围为0到1之间</param>
        /// <param name="anchor">相对于源图像的方位锚点</param>
        /// <param name="horizontalEdgeOffset">左侧或右侧的边距</param>
        /// <param name="verticalEdgeOffset">上方或下方的边距</param>
        public static void AddWatermarkImage<TPixel>(this Image<TPixel> img, Image<TPixel> watermarkImage, AnchorPositionMode anchor, int horizontalEdgeOffset, int verticalEdgeOffset, PixelBlenderMode blendMode = PixelBlenderMode.Normal, float blendPercentage = 1) where TPixel : struct, IPixel<TPixel>
        {
            img.Mutate(q => q.AddWatermarkImage(watermarkImage, anchor, horizontalEdgeOffset, verticalEdgeOffset, blendMode, blendPercentage));
        }

        /// <summary>
        /// 为图像添加文字水印
        /// </summary>
        /// <param name="context">图像修改上下文</param>
        /// <param name="watermarkImage">水印图像</param>
        /// <param name="text">文字内容，注意，可能尚不支持中文等双字节文字，会报错：cmap table doesn't support 32-bit characters yet.</param>
        /// <param name="font">字体，目前似乎只支持ttf格式的字体，定义方法详见注释中的example节点</param>
        /// <param name="color">文字颜色</param>
        /// <param name="blendMode">混合类型</param>
        /// <param name="blendPercentage">混合百分比，约等于不透明度，取值范围为0到1之间</param>
        /// <param name="anchor">相对于源图像的方位锚点</param>
        /// <param name="horizontalEdgeOffset">左侧或右侧的边距</param>
        /// <param name="verticalEdgeOffset">上方或下方的边距</param>
        /// <example>
        /// 调用范例：
        /// <code>
        /// image.Mutate(q =>
        /// {
        ///     q.AutoOrient();
        ///     //使用程序所在目录下的FZYTK.TTF作为字体
        ///     var fontfamily = new FontCollection().Install(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "FZYTK.TTF"));
        ///     var font = new Font(fontfamily, 32);
        ///     q.AddWatermarkText("DrawTextDrawText111111DrawTextDrawTextDrawText", font, Rgba32.Blue, AnchorPositionMode.TopRight, 50, 100, PixelBlenderMode.Add, 0.8f);
        /// });
        /// </code>
        /// </example>
        /// <returns>图像修改上下文</returns>
        public static IImageProcessingContext<TPixel> AddWatermarkText<TPixel>(this IImageProcessingContext<TPixel> context, string text, Font font, TPixel color, AnchorPositionMode anchor, int horizontalEdgeOffset, int verticalEdgeOffset, PixelBlenderMode blendMode = PixelBlenderMode.Normal, float blendPercentage = 1) where TPixel : struct, IPixel<TPixel>
        {
            var size = context.GetCurrentSize();
            var x = horizontalEdgeOffset;
            var y = verticalEdgeOffset;
            var options = new TextGraphicsOptions(true)
            {
                BlenderMode = blendMode,
                BlendPercentage = blendPercentage,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left
            };
            switch (anchor)
            {
                case AnchorPositionMode.Center:
                    x = size.Width / 2;
                    y = size.Height / 2;
                    options.HorizontalAlignment = HorizontalAlignment.Center;
                    options.VerticalAlignment = VerticalAlignment.Center;
                    break;
                case AnchorPositionMode.Top:
                    x = size.Width / 2;
                    options.HorizontalAlignment = HorizontalAlignment.Center;
                    break;
                case AnchorPositionMode.Bottom:
                    x = size.Width / 2;
                    y = size.Height - verticalEdgeOffset;
                    options.HorizontalAlignment = HorizontalAlignment.Center;
                    options.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                case AnchorPositionMode.Left:
                    y = size.Height / 2;
                    options.VerticalAlignment = VerticalAlignment.Center;
                    break;
                case AnchorPositionMode.Right:
                    x = size.Width - horizontalEdgeOffset;
                    y = size.Height / 2;
                    options.HorizontalAlignment = HorizontalAlignment.Right;
                    options.VerticalAlignment = VerticalAlignment.Center;
                    break;
                case AnchorPositionMode.TopLeft:
                    break;
                case AnchorPositionMode.TopRight:
                    x = size.Width - horizontalEdgeOffset;
                    options.HorizontalAlignment = HorizontalAlignment.Right;
                    break;
                case AnchorPositionMode.BottomRight:
                    x = size.Width - horizontalEdgeOffset;
                    y = size.Height - verticalEdgeOffset;
                    options.HorizontalAlignment = HorizontalAlignment.Right;
                    options.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                case AnchorPositionMode.BottomLeft:
                    y = size.Height - verticalEdgeOffset;
                    options.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
                default:
                    break;
            }
            context.DrawText(options, text, font, color, new SixLabors.Primitives.PointF(x, y));
            return context;
        }

        /// <summary>
        /// 为图像添加文字水印
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="watermarkImage">水印图像</param>
        /// <param name="text">文字内容，注意，可能尚不支持中文等双字节文字，会报错：cmap table doesn't support 32-bit characters yet.</param>
        /// <param name="font">字体，目前似乎只支持ttf格式的字体，定义方法详见注释中的example节点</param>
        /// <param name="color">文字颜色</param>
        /// <param name="blendMode">混合类型</param>
        /// <param name="blendPercentage">混合百分比，约等于不透明度，取值范围为0到1之间</param>
        /// <param name="anchor">相对于源图像的方位锚点</param>
        /// <param name="horizontalEdgeOffset">左侧或右侧的边距</param>
        /// <param name="verticalEdgeOffset">上方或下方的边距</param>
        /// <example>
        /// 调用范例：
        /// <code>
        /// image.Mutate(q =>
        /// {
        ///     q.AutoOrient();
        ///     //使用程序所在目录下的FZYTK.TTF作为字体
        ///     var fontfamily = new FontCollection().Install(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "FZYTK.TTF"));
        ///     var font = new Font(fontfamily, 32);
        ///     q.AddWatermarkText("DrawTextDrawText111111DrawTextDrawTextDrawText", font, Rgba32.Blue, AnchorPositionMode.TopRight, 50, 100, PixelBlenderMode.Add, 0.8f);
        /// });
        /// </code>
        /// </example>
        public static void AddWatermarkText<TPixel>(this Image<TPixel> img, string text, Font font, TPixel color, AnchorPositionMode anchor, int horizontalEdgeOffset, int verticalEdgeOffset, PixelBlenderMode blendMode = PixelBlenderMode.Normal, float blendPercentage = 1) where TPixel : struct, IPixel<TPixel>
        {
            img.Mutate(q => q.AddWatermarkText(text, font, color, anchor, horizontalEdgeOffset, verticalEdgeOffset, blendMode, blendPercentage));
        }

        /// <summary>
        /// 验证Web文件格式是否为常用图片格式
        /// </summary>
        public static bool ValidationImageFormat(string MIME)
        {
            string[] normalFormat = { "jpg", "jpeg", "jpe", "bmp", "png", "gif" };
            bool b = false;
            foreach (string f in normalFormat)
            {
                if (MIME.IndexOf(f) >= 0) b = true;
            }
            return b;
        }

        static Random rand = new Random();

        /// <summary>
        /// 生成验证码图像
        /// </summary>
        /// <param name="code">验证码，注意，可能尚不支持中文等双字节文字，会报错：cmap table doesn't support 32-bit characters yet.</param>
        /// <param name="font">字体，目前似乎只支持ttf格式的字体，定义方法详见注释中的example节点</param>
        /// <param name="width">图像宽度</param>
        /// <param name="height">图像高度</param>
        /// <returns>生成的图像</returns>
        /// <example>
        /// 调用范例：
        /// <code>
        /// var font = SystemFonts.CreateFont("Arial", 16, FontStyle.Regular);
        /// SkyDCoreDrawingAssist.CreateVerificationCodeImage("1jz19", font, 120, 32).SaveAsJpegToFile("CreateVerificationCode.jpg", 90);
        /// </code>
        /// </example>
        public static Image<Rgb24> CreateVerificationCodeImage(string code, Font font, int width, int height)
        {
            var img = new Image<Rgb24>(Configuration.Default, width, height, new Rgb24(255, 255, 255));
            var options = new GraphicsOptions(true) { BlenderMode = PixelBlenderMode.Darken, BlendPercentage = 0.7f };
            var textOptions = new TextGraphicsOptions(true) { BlenderMode = PixelBlenderMode.Darken, BlendPercentage = 0.7f, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            img.Mutate(q =>
            {
                for (int i = 0; i < rand.Next(3, 6); i++)
                {
                    q.DrawBeziers(
                        options,
                        new Rgb24((byte)rand.Next(128, 255), (byte)rand.Next(128, 255), (byte)rand.Next(128, 255)),
                        (float)(rand.NextDouble() * 3),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height)),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height)),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height)),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height))
                    );
                }
                for (int i = 0; i < rand.Next(3, 6); i++)
                {
                    q.DrawLines(
                        options,
                        new Rgb24((byte)rand.Next(128, 255), (byte)rand.Next(128, 255), (byte)rand.Next(128, 255)),
                        (float)(rand.NextDouble() * 3),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height)),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height)),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height)),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height))
                    );
                }
                for (int i = 0; i < code.Length; i++)
                {
                    q.DrawText(
                        textOptions,
                        code[i].ToString(),
                        font,
                        new Rgb24((byte)rand.Next(0, 128), (byte)rand.Next(0, 128), (byte)rand.Next(0, 128)),
                        new SixLabors.Primitives.PointF((i + 1.5f) * (width * 1.0f / (code.Length + 2)), rand.Next(font.Size.RoundToInt() / 2, height - font.Size.RoundToInt() / 2))
                        );
                }
                for (int i = 0; i < rand.Next(1, 3); i++)
                {
                    q.DrawBeziers(
                        options,
                        new Rgb24((byte)rand.Next(128, 255), (byte)rand.Next(128, 255), (byte)rand.Next(128, 255)),
                        (float)(rand.NextDouble() * 3),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height)),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height)),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height)),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height))
                    );
                }
                for (int i = 0; i < rand.Next(1, 3); i++)
                {
                    q.DrawLines(
                        options,
                        new Rgb24((byte)rand.Next(128, 255), (byte)rand.Next(128, 255), (byte)rand.Next(128, 255)),
                        (float)(rand.NextDouble() * 3),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height)),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height)),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height)),
                        new SixLabors.Primitives.PointF((float)(rand.NextDouble() * width), (float)(rand.NextDouble() * height))
                    );
                }
            });
            return img;
        }

        /// <summary>
        /// 验证矩形区域中是否包含指定坐标点
        /// </summary>
        /// <param name="rect">矩形</param>
        /// <param name="point">坐标点</param>
        /// <returns>是否包含</returns>
        public static bool VerificationIsIncludePoint(this System.Drawing.Rectangle rect, System.Drawing.Point point)
        {
            return point.X >= rect.Left && point.X <= rect.Right && point.Y >= rect.Top && point.Y <= rect.Bottom;
        }

        /// <summary>
        /// 求最大公约数
        /// </summary>
        /// <returns>最大公约数</returns>
        private static int CalculateGCD(int a, int b)
        {
            if (a % b == 0)
            {
                return b;
            }
            return CalculateGCD(b, a % b);
        }

        /// <summary>
        /// 求宽高比例
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <returns>宽高比例</returns>
        public static SixLabors.Primitives.Size CalculateRatio(int width, int height)
        {
            var n = CalculateGCD(width, height);
            return new SixLabors.Primitives.Size(width / n, height / n);
        }

        /// <summary>
        /// 求宽高比例
        /// </summary>
        /// <param name="img">源图像</param>
        /// <returns>宽高比例</returns>
        public static SixLabors.Primitives.Size CalculateRatio<TPixel>(this Image<TPixel> img) where TPixel : struct, IPixel<TPixel>
        {
            return CalculateRatio(img.Width, img.Height);
        }

        /// <summary>
        /// 计算指定尺寸的比例，如果不合比例（超出最大比值）则返回null
        /// </summary>
        /// <param name="width">指定宽度</param>
        /// <param name="height">指定高度</param>
        /// <param name="maxRatioValue">最大的比例值</param>
        /// <returns>比例</returns>
        public static SixLabors.Primitives.Size? CalculateRatio(int width, int height, int maxRatioValue)
        {
            for (int i = 1; i <= maxRatioValue; i++)
            {
                for (int j = 1; j <= maxRatioValue; j++)
                {
                    if (width * 1.00 / i == height * 1.00 / j)
                    {
                        return new SixLabors.Primitives.Size(i, j);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 计算图像的比例，如果不合比例（超出最大比值）则返回null
        /// </summary>
        /// <param name="图像">源图像</param>
        /// <param name="最大比值">最大的比例值</param>
        /// <returns>比例</returns>
        public static SixLabors.Primitives.Size? CalculateRatio<TPixel>(this Image<TPixel> img, int maxRatioValue) where TPixel : struct, IPixel<TPixel>
        {
            return CalculateRatio(img.Width, img.Height, maxRatioValue);
        }

        /// <summary>
        /// 计算小于或等于指定尺寸的最邻近的符合比例的尺寸，比如300*201将会返回300*200，因其符合3:2的比例。
        /// </summary>
        /// <param name="width">指定宽度</param>
        /// <param name="height">指定高度</param>
        /// <param name="horizontalMaxRatioValue">最大的比例值</param>
        /// <param name="verticalMaxRatioValue">最大的比例值</param>
        /// <param name="maxRatioValueProduct">纵横比例值的最大乘积</param>
        /// <returns>邻近比例尺寸</returns>
        public static SixLabors.Primitives.Size CalculateApproximateRatioSize(int width, int height, int horizontalMaxRatioValue, int verticalMaxRatioValue, int maxRatioValueProduct)
        {
            int offset = Int32.MaxValue;
            int ow = 0;
            int oh = 0;


            for (int w = 1; w <= horizontalMaxRatioValue; w++)
            {
                for (int h = 1; h <= verticalMaxRatioValue; h++)
                {
                    if (w * h > maxRatioValueProduct) continue;
                    for (int i = 1; i <= Math.Min(width, height); i++)
                    {
                        var tw = w * i;
                        if (tw > width) continue;
                        var th = h * i;
                        if (th > height) continue;
                        var r = (width - tw) + (height - th);
                        if (r < offset)
                        {
                            offset = r;
                            ow = tw;
                            oh = th;
                        }
                    }
                }
            }

            return new SixLabors.Primitives.Size(ow, oh);
        }

        /// <summary>
        /// 应用圆角
        /// </summary>
        /// <param name="img">图像</param>
        /// <param name="cornerRadius">圆角半径</param>
        public static void ApplyRoundedCorners(this Image<Rgba32> img, float cornerRadius)
        {
            img.Mutate(x => x.ApplyRoundedCorners(cornerRadius));
        }

        /// <summary>
        /// 应用圆角
        /// </summary>
        /// <param name="context">图像修改上下文</param>
        /// <param name="cornerRadius">圆角半径</param>
        /// <returns>图像修改上下文</returns>
        public static IImageProcessingContext<Rgba32> ApplyRoundedCorners(this IImageProcessingContext<Rgba32> context, float cornerRadius)
        {
            var size = context.GetCurrentSize();
            IPathCollection corners = BuildCorners(size.Width, size.Height, cornerRadius);
            var graphicOptions = new GraphicsOptions(true) { BlenderMode = PixelBlenderMode.Src };
            context.Fill(graphicOptions, Rgba32.Transparent, corners);
            return context;
        }

        private static IPathCollection BuildCorners(int imageWidth, int imageHeight, float cornerRadius)
        {
            var rect = new RectangularPolygon(-0.5f, -0.5f, cornerRadius, cornerRadius);

            IPath cornerToptLeft = rect.Clip(new EllipsePolygon(cornerRadius - 0.5f, cornerRadius - 0.5f, cornerRadius));

            var center = new Vector2(imageWidth / 2F, imageHeight / 2F);

            float rightPos = imageWidth - cornerToptLeft.Bounds.Width + 1;
            float bottomPos = imageHeight - cornerToptLeft.Bounds.Height + 1;

            IPath cornerTopRight = cornerToptLeft.RotateDegree(90).Translate(rightPos, 0);
            IPath cornerBottomLeft = cornerToptLeft.RotateDegree(-90).Translate(0, bottomPos);
            IPath cornerBottomRight = cornerToptLeft.RotateDegree(180).Translate(rightPos, bottomPos);

            return new PathCollection(cornerToptLeft, cornerBottomLeft, cornerTopRight, cornerBottomRight);
        }
    }
}

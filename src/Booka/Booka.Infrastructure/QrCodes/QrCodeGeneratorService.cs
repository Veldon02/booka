using Booka.Core.Domain;
using Booka.Core.Interfaces.Services;
using Booka.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using QRCoder;
using SkiaSharp;

namespace Booka.Infrastructure.QrCodes;

public class QrCodeGeneratorService : IQrCodeGeneratorService
{
    private readonly QrConfig _qrConfig;

    public QrCodeGeneratorService(IOptions<QrConfig> qrConfig)
    {
        _qrConfig = qrConfig.Value;
    }

    public Stream GenerateWorkplaceBook(int workplaceId, int workplaceNumber)
    {
        var text = $"{workplaceNumber}";

        var qrCodeImage = GenerateQrCode(workplaceId);
        using var qrCodeBitmap = SKBitmap.Decode(new MemoryStream(qrCodeImage));

        using var paint = new SKPaint();
        paint.IsAntialias = true;
        paint.Color = SKColors.Black;

        using var font = new SKFont(SKTypeface.FromFamilyName(
            _qrConfig.FontFamily, 
            SKFontStyleWeight.Bold,
            SKFontStyleWidth.Normal, 
            SKFontStyleSlant.Upright));

        font.Size = _qrConfig.FontSize;

        var actualFontSize = GetActualFontHeight(text, paint, font);

        // Canvas preparations
        var canvasWidth = qrCodeBitmap.Width;
        var canvasHeight = actualFontSize + qrCodeBitmap.Height + _qrConfig.Margin;

        using var surface = SKSurface.Create(new SKImageInfo(canvasWidth, canvasHeight));
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.White);

        // Draw number and QR code
        var textX = canvasWidth / 2f;
        var textY = actualFontSize + _qrConfig.Margin;

        canvas.DrawBitmap(qrCodeBitmap, 0, textY);
        canvas.DrawText(text, textX, textY, SKTextAlign.Center, font, paint);

        return SaveToStream(surface);
    }

    private byte[] GenerateQrCode(int workplaceId)
    {
        using var qrGenerator = new QRCodeGenerator();

        var url = string.Format(_qrConfig.QrScanUrlPattern, workplaceId);
        using var data = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q, requestedVersion: _qrConfig.QrCodeVersion);
        using var qrCode = new PngByteQRCode(data);

        var qrCodeImage = qrCode.GetGraphic(_qrConfig.PixelsPerModule);
        return qrCodeImage;
    }

    private static MemoryStream SaveToStream(SKSurface surface)
    {
        var finalImage = surface.Snapshot().Encode(SKEncodedImageFormat.Png, 100);
        var stream = new MemoryStream(finalImage.ToArray());

        stream.Position = 0;

        return stream;
    }

    private static int GetActualFontHeight(string text, SKPaint paint, SKFont font)
    {
        font.MeasureText(text, out var bounds, paint);
        return (int)Math.Ceiling(bounds.Height);
    }
}
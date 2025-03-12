namespace Booka.Infrastructure.Configuration;

public class QrConfig
{
    public int QrCodeVersion { get; set; }

    public int PixelsPerModule { get; set; }

    public int FontSize { get; set; }

    public int Margin { get; set; }

    public string QrScanUrlPattern { get; set; }
}
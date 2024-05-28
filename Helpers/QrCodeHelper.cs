
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;
namespace MEAL_2024_API.Helpers
{
    public interface IQrCodeHelper
    {
        string GenerateQrCode(string text);
    }
    public class QrCodeHelper:IQrCodeHelper
    {
        public string GenerateQrCode(string text)
        {
            using (var qrGenerator = new QRCodeGenerator())
            using (var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new QRCode(qrCodeData))
            using (var bitmap = qrCode.GetGraphic(20))
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}

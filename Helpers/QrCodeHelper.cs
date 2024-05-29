
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
            var qrString = new Guid() + text;
            return qrString.ToString();
        }
    }
}

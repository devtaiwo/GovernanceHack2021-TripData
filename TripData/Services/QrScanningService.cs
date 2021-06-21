using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TripData.Services;
using Xamarin.Forms;
using ZXing.Mobile;
[assembly: Dependency(typeof(QrScanningService))]
namespace TripData.Services
{
    public class QrScanningService : IQrScanningService
    {
        public async Task<string> ScanAsync()
        {
            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please Wait",
            };

            var scanResult = await scanner.Scan(optionsCustom);
            return scanResult.Text;
        }
      
    }
}

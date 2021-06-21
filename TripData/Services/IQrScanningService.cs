using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TripData.Services
{
    interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}

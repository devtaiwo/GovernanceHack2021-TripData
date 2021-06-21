using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using TripData.Models;
using TripData.Services;
using Xamarin.Forms;

namespace TripData.ViewModels
{
    public class RecentPlaceViewModel : INotifyPropertyChanged
    {
        public ICommand ScanQRCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public AddressMap AddressMaps { get; set; }
        public string FullAddress { get; set; }

        public RecentPlaceViewModel()
            {

            ScanQRCommand = new Command<string>(async (param) =>
            {
                try
                {
                    var scanner = DependencyService.Get<IQrScanningService>();
                    var result = await scanner.ScanAsync();
                    if (result != null)
                    {
                        AddressMaps = new AddressMap()
            
                { Longitude="3.361210", Latitude="6.577410", FullAddress="Sheraton Lagos Hotel, 30 Mobolaji Bank Anthony Way, Maryland 021189, Lagos", TimeStamp=DateTime.Now
                    }             ;
                    
                        AddressMaps.FullAddress = result;
                        FullAddress = AddressMaps.FullAddress;
                    }
                }
                catch (Exception ex)
                {

                    //throw;
                }
            });

        }
    }
}

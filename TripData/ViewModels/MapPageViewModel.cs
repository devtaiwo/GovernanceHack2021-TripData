using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using TrackingSample.Helpers;
using TripData.Helpers;
using TripData.Models;
using TripData.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace TripData.ViewModels
{
    public class MapPageViewModel : INotifyPropertyChanged
    {
        IGoogleMapsApiService googleMapsApi = new GoogleMapsApiService();

        public PageStatusEnum PageStatusEnum { get; set; }

        public ICommand DrawRouteCommand { get; set; }
        public ICommand GetPlacesCommand { get; set; }
        public ICommand GetUserLocationCommand { get; set; }
        public ICommand ChangePageStatusCommand { get; set; }
        public ICommand GetLocationNameCommand { get; set; }
        public ICommand CenterMapCommand { get; set; }
        public ICommand GetPlaceDetailCommand { get; set; }
        public ICommand LoadRouteCommand { get; set; }
        public ICommand CleanPolylineCommand { get; set; }
        public ICommand ChooseLocationCommand { get; set; }

        public ObservableCollection<GooglePlaceAutoCompletePrediction> Places { get; set; }
        public ObservableCollection<GooglePlaceAutoCompletePrediction> RecentPlaces { get; set; }
        public GooglePlaceAutoCompletePrediction RecentPlace1 {get;set;}
        public GooglePlaceAutoCompletePrediction RecentPlace2 { get; set; }
        public ObservableCollection<PriceOption> PriceOptions { get; set; }
        public PriceOption PriceOptionSelected { get; set; }

        public string PickupLocation { get; set; }

        Location OriginCoordinates { get; set; }
        Location DestinationCoordinates { get; set; }

        string _destinationLocation;
        public string DestinationLocation
        {
            get
            {
                return _destinationLocation;
            }
            set
            {
                _destinationLocation = value;
                if (!string.IsNullOrEmpty(_destinationLocation))
                {
                    GetPlacesCommand.Execute(_destinationLocation);
                }
            }
        }

        GooglePlaceAutoCompletePrediction _placeSelected;
        public GooglePlaceAutoCompletePrediction PlaceSelected
        {
            get
            {
                return _placeSelected;
            }
            set
            {
                _placeSelected = value;
                if (_placeSelected != null)
                    GetPlaceDetailCommand.Execute(_placeSelected);
            }
        }

        public MapPageViewModel()
        {
            LoadRouteCommand = new Command(async () => await LoadRoute());
            GetPlaceDetailCommand = new Command<GooglePlaceAutoCompletePrediction>(async (param) => await GetPlacesDetail(param));
            GetPlacesCommand = new Command<string>(async (param) => await GetPlacesByName(param));
            GetUserLocationCommand = new Command(async () => await GetActualUserLocation());
            GetLocationNameCommand = new Command<Position>(async (param) => await GetLocationName(param));
            ChangePageStatusCommand = new Command<PageStatusEnum>((param) =>
              {
                 PageStatusEnum = param;

                  if (PageStatusEnum == PageStatusEnum.Default)
                  {
                      CleanPolylineCommand.Execute(null);
                      GetUserLocationCommand.Execute(null);
                      DestinationLocation = string.Empty;
                  }else if(PageStatusEnum== PageStatusEnum.Searching)
                  {
                      Places = new ObservableCollection<GooglePlaceAutoCompletePrediction>(RecentPlaces);
                  }
              });

            ChooseLocationCommand = new Command<Position>((param) =>
            {
                if(PageStatusEnum== PageStatusEnum.Searching)
                {
                    GetLocationNameCommand.Execute(param);
                }
            });

            FillRecentPlacesList();
            FillPriceOptions();
            GetUserLocationCommand.Execute(null);
        }

        async Task GetActualUserLocation()
        {
            try
            {
                await Task.Yield();
                var request = new GeolocationRequest(GeolocationAccuracy.High,TimeSpan.FromSeconds(5000));
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    OriginCoordinates = location;
                    CenterMapCommand.Execute(location);
                    GetLocationNameCommand.Execute(new Position(location.Latitude, location.Longitude));
                }
            }
            catch (Exception ex)
            {
                await UserDialogs.Instance.AlertAsync("Error", "Unable to get actual location", "Ok");
            }
        }

        //Get place 
        public async Task GetLocationName(Position position)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
                PickupLocation = placemarks?.FirstOrDefault()?.FeatureName;
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public async Task GetPlacesByName(string placeText)
        {
            var places = await googleMapsApi.GetPlaces(placeText);
            var placeResult = places.AutoCompletePlaces;
            if (placeResult != null && placeResult.Count > 0)
            {
                Places = new ObservableCollection<GooglePlaceAutoCompletePrediction>(placeResult);
            }
        }

        public async Task GetPlacesDetail(GooglePlaceAutoCompletePrediction placeA)
        {
            var place = await googleMapsApi.GetPlaceDetails(placeA.PlaceId);
            if (place != null)
            {
                DestinationCoordinates = new Location(place.Latitude, place.Longitude);
                LoadRouteCommand.Execute(null);
                RecentPlaces.Add(placeA);
            }
        }

        public async Task LoadRoute()
        {
            if (OriginCoordinates == null)
                return;

            ChangePageStatusCommand.Execute(PageStatusEnum.ShowingRoute);

            var googleDirection = await googleMapsApi.GetDirections($"{OriginCoordinates.Latitude}", $"{OriginCoordinates.Longitude}", $"{DestinationCoordinates.Latitude}", $"{DestinationCoordinates.Longitude}");
            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
               

                var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));
                DrawRouteCommand.Execute(positions);
            }
            else
            {
                ChangePageStatusCommand.Execute(PageStatusEnum.Default);
                await UserDialogs.Instance.AlertAsync(":(", "No route found", "Ok");
               
            }
        }

        void FillRecentPlacesList()
        {
            RecentPlaces = new ObservableCollection<GooglePlaceAutoCompletePrediction>()
            {
                {new GooglePlaceAutoCompletePrediction(){ PlaceId="ChIJq0wAE_CJr44RtWSsTkp4ZEM", StructuredFormatting=new StructuredFormatting(){ MainText="Ministry Of Transportation", SecondaryText="Oregun Road, Kudirat Abiola Way, Ojota, Lagos" } } },
                {new GooglePlaceAutoCompletePrediction(){ PlaceId="ChIJq0wAE_CJr44RtWSsTkp4ZEM", StructuredFormatting=new StructuredFormatting(){ MainText="Bay Lounge", SecondaryText="10 Admiralty Rd, Lekki Phase 1, Lagos" } } },
                {new GooglePlaceAutoCompletePrediction(){ PlaceId="ChIJm02ImNyJr44RNs73uor8pFU", StructuredFormatting=new StructuredFormatting(){ MainText="Eko Hotels", SecondaryText=" Plot 1415 Adetokunbo Ademola Street, Victoria Island" } } },
            };

            RecentPlace1 = RecentPlaces[0];
            RecentPlace2 = RecentPlaces[1];

        }

        void FillPriceOptions()
        {
            PriceOptions = new ObservableCollection<PriceOption>()
            {
                {new PriceOption(){ Tag="TripDataBus", Category="Bus", CategoryDescription="Lagos BRT", PriceDetails=new System.Collections.Generic.List<PriceDetail>(){
                    { new PriceDetail(){ Type="TripData Bus", Price=332, ArrivalETA="12:20pm", Icon="data:image/png;base64,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" } },
                  { new PriceDetail(){ Type="TripData Coach", Price=150, ArrivalETA="12:45pm", Icon="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSYZqvmOGvsgskiOIv-0on15oL1BFV2gb5GVQ&usqp=CAU" } }}
                 } },
                {new PriceOption(){Tag="TripDataRail", Category="Rail", CategoryDescription="Lagos to Kaduna Railway" ,  PriceDetails=new System.Collections.Generic.List<PriceDetail>(){
                    { new PriceDetail(){ Type="TripData Rail", Price=332, ArrivalETA="12:20pm", Icon="data:image/jpeg;base64,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" } }
                  } } }
            };
            PriceOptionSelected = PriceOptions.First();

        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

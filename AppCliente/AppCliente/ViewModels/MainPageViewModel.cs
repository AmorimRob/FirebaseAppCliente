using AppCliente.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using MvvmHelpers;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCliente.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly string FIREBASE_API_ADDRESS = "api_address";
        private readonly FirebaseClient _firebaseClient;

        private Order _order;

        public Order Order
        {
            get { return _order; }
            set { _order = value; OnPropertyChanged(); }
        }

        public ICommand SendOrderCommand { get; set; }

        public MainPageViewModel()
        {
            Order = new Order();
            _firebaseClient = new FirebaseClient(FIREBASE_API_ADDRESS);

            SendOrderCommand = new Command(async () =>
            {
                    var keyOrder = await _firebaseClient
                             .Child("orders")
                             .PostAsync<Order>(Order);

                if (!string.IsNullOrEmpty(keyOrder.Key))
                    await App.Current.MainPage.DisplayAlert("MonkeyFestLATAM", "Order sent success!", "OK");

                Order = new Order();
            });

            ObservableOrders();
        }

        public void ObservableOrders()
        {
            _firebaseClient
                .Child("orders")
                .AsObservable<Order>()
                .Subscribe(order =>
                {
                    if (order.EventType == FirebaseEventType.InsertOrUpdate)
                    {
                        if (order.Object != null && order.Object.IdSeller != 0)
                            Device.BeginInvokeOnMainThread(async () => 
                                await App.Current.MainPage.DisplayAlert("MonkeyFestLATAM", "The order: " 
                                + order.Object.ProductName + " was accepeted by seller", "OK"));
                    }
                    else if (order.Object != null && order.EventType == FirebaseEventType.Delete)
                    {
                    }
                });
        }
    }
}

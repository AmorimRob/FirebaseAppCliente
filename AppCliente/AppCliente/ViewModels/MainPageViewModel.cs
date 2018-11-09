using AppCliente.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCliente.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly string ENDERECO_FIREBASE = "https://demoapp-2ea27.firebaseio.com/";
        private readonly FirebaseClient _firebaseClient;

        private Pedido _pedido;

        public Pedido Pedido
        {
            get { return _pedido; }
            set { _pedido = value; OnPropertyChanged(); }
        }

        public ICommand EnviarPedidoCommand { get; set; }

        public MainPageViewModel()
        {
            Pedido = new Pedido();
            _firebaseClient = new FirebaseClient(ENDERECO_FIREBASE);

            EnviarPedidoCommand = new Command(async () =>
            {
                    var keyPedido = await _firebaseClient
                             .Child("pedidos")
                             .PostAsync<Pedido>(Pedido);

                if (!string.IsNullOrEmpty(keyPedido.Key))
                    await App.Current.MainPage.DisplayAlert("Xamarin Saturday", "Pedido enviado com sucesso!", "OK");

                Pedido = new Pedido();
            });

            ObservablePedidos();
        }

        public void ObservablePedidos()
        {
            _firebaseClient
                .Child("pedidos")
                .AsObservable<Pedido>()
                .Subscribe(pedido =>
                {
                    if (pedido.EventType == FirebaseEventType.InsertOrUpdate)
                    {
                        if (pedido.Object != null && pedido.Object.IdVendedor != 0)
                            Device.BeginInvokeOnMainThread(async () => 
                                await App.Current.MainPage.DisplayAlert("Xamarin Saturday", "O pedido: " + pedido.Object.Produto + " foi aceito pelo vendedor", "OK"));
                    }
                    else if (pedido.Object != null && pedido.EventType == FirebaseEventType.Delete)
                    {
                    }
                });
        }
    }
}

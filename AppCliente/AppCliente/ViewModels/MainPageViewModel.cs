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
            _firebaseClient = new FirebaseClient(ENDERECO_FIREBASE);

            Pedido = new Pedido();

            EnviarPedidoCommand = new Command(async () =>
            {
                var retorno = string.Empty;

                if (!string.IsNullOrEmpty(retorno))
                    await App.Current.MainPage.DisplayAlert("Xamarin Saturday", "Pedido enviado com sucesso!", "OK");

                Pedido = new Pedido();
            });

            ObservablePedidos();
        }

        public void ObservablePedidos()
        {
            
        }
    }
}

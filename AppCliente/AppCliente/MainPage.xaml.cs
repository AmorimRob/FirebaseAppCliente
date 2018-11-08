using AppCliente.ViewModels;
using Xamarin.Forms;

namespace AppCliente
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }
}

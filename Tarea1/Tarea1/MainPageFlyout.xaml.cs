using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarea1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageFlyout : ContentPage
    {
        public ListView ListView;

        public MainPageFlyout()
        {
            InitializeComponent();
        }

        //private async void OnInicioClicked(object sender, EventArgs e)
        //{
        //    var parent = (FlyoutPage)Parent;
        //    parent.Detail = new NavigationPage(new vistas.Calculadora());
        //    parent.IsPresented = false;
        //}

        //private async void OnAcercaDeClicked(object sender, EventArgs e)
        //{
        //    var parent = (FlyoutPage)Parent;
        //    parent.Detail = new NavigationPage(new vistas.AcercaDePage());
        //    parent.IsPresented = false;
        //}

        //private async void OnIniciarSesionClicked(object sender, EventArgs e)
        //{
        //    var parent = (FlyoutPage)Parent;
        //    parent.Detail = new NavigationPage(new vistas.IniciarSesionPage());
        //    parent.IsPresented = false;
        //}
        private async void btnImagenes(object sender, EventArgs e)
        {
            var parent = (FlyoutPage)Parent;
            parent.Detail = new NavigationPage(new vistas.Imagenes());
            parent.IsPresented = false;
        }
        private async void btnCalculadora(object sender, EventArgs e)
        {
            var parent = (FlyoutPage)Parent;
            parent.Detail = new NavigationPage(new vistas.Calculadora());
            parent.IsPresented = false;
        }
    }
}
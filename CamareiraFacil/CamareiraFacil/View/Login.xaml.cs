using CamareiraFacil.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CamareiraFacil.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        private List<Usuario> listaUsuario;
        public Login()
        {
            InitializeComponent();

            listaUsuario = new List<Usuario>
            {
                new Usuario {Id = 1, Descricao = "Usuario 1" },
                new Usuario {Id = 2, Descricao = "Usuario 2" }
            };

            pckUsuarios.ItemsSource = listaUsuario;
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            Page original = App.Current.MainPage.Navigation.NavigationStack.Last();
            App.Current.MainPage.Navigation.PopAsync().ConfigureAwait(false);
            App.Current.MainPage.Navigation.PushAsync(new Principal());
            App.Current.MainPage.Navigation.RemovePage(original);
        }
    }
}
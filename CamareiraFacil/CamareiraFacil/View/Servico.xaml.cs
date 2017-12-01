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
	public partial class Servico : ContentPage
	{
		public Servico ()
		{
			InitializeComponent ();

            List<String> listaApartamentos = new List<string> { "101", "102", "103", "104", "105" };
            pckApartamentos.ItemsSource = listaApartamentos;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Page original = App.Current.MainPage.Navigation.NavigationStack.Last();
            App.Current.MainPage.Navigation.PopAsync().ConfigureAwait(false);
            App.Current.MainPage.Navigation.PushAsync(new Principal());
            App.Current.MainPage.Navigation.RemovePage(original);
        }
    }
}

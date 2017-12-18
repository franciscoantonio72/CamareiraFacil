using Acr.UserDialogs;
using CamareiraFacil.Model;
using CamareiraFacil.Service;
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
        public LocaisManutencao locais;
        List<LocaisManutencao> listaLocais = new List<LocaisManutencao>();

        public Servico ()
		{
			InitializeComponent ();

            CarregarLocaisManutencao();
        }

        private async void CarregarLocaisManutencao()
        {
            using (var objDialog = UserDialogs.Instance.Loading("Carregando..."))
            {
                await Task.Delay(2000);
            }
            ApiCamareiraFacil api = new ApiCamareiraFacil();
            listaLocais = api.GetLocaisManutencao();
            pckApartamentos.ItemsSource = listaLocais;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (pckApartamentos.SelectedIndex == -1)
            {
                DisplayAlert("Aviso", "Falta informar o local da manutenção!", "OK");
                return;
            }

            DisplayAlert("Informação", "Gravado com sucesso!", "OK");
            Navigation.PopAsync().ConfigureAwait(false);
        }

        private void pckApartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            locais = listaLocais[pckApartamentos.SelectedIndex];
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync().ConfigureAwait(false);
        }
    }
}

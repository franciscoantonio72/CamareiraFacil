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
	public partial class Limpeza : ContentPage
	{
        List<String> listaGravar = new List<string>();

		public Limpeza ()
		{
			InitializeComponent ();

            List<String> listaLimpeza = new List<string>();
            listaLimpeza.Add("Recolher Toalhas");
            listaLimpeza.Add("Recolher Lençois");
            listaLimpeza.Add("Estender Roupas");
            listaLimpeza.Add("Lavar Banheiro");
            listaLimpeza.Add("Secar Tudo");
            listaLimpeza.Add("Repõe Toalhas");
            listaLimpeza.Add("Colocar sabonete mini");
            listaLimpeza.Add("Repor papel higiênico");
            listaLimpeza.Add("Dar descarga");
            listaLimpeza.Add("Varrer chão");
            listaLimpeza.Add("Tirar pó");
            listaLimpeza.Add("Passar pano no chão");
            listaLimpeza.Add("Borrifar Bom Ar");
            listaLimpeza.Add("Colocar saco de lixo");
            listaLimpeza.Add("Ver TV, Som e Ar");

            List<String> listaApartamentos = new List<string> {"101", "102", "103", "104", "105"};
            pckApartamentos.ItemsSource = listaApartamentos;

            lstView.ItemsSource = listaLimpeza; // Enum.GetValues(typeof(DayOfWeek)).OfType<DayOfWeek>().Select(c => c.ToString());
		}

        private void btnIniciar_Clicked(object sender, EventArgs e)
        {
            lstView.IsEnabled = true;
        }

        private void btnFinalizar_Clicked(object sender, EventArgs e)
        {
            Page original = App.Current.MainPage.Navigation.NavigationStack.Last();
            App.Current.MainPage.Navigation.PopAsync().ConfigureAwait(false);
            App.Current.MainPage.Navigation.PushAsync(new Principal());
            App.Current.MainPage.Navigation.RemovePage(original);
        }


        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        private void lstView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            listaGravar.Add(e.Item.ToString());
        }
    }
}

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CamareiraFacil.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Principal : ContentPage
	{
		public Principal ()
		{
			InitializeComponent ();

            limpeza.Clicked += btnLimpeza_Clicked;
            consumofrigobar.Clicked += btnConsumo_Clicked;
            mensagem.Clicked += btnMensagem_Clicked;
            ordemservico.Clicked += btnServico_Clicked;
            mapa.Clicked += Mapa_Clicked;
            configuracao.Clicked += Configuracao_Clicked;
        }

        private void Configuracao_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Configuracao());
        }

        private void Mapa_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MapaOcupacao());
        }

        private void btnServico_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Servico());
        }

        private void btnConsumo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Consumo());
        }

        private void btnLimpeza_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Limpeza());
        }

        private void btnMensagem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Mensagem());
        }
    }
}
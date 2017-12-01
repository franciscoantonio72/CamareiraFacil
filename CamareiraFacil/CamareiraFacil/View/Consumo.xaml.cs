using CamareiraFacil.Model;
using CamareiraFacil.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CamareiraFacil.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Consumo : ContentPage
	{
        private List<ItemPDV> produtos;
        private List<Apartamento> apartamentos;
        public ObservableCollection<ItemLancamento> ListaItens { get; }
        public ItemPDV produto;
        public Apartamento apartamento;

        public Consumo ()
		{
			InitializeComponent ();

            ListaItens = new ObservableCollection<ItemLancamento>();
            lstView.ItemsSource = ListaItens;

            ApiCamareiraFacil api = new ApiCamareiraFacil();
            produtos = api.GetItensPDV("0007");
            pckProdutos.ItemsSource = produtos;

            apartamentos = api.GetApartamentos();
            pckApartamentos.ItemsSource = apartamentos;

            List<String> pontos = new List<string> {"Frigobar"};
            pckPontoVenda.ItemsSource = pontos;
        }

        private void btnAdicionar_Clicked(object sender, EventArgs e)
        {
            ListaItens.Add(new ItemLancamento
                                {Codigo = produto.Codigo,
                                Descricao = produto.Descricao,
                                Quantidade = Convert.ToDouble(edtQuantidade.Text),
                                Cod_Emp = "001",
                                Codigo_Apto = apartamento.NApto,
                                Codigo_PDV = "0007",
                                Operador = "** TEC-SOFT **"
            });
        }

        private void pckProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            produto = produtos[pckProdutos.SelectedIndex];
        }

        private async void btnOK_Clicked(object sender, EventArgs e)
        {
            ApiCamareiraFacil api = new ApiCamareiraFacil();
            if (!await api.LancaConsumo(ListaItens))
            {
                DisplayAlert("Titulo", "Mensagem Alerta", "OK");
            }
            else
            {
                Page original = App.Current.MainPage.Navigation.NavigationStack.Last();
                App.Current.MainPage.Navigation.PopAsync().ConfigureAwait(false);
                App.Current.MainPage.Navigation.PushAsync(new Principal());
                App.Current.MainPage.Navigation.RemovePage(original);
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Page original = App.Current.MainPage.Navigation.NavigationStack.Last();
            App.Current.MainPage.Navigation.PopAsync().ConfigureAwait(false);
            App.Current.MainPage.Navigation.PushAsync(new Principal());
            App.Current.MainPage.Navigation.RemovePage(original);
        }

        private void pckApartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            apartamento = apartamentos[pckApartamentos.SelectedIndex];
        }
    }
}
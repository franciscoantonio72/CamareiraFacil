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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            using (var objDialog = UserDialogs.Instance.Loading("Aguarde. Gravando dados..."))
            {
                await Task.Delay(2000);
            }

            if (pckApartamentos.SelectedIndex == -1)
            {
                DisplayAlert("Aviso", "Falta informar o local da manutenção!", "OK");
                return;
            }

            OrdemServico ordemServico = new OrdemServico
            {
                Cod_Emp = "001",
                Data_Cad = DateTime.Now,
                Descricao = edtMensagem.Text,
                Hora = DateTime.Now.ToString("HH:mm:ss"),
                Operador = "** MOBILE **",
                Remetente = "** MOBILE **",
                Status = "A",
                Setor = locais.Descricao,
                Cod_LocalManutencao = locais.Codigo
            };

            ApiCamareiraFacil api = new ApiCamareiraFacil();
            if (!await api.GravaOrdemServico(ordemServico))
            {
                DisplayAlert("Alerta", "Não foi possível gravar a Ordem de Serviço", "OK");
            }
            else
            {
                DisplayAlert("Informação", "Gravado com sucesso!", "OK");
                await Navigation.PopAsync().ConfigureAwait(false);
            }
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

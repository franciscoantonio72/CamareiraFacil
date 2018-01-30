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
    public partial class Mensagem : ContentPage
    {
        public List<Funcionario> funcionarios;
        public Funcionario funcionario;

        public Mensagem()
        {
            InitializeComponent();

            CarregarDados();
        }

        private async void CarregarDados()
        {
            using (var objDialog = UserDialogs.Instance.Loading("Carregando..."))
            {
                await Task.Delay(2000);
            }

            ApiCamareiraFacil api = new ApiCamareiraFacil();
            funcionarios = api.GetFuncionarios();
            pckFuncionario.ItemsSource = funcionarios;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Recado recado = new Recado
            {
                Assunto = edtAssunto.Text,
                Cod_Emp = "001",
                Descricao = edtMensagem.Text,
                Destinatario = funcionario.Codigo,
                Data_Cad = DateTime.Now,
                Hora = DateTime.Now.ToString("HH:mm:ss"),
                Remetente = "00001"
            };

            ApiCamareiraFacil api = new ApiCamareiraFacil();
            if (!await api.GravaRecado(recado))
            {
                DisplayAlert("Recado", "Não foi possível gravar recado", "OK");
            }
            else
            {
                await Navigation.PopAsync().ConfigureAwait(false);
            }
        }

        private void pckFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            funcionario = funcionarios[pckFuncionario.SelectedIndex];
        }
    }
}

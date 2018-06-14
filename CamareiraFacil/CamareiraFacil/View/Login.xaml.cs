using CamareiraFacil.Model;
using System;
using System.Linq;
using CamareiraFacil.Service;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace CamareiraFacil.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        AppPreferences app;
        public Login()
        {
            InitializeComponent();

            app = new AppPreferences(Forms.Context);
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            using (var objDialog = UserDialogs.Instance.Loading("Autenticando..."))
            {
                await Task.Delay(2000);
            }

            Funcoes funcoes = new Funcoes();
            string lsSenhaCripto = funcoes.Encrypt(edtSenha.Text.Trim());

            try
            {
                ApiCamareiraFacil apiApto = new ApiCamareiraFacil();
                var funcionarios = apiApto.ValidaSenha(lsSenhaCripto);
                if (funcionarios != null)
                {
                    app.saveAcessKey("USUARIO", funcionarios.FirstOrDefault().Descricao);
                    app.saveAcessKey("CODUSUARIO", funcionarios.FirstOrDefault().Codigo);

                    Page original = App.Current.MainPage.Navigation.NavigationStack.Last();
                    await App.Current.MainPage.Navigation.PopAsync().ConfigureAwait(false);
                    await App.Current.MainPage.Navigation.PushAsync(new Principal());
                    App.Current.MainPage.Navigation.RemovePage(original);
                }
                else
                {
                    await DisplayAlert("Aviso", "Não foi possível validar senha.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", "Não foi possível carregar apartamentos. Erro: " + ex.Message, "OK");
            }
        }

        private void Menu_Clicked(object sender, EventArgs e)
        {
            app.saveAcessKey("CONFIGURADO", "");

            Navigation.PushAsync(new Configuracao());
        }
    }
}
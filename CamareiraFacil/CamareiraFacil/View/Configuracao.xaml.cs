using CamareiraFacil.Model;
using CamareiraFacil.Service;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CamareiraFacil.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Configuracao : ContentPage
    {
        private List<PDv> listaSetores;
        private PDv pdv;
        private AppPreferences appp;
        ApiCamareiraFacil api;

        public Configuracao()
        {
            InitializeComponent();

            api = new ApiCamareiraFacil();

            appp = new AppPreferences(Forms.Context);

            edtIp.Text = (appp.getAcessKey("IP") != "" ? appp.getAcessKey("IP") : "");
            edtPorta.Text = (appp.getAcessKey("PORTA") != "" ? appp.getAcessKey("PORTA") : "");

            if (appp.getAcessKey("SETOR") != "")
            {
                listaSetores = api.GetPDVs();

                foreach (var item in listaSetores)
                {
                    if (appp.getAcessKey("SETOR") == item.Codigo)
                        pckSetor.ItemsSource.IndexOf(item);
                }
            }
        }

        private void pckSetor_SelectedIndexChanged(object sender, EventArgs e)
        {
            pdv = listaSetores[pckSetor.SelectedIndex];
        }

        private void btnGravar_Clicked(object sender, EventArgs e)
        {
            appp.saveAcessKey("IP", edtIp.Text);
            appp.saveAcessKey("PORTA", edtPorta.Text);
            if (pdv == null)
            {
                DisplayAlert("Alerta", "Informe o PDV padrão", "OK");
                ApiCamareiraFacil api_ = new ApiCamareiraFacil();
                listaSetores = api_.GetPDVs();
                pckSetor.ItemsSource = listaSetores;
                return;
            }
            appp.saveAcessKey("SETOR", pdv.Codigo);

            if (appp.getAcessKey("CONFIGURADO") == "")
            {
                appp.saveAcessKey("CONFIGURADO", "SIM");

                Page original = App.Current.MainPage.Navigation.NavigationStack.Last();
                App.Current.MainPage.Navigation.PopAsync().ConfigureAwait(false);
                App.Current.MainPage.Navigation.PushAsync(new Login());
                App.Current.MainPage.Navigation.RemovePage(original);
            }
            else
            {
                Navigation.PopAsync().ConfigureAwait(false);
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync().ConfigureAwait(false);
        }
    }
}
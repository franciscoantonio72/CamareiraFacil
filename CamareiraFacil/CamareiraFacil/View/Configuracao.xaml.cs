using CamareiraFacil.Model;
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
        private List<Setor> listaSetores;
        private Setor setor;
        private AppPreferences appp;

        public Configuracao()
        {
            InitializeComponent();

            listaSetores = new List<Setor>
            {
                new Setor {Codigo="0001", Descricao="Setor 1" },
                new Setor {Codigo="0002", Descricao="Setor 2" }
            };
            pckSetor.ItemsSource = listaSetores;

            appp = new AppPreferences(Forms.Context);

            edtIp.Text = (appp.getAcessKey("IP") != "" ? appp.getAcessKey("IP") : "");
            edtPorta.Text = (appp.getAcessKey("PORTA") != "" ? appp.getAcessKey("PORTA") : "");

            if (appp.getAcessKey("SETOR") != "")
            {
                foreach (var item in listaSetores)
                {
                    if (appp.getAcessKey("SETOR") == item.Codigo)
                        pckSetor.ItemsSource.IndexOf(item);
                }
            }
        }

        private void pckSetor_SelectedIndexChanged(object sender, EventArgs e)
        {
            setor = listaSetores[pckSetor.SelectedIndex];
        }

        private void btnGravar_Clicked(object sender, EventArgs e)
        {
            appp.saveAcessKey("IP", edtIp.Text);
            appp.saveAcessKey("PORTA", edtPorta.Text);
            appp.saveAcessKey("SETOR", setor.Codigo);

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
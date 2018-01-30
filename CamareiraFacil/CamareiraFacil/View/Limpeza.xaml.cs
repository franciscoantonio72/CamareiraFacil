using Acr.UserDialogs;
using CamareiraFacil.Model;
using CamareiraFacil.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections;

namespace CamareiraFacil.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Limpeza : ContentPage
    {
        List<String> listaGravar = new List<string>();
        public Apartamento apartamento;
        List<Apartamento> listaApartamentos;

        public Limpeza()
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

            try
            {
                ApiCamareiraFacil apiApto = new ApiCamareiraFacil();
                listaApartamentos = apiApto.GetApartamentos();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", "Não foi possível carregar apartamentos. Erro: " + ex.Message, "OK");
            }
            pckApartamentos.ItemsSource = listaApartamentos;

            lstView.ItemsSource = RetornarListaLimpeza();
        }

        private List<string> RetornarListaLimpeza()
        {
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
            return listaLimpeza;
        }

        private async void btnIniciar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (pckApartamentos.SelectedIndex == -1)
                {
                    await DisplayAlert("Alerta", "Informe o apartamento.", "OK");
                    return;
                }

                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
                ApiCamareiraFacil api = new ApiCamareiraFacil();
                Faxina faxina = new Faxina
                {
                    Cod_emp = "001",
                    Operador = "** MOBILE **",
                    Historico = "Faxina",
                    Hora = DateTime.Now.ToLongTimeString(),
                    NApto = apartamento.NApto,
                    Hora_Inicial = DateTime.Now.ToLongTimeString(),
                    Cod_Camareira = "00101"
                };

                UserDialogs.Instance.ShowLoading("Gravando...",MaskType.Clear);
                if (! await api.ComecaFaxina(faxina))
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Erro", "Não foi possivel começar faxina", "OK");
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Sucesso", "Gravado com sucesso!", "OK");
                    await Navigation.PopAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Erro", "Ocorreu um erro ao gravar as informações. " + ex.Message, "OK");
            }
        }

        private async void btnFinalizar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (pckApartamentos.SelectedIndex == -1)
                {
                    await DisplayAlert("Alerta", "Informe o apartamento.", "OK");
                    return;
                }

                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("pt-BR");
                ApiCamareiraFacil api = new ApiCamareiraFacil();
                Faxina faxina = new Faxina
                {
                    Cod_emp = "001",
                    Operador = "** MOBILE **",
                    Historico = "Faxina",
                    Hora = DateTime.Now.ToLongTimeString(),
                    NApto = apartamento.NApto,
                    Hora_Final = DateTime.Now.ToLongTimeString(),
                    Cod_Camareira = "00101"
                };

                UserDialogs.Instance.ShowLoading("Gravando...", MaskType.Clear);
                if (!await api.TerminaFaxina(faxina))
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Erro", "Não foi possivel finalizar faxina", "OK");
                }
                else
                {
                    UserDialogs.Instance.HideLoading();
                    await DisplayAlert("Sucesso", "Finalizado com sucesso!", "OK");
                    await Navigation.PopAsync().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DisplayAlert("Erro", "Ocorreu um erro ao gravar as informações. " + ex.Message, "OK");
            }
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

        private void pckApartamentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            apartamento = listaApartamentos[pckApartamentos.SelectedIndex];
        }
    }
}
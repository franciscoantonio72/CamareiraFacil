﻿using CamareiraFacil.Model;
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
                Page original = App.Current.MainPage.Navigation.NavigationStack.Last();
                App.Current.MainPage.Navigation.PopAsync().ConfigureAwait(false);
                App.Current.MainPage.Navigation.PushAsync(new Principal());
                App.Current.MainPage.Navigation.RemovePage(original);
            }
        }

        private void pckFuncionario_SelectedIndexChanged(object sender, EventArgs e)
        {
            funcionario = funcionarios[pckFuncionario.SelectedIndex];
        }
    }
}
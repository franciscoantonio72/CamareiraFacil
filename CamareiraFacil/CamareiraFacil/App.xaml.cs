﻿using CamareiraFacil.Model;
using CamareiraFacil.View;

using Xamarin.Forms;

namespace CamareiraFacil
{
    public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            AppPreferences appp = new AppPreferences(Forms.Context);
            if (appp.getAcessKey("CONFIGURADO") != "")
            {
                MainPage = new NavigationPage(new Login());
            }
            else
            {
                MainPage = new NavigationPage(new Configuracao());
            }
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

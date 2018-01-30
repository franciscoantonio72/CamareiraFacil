using CamareiraFacil.Model;
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
	public partial class Configuracao : ContentPage
	{
        private List<Setor> listaSetores;

        public Configuracao ()
		{
			InitializeComponent ();

            listaSetores = new List<Setor>
            {
                new Setor {Codigo="0001", Descricao="Setor 1" },
                new Setor {Codigo="0002", Descricao="Setor 2" }
            };
            pckSetor.ItemsSource = listaSetores;
		}

        private void pckSetor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
using CamareiraFacil.Model;
using CamareiraFacil.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CamareiraFacil.View
{
    public class MapaOcupacao : ContentPage
    {
        Button button;
        List<Apartamento> listaApartamento;

        public MapaOcupacao()
        {
            ApiCamareiraFacil api = new ApiCamareiraFacil();
            try
            {
                listaApartamento = api.GetApartamentos();
            }
            catch (Exception E)
            {
                DisplayAlert("Erro", "Erro: " + E.Message, "OK");
                return;
            }

            var plainButton = new Style(typeof(Button))
            {
                Setters = {
              new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex ("#eee") },
              new Setter { Property = Button.TextColorProperty, Value = Color.Black },
              new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
              new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button)) }
            }
            };
            var darkerButton = new Style(typeof(Button))
            {
                Setters = {
              new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex ("#ddd") },
              new Setter { Property = Button.TextColorProperty, Value = Color.Black },
              new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
              new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button)) }
            }
            };
            var orangeButton = new Style(typeof(Button))
            {
                Setters = {
              new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex ("#E8AD00") },
              new Setter { Property = Button.TextColorProperty, Value = Color.White },
              new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
              new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button))}
            }
            };

            var grid = new Grid
            {
                RowSpacing = 1,
                ColumnSpacing = 1
            };
            int quantLinhas = (listaApartamento.Count / 4);
            for (int i = 0; i < quantLinhas + 1; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            var row = 0;
            var col = 0;
            var lista = 0;
            var cor1 = Color.Green;
            var cor2 = Color.Blue;
            var cor3 = Color.Yellow;
            var cor4 = Color.Salmon;
            var listaCor = new List<Color>();
            listaCor.Add(cor1);
            listaCor.Add(cor2);
            listaCor.Add(cor3);
            listaCor.Add(cor4);
            for (int i = 0; i < listaApartamento.Count; i++)
            {
                //if (listaApartamento[i].Situacao == "O")

                button = new Button
                {
                    Text = listaApartamento[i].NApto,
                    Style = orangeButton
                };
                grid.Children.Add(button, col, row);
                col = col + 1;
                if (col == 4)
                {
                    row = row + 1;
                    col = 0;
                }
                lista = lista + 1;
                if (lista == 4)
                    lista = 0;
            }
            Content = new ScrollView
            {
                Orientation = ScrollOrientation.Horizontal,
                Content = grid
            };
        }
    }
}
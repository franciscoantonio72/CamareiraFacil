using CamareiraFacil.Model;
using CamareiraFacil.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CamareiraFacil.View
{
    public class MapaOcupacao : ContentPage
    {
        Button button;
        private List<Apartamento> listaApartamentoOriginal;
        private List<Apartamento> listaApartamento;
        Style ocupadoStyle;
        Style interditadoStyle;
        Style desocupadoStyle;
        Style sujoStyle;
        Style bloqueadoStyle;
        public MapaOcupacao()
        {
            ApiCamareiraFacil api = new ApiCamareiraFacil();
            try
            {
                listaApartamento = api.GetApartamentosOcupados();
                listaApartamentoOriginal = api.GetApartamentosOcupados();
            }
            catch (Exception E)
            {
                DisplayAlert("Erro", "Erro: " + E.Message, "OK");
                return;
            }

            CallingApartamentosAsync();
        }

        void eTeste()
        { 
            ocupadoStyle = new Style(typeof(Button))
            {
                Setters = {
                      new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex ("#E8AD00") },
                      new Setter { Property = Button.TextColorProperty, Value = Color.White },
                      new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
                      new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button)) }
                }
            };
            interditadoStyle = new Style(typeof(Button))
            {
                Setters = {
                      new Setter { Property = Button.BackgroundColorProperty, Value = Color.FromHex ("#ddd") },
                      new Setter { Property = Button.TextColorProperty, Value = Color.Black },
                      new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
                      new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button)) }
                }
            };
            desocupadoStyle = new Style(typeof(Button))
            {
                Setters = {
                      new Setter { Property = Button.BackgroundColorProperty, Value = Color.Green /* Color.FromHex ("#E8AD00")*/ },
                      new Setter { Property = Button.TextColorProperty, Value = Color.White },
                      new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
                      new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button))}
                }
            };
            sujoStyle = new Style(typeof(Button))
            {
                Setters = {
                      new Setter { Property = Button.BackgroundColorProperty, Value = Color.Salmon },
                      new Setter { Property = Button.TextColorProperty, Value = Color.WhiteSmoke },
                      new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
                      new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button))}
                }
            };
            bloqueadoStyle = new Style(typeof(Button))
            {
                Setters = {
                      new Setter { Property = Button.BackgroundColorProperty, Value = Color.Red  /* Color.FromHex ("#E8AD00")*/ },
                      new Setter { Property = Button.TextColorProperty, Value = Color.White },
                      new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
                      new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button))}
                }
            };

            Grid grid = RetornarGrid(listaApartamento);

            var stackpricipal = new StackLayout();

            var stack1 = new StackLayout
            {
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            stack1.Children.Add(grid);
            var scrol = new ScrollView
            {
                Orientation = ScrollOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Content = stack1
            };

            var stack2 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.End
            };

            var ocupadoButton = new Button { Text = "O", WidthRequest = 60, Style = ocupadoStyle };
            ocupadoButton.Clicked += OcupadoButton_Clicked;
            var desocupadoButton = new Button { Text = "D", WidthRequest = 60, Style = desocupadoStyle };
            desocupadoButton.Clicked += DesocupadoButton_Clicked;
            var sujoButton = new Button { Text = "S", WidthRequest = 60, Style = sujoStyle };
            sujoButton.Clicked += SujoButton_Clicked;
            var interditadoButton = new Button { Text = "I", WidthRequest = 60, Style = interditadoStyle };
            interditadoButton.Clicked += InterditadoButton_Clicked;
            var bloqueadoButton = new Button { Text = "B", WidthRequest = 60, Style = bloqueadoStyle };
            bloqueadoButton.Clicked += BloqueadoButton_Clicked;
            var todosButton = new Button { Text = "T", WidthRequest = 60, Style = bloqueadoStyle };
            todosButton.Clicked += TodosButton_Clicked;
            stack2.Children.Add(ocupadoButton);
            stack2.Children.Add(desocupadoButton);
            stack2.Children.Add(sujoButton);
            stack2.Children.Add(interditadoButton);
            stack2.Children.Add(ocupadoButton);
            stack2.Children.Add(bloqueadoButton);
            stackpricipal.Children.Add(scrol);
            stackpricipal.Children.Add(stack2);

            Content = stackpricipal;
        }

        private Grid RetornarGrid(List<Apartamento> llistaApartamento)
        {
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                RowSpacing = 1,
                ColumnSpacing = 1
            };
            int quantLinhas = (llistaApartamento.Count / 4);
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
            for (int i = 0; i < llistaApartamento.Count; i++)
            {
                var corButton = new Style(typeof(Button));
                if (llistaApartamento[i].Situacao == "D")
                    corButton = desocupadoStyle;
                else if (llistaApartamento[i].Situacao == "S")
                    corButton = sujoStyle;
                else if (llistaApartamento[i].Situacao == "O")
                    corButton = ocupadoStyle;
                else if (llistaApartamento[i].Situacao == "I")
                    corButton = interditadoStyle;
                else
                    corButton = bloqueadoStyle;

                button = new Button()
                {
                    Text = llistaApartamento[i].NApto,
                    Style = corButton
                };
                grid.Children.Add(button, col, row);
                col = col + 1;
                if (col == 4)
                {
                    row = row + 1;
                    col = 0;
                }
            }

            return grid;
        }

        async Task CallingApartamentosAsync(string tipo = "")
        {
            /*
            listaApartamento.Clear();
            if (tipo == "")
                listaApartamento = listaApartamentoOriginal;
            */
            if (tipo != "")
                listaApartamento = listaApartamentoOriginal.Where(w => w.Situacao == tipo).ToList();

            eTeste();
        }

        private void InterditadoButton_Clicked(object sender, EventArgs e)
        {
            CallingApartamentosAsync("I");
            //throw new NotImplementedException();
        }

        private void BloqueadoButton_Clicked(object sender, EventArgs e)
        {
            CallingApartamentosAsync("B");
            //throw new NotImplementedException();
        }

        private void SujoButton_Clicked(object sender, EventArgs e)
        {
            CallingApartamentosAsync("S");
            //throw new NotImplementedException();
        }

        private void DesocupadoButton_Clicked(object sender, EventArgs e)
        {
            CallingApartamentosAsync("D");
            // throw new NotImplementedException();
        }

        private void OcupadoButton_Clicked(object sender, EventArgs e)
        {
            CallingApartamentosAsync("O");

            // listaApartamento.Where(w => w.Situacao == "O");
        }

        private void TodosButton_Clicked(object sender, EventArgs e)
        {
            //
        }
    }
}

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
        private List<Apartamento> listaApartamentoCores;
        Style ocupadoStyle;
        Style interditadoStyle;
        Style desocupadoStyle;
        Style sujoStyle;
        Style bloqueadoStyle;
        Style arrumacaoStyle;
        Color corFundoO;
        Color corTextoO;
        Color corFundoD;
        Color corTextoD;
        Color corFundoS;
        Color corTextoS;
        Color corFundoI;
        Color corTextoI;
        Color corFundoB;
        Color corTextoB;
        Color corFundoA;
        Color corTextoA;

        public MapaOcupacao()
        {
            ApiCamareiraFacil api = new ApiCamareiraFacil();
            try
            {
                listaApartamento = api.GetApartamentosOcupados();
                listaApartamentoOriginal = api.GetApartamentosOcupados();
                listaApartamentoCores = api.GetCarregarCoresApartamento();

                CarregarCores();
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
                      new Setter { Property = Button.BackgroundColorProperty, Value = corFundoO  },
                      new Setter { Property = Button.TextColorProperty, Value = corTextoO  },
                      new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
                      new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button)) }
                }
            };
            interditadoStyle = new Style(typeof(Button))
            {
                Setters = {
                      new Setter { Property = Button.BackgroundColorProperty, Value = corFundoI },
                      new Setter { Property = Button.TextColorProperty, Value = corTextoI },
                      new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
                      new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button)) }
                }
            };
            desocupadoStyle = new Style(typeof(Button))
            {
                Setters = {
                      new Setter { Property = Button.BackgroundColorProperty, Value = corFundoD },
                      new Setter { Property = Button.TextColorProperty, Value = corTextoD },
                      new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
                      new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button))}
                }
            };
            sujoStyle = new Style(typeof(Button))
            {
                Setters = {
                      new Setter { Property = Button.BackgroundColorProperty, Value = corFundoS },
                      new Setter { Property = Button.TextColorProperty, Value = corTextoS },
                      new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
                      new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button))}
                }
            };
            bloqueadoStyle = new Style(typeof(Button))
            {
                Setters = {
                      new Setter { Property = Button.BackgroundColorProperty, Value = corFundoB },
                      new Setter { Property = Button.TextColorProperty, Value = corTextoB },
                      new Setter { Property = Button.BorderRadiusProperty, Value = 0 },
                      new Setter { Property = Button.FontSizeProperty, Value = Device.GetNamedSize(NamedSize.Medium, typeof(Button))}
                }
            };

            arrumacaoStyle = new Style(typeof(Button))
            {
                Setters = {
                      new Setter { Property = Button.BackgroundColorProperty, Value = corFundoA },
                      new Setter { Property = Button.TextColorProperty, Value = corTextoA },
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

            var ocupadoButton = new Button { Text = "O", WidthRequest = 55, Style = ocupadoStyle };
            ocupadoButton.Clicked += OcupadoButton_Clicked;
            var desocupadoButton = new Button { Text = "D", WidthRequest = 55, Style = desocupadoStyle };
            desocupadoButton.Clicked += DesocupadoButton_Clicked;
            var sujoButton = new Button { Text = "S", WidthRequest = 55, Style = sujoStyle };
            sujoButton.Clicked += SujoButton_Clicked;
            var interditadoButton = new Button { Text = "I", WidthRequest = 55, Style = interditadoStyle };
            interditadoButton.Clicked += InterditadoButton_Clicked;
            var bloqueadoButton = new Button { Text = "B", WidthRequest = 55, Style = bloqueadoStyle };
            bloqueadoButton.Clicked += BloqueadoButton_Clicked;
            var todosButton = new Button { Text = "T", WidthRequest = 55, Style = bloqueadoStyle };
            todosButton.Clicked += Arrumacao_Clicked;
            var arrumacaoButton = new Button { Text = "A", WidthRequest = 55, Style = arrumacaoStyle };
            arrumacaoButton.Clicked += Arrumacao_Clicked;
            stack2.Children.Add(ocupadoButton);
            stack2.Children.Add(desocupadoButton);
            stack2.Children.Add(sujoButton);
            stack2.Children.Add(interditadoButton);
            stack2.Children.Add(ocupadoButton);
            stack2.Children.Add(bloqueadoButton);
            stack2.Children.Add(arrumacaoButton);
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
            if (tipo != "")
                listaApartamento = listaApartamentoOriginal.Where(w => w.Situacao == tipo).ToList();

            eTeste();
        }

        private void InterditadoButton_Clicked(object sender, EventArgs e)
        {
            CallingApartamentosAsync("I");
        }

        private void BloqueadoButton_Clicked(object sender, EventArgs e)
        {
            CallingApartamentosAsync("B");
        }

        private void SujoButton_Clicked(object sender, EventArgs e)
        {
            CallingApartamentosAsync("S");
        }

        private void DesocupadoButton_Clicked(object sender, EventArgs e)
        {
            CallingApartamentosAsync("D");
        }

        private void OcupadoButton_Clicked(object sender, EventArgs e)
        {
            CallingApartamentosAsync("O");
        }

        private void Arrumacao_Clicked(object sender, EventArgs e)
        {
            CallingApartamentosAsync("A");
        }

        private void CarregarCores()
        {
            var lista = listaApartamentoCores.GroupBy(g => g.Situacao).ToList();

            foreach (var item in lista)
            {
                var rgbf = new RedGreenBlue(int.Parse(item.FirstOrDefault().CorFundo));
                var rgbt = new RedGreenBlue(int.Parse(item.FirstOrDefault().CorTexto));

                if (item.Key.Equals("O"))
                {
                    corTextoO = rgbt.Color;
                    corFundoO = rgbf.Color;
                } else
                if (item.Key.Equals("D"))
                {
                    corTextoD = rgbt.Color;
                    corFundoD = rgbf.Color;
                } else
                if (item.Key.Equals("S"))
                {
                    corTextoS = rgbt.Color;
                    corFundoS = rgbf.Color;
                } else
                if (item.Key.Equals("I"))
                {
                    corTextoI = rgbt.Color;
                    corFundoI = rgbf.Color;
                } else
                if (item.Key.Equals("A"))
                {
                    corTextoA = rgbt.Color;
                    corFundoA = rgbf.Color;
                } else
                if (item.Key.Equals("B"))
                {
                    corTextoB = rgbt.Color;
                    corFundoB = rgbf.Color;
                };
            }
        }
    }

    internal class RedGreenBlue
    {
        public byte Red { get; }
        public byte Green { get; }
        public byte Blue { get; }

        public Color Color => Color.FromRgb(Red, Green, Blue);

        public RedGreenBlue(byte red, byte green, byte blue)
        {
            Red = red;
            Green = green;
            Blue = blue;            
        }

        public RedGreenBlue(string hex)
        {
            if (string.IsNullOrEmpty(hex) || hex.Length < 6)
                throw new Exception("Invalid hex");
            Blue  = Convert.ToByte(hex.Substring(0, 2), 16);
            Green = Convert.ToByte(hex.Substring(2, 2), 16);
            Red = Convert.ToByte(hex.Substring(4, 2), 16);
        }

        public RedGreenBlue(int value) : this((value).ToString("X6"))
        {

        }
    }
}

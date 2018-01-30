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
        StackLayout stack = null;

        public MapaOcupacao ()
		{
            stack = new StackLayout { Orientation = StackOrientation.Horizontal };

            var grid = new Grid { Padding = 5, HorizontalOptions = LayoutOptions.CenterAndExpand };
            for (int i = 0; i < 4; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100, GridUnitType.Star) });
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
            for (int i = 0; i < 12; i++)
            {
                button = new Button
                {
                    Text = "10" + (i + 1).ToString(),
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Fill,
                    BackgroundColor = listaCor[lista],
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
            stack.Children.Add(grid);
            Content = stack;
        }
	}
}
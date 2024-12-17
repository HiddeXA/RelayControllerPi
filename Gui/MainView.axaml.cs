using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Gui.Commands;
using Library;
using Projektanker.Icons.Avalonia;

namespace Gui;

public partial class MainView : UserControl
{
    public List<Relay> Relays = new List<Relay>();
    public MainView()
    {
        InitializeComponent();
        Cursor = new Cursor(StandardCursorType.None);

        Relays = new List<Relay>
        {
            new Relay(17, "Relay 1", "fa-solid fa-car"),
            new Relay(18, "Relay 2", "fa-solid fa-beer-mug-empty"),
            new Relay(27, "Relay 3", "fa-solid fa-battery-empty"),
            new Relay(22, "Relay 4", "fa-solid fa-joint"),
            new Relay(23, "Relay 5", "fa-brands fa-playstation"),
            new Relay(24, "Relay 6", "fa-solid fa-robot"),
            new Relay(25, "Relay 7", "fa-solid fa-rocket"),
            new Relay(5, "Relay 8", "fa-solid fa-satellite-dish"),
            new Relay(6, "Relay 9", "fa-solid fa-umbrella-beach"),
            new Relay(12, "Relay 10", "fa-solid fa-wheelchair-move"),
            new Relay(13, "Relay 11", "fa-solid fa-wifi"),
            new Relay(19, "Relay 12", "fa-solid fa-wind"),
            new Relay(16, "Relay 13", "fa-solid fa-wine-bottle"),
            new Relay(26, "Relay 14", "fa-solid fa-wine-glass"),
            new Relay(20, "Relay 15", "fa-solid fa-wrench"),
            new Relay(21, "Relay 16", "fa-solid fa-yin-yang"),
        };

        ButtonsGrid.Children.Clear();
        foreach (var relay in Relays)
        {
            Button button = new Button
            {
                Content = new WrapPanel
                {
                    Children =
                    {
                        new Icon
                        {
                            Value = relay.Icon,
                            Foreground = Brushes.White,
                            FontSize = 30,
                            Margin = new Thickness(0, 0, 0, 10),
                        },
                        new TextBlock
                        {
                            Text = relay.Name,
                        },
                    },
                    Orientation = Orientation.Vertical,
                    
                },
                CommandParameter = relay,
                Command = new RelayCommand(),
                [Grid.ColumnProperty] = Relays.IndexOf(relay) % 6,
                [Grid.RowProperty] = Relays.IndexOf(relay) / 6,
            };
            ButtonsGrid.Children.Add(button);
        }


    }

   
    
}
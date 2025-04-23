using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Gui.Commands;
using Gui.Commands.Args;
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

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        List<Relay> relays = new List<Relay>
        {
            new Relay(17, "Relay 1", "fa-solid fa-car", Relay.buttonType.Toggle),
            new Relay(18, "Relay 2", "fa-solid fa-beer-mug-empty", Relay.buttonType.Toggle),
            new Relay(27, "Relay 3", "fa-solid fa-battery-empty", Relay.buttonType.Toggle),
            new Relay(22, "Relay 4", "fa-solid fa-joint", Relay.buttonType.Toggle),
            new Relay(23, "Relay 5", "fa-brands fa-playstation", Relay.buttonType.Toggle),
            new Relay(24, "Relay 6", "fa-solid fa-robot", Relay.buttonType.Toggle),
            new Relay(25, "Relay 7", "fa-solid fa-rocket", Relay.buttonType.Toggle),
            new Relay(5, "Relay 8", "fa-solid fa-satellite-dish", Relay.buttonType.Toggle),
            new Relay(6, "Relay 9", "fa-solid fa-umbrella-beach", Relay.buttonType.Toggle),
            new Relay(12, "Relay 10", "fa-solid fa-wheelchair-move", Relay.buttonType.Toggle),
            new Relay(13, "Relay 11", "fa-solid fa-wifi", Relay.buttonType.Toggle),
            new Relay(19, "Relay 12", "fa-solid fa-wind", Relay.buttonType.Toggle),
            new Relay(16, "Relay 13", "fa-solid fa-wine-bottle", Relay.buttonType.Toggle),
            new Relay(26, "Relay 14", "fa-solid fa-wine-glass", Relay.buttonType.Toggle),
            new Relay(20, "Relay 15", "fa-solid fa-wrench", Relay.buttonType.Toggle),
            new Relay(21, "Relay 16", "fa-solid fa-yin-yang", Relay.buttonType.Toggle),
        };

        foreach (var relay in relays)
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
                [Grid.ColumnProperty] = relays.IndexOf(relay) % 6,
                [Grid.RowProperty] = relays.IndexOf(relay) / 6,
                Transitions = null,
            };
            if (relay.ButtonType == Relay.buttonType.Toggle)
            {
                button.CommandParameter = new RelayCommandArgs(relay, button);
                button.Command = new RelayCommand();
            }

            if (relay.ButtonType == Relay.buttonType.Hold)
            {
                button.AddHandler(Button.PointerPressedEvent, (sender, e) => { relay.Activate(); }, handledEventsToo: true);
                button.AddHandler(Button.PointerReleasedEvent, (sender, e) => { relay.Deactivate(); }, handledEventsToo: true);
            }
            ButtonsGrid.Children.Add(button);
        }
    }
}
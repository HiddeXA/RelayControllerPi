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

        string json = File.ReadAllText("relays.json");
        var relays = JsonSerializer.Deserialize<List<Relay>>(json, options);

        foreach (var relay in relays)
        {
            Console.WriteLine($"{relay.Name} - GPIO: {relay.Pin} - Type: {relay.ButtonType} - icon: {relay.Icon}");
        }

        ButtonsGrid.Children.Clear();
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
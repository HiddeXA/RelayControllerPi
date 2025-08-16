using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using Gui.Commands;
using Gui.Commands.Args;
using Library;
using Projektanker.Icons.Avalonia;

namespace Gui;

public partial class MainView : UserControl
{
    public List<Relay> Relays = new List<Relay>();

    public int AuxPage = 0;

    public void RefreshAuxButtons(AuxController auxController)
    {
        int soundShowAmount = 9;
        AuxGrid.Children.Clear();
        
        int start = AuxPage * soundShowAmount;
        int end = Math.Min(start + soundShowAmount, auxController.Sounds.Count);

        if (start > auxController.Sounds.Count)
        {
            AuxPage = 0;
            start = AuxPage * soundShowAmount;
            end = Math.Min(start + soundShowAmount, auxController.Sounds.Count);
        }

        if (AuxPage < 0)
        {
            AuxPage = auxController.Sounds.Count / soundShowAmount;
            start = AuxPage * soundShowAmount;
            end = Math.Min(start + soundShowAmount, auxController.Sounds.Count);
        }
        
        for (int i = start; i < end; i++)
        {
            int y = i;
            Button button = new Button
            {
                Content = auxController.Sounds.ElementAt(i).Key,
                [Grid.ColumnProperty] = (i - start) % 3,
                [Grid.RowProperty] = (i - start) / 3,
                Transitions = null
            };
            
            button.AddHandler(Button.ClickEvent, (sender, e) =>
            {
                if (auxController.LastPlayed == auxController.Sounds.ElementAt(y).Key && auxController.IsPlaying())
                {
                    auxController.StopAllSound();
                    // Background = new SolidColorBrush(Color.Parse("#373F51"));
                    return;
                }
                Console.WriteLine("3");
                auxController.PlaySound(auxController.Sounds.ElementAt(y).Key);
                // Background = new SolidColorBrush(Color.Parse("#00A6FF"));
            });
                
            AuxGrid.Children.Add(button);
        }
    }
    public MainView()
    {
        InitializeComponent();
        // Cursor = new Cursor(StandardCursorType.None);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter() }
        };

        List<Relay> relays = new List<Relay>
        {
            new Relay(new List<int> { 17, 25 }, "Zwaailicht", "fa-solid fa-tower-broadcast", Relay.buttonType.Toggle),
            new Relay(18, "Wisselen Zwaailicht", "fa-solid fa-rotate-right", Relay.buttonType.Hold),
            new Relay( new List<int> { 27, 22, 23, 5 } , "Groot Light", "fa-solid fa-lightbulb", Relay.buttonType.Toggle),
            new Relay(24, "Sier lighten", "fa-solid fa-bookmark", Relay.buttonType.Toggle),
            // new Relay(5, "Relay 8", "fa-solid fa-satellite-dish", Relay.buttonType.Toggle),
            // new Relay(6, "Relay 9", "fa-solid fa-umbrella-beach", Relay.buttonType.Toggle),
            // new Relay(12, "Relay 10", "fa-solid fa-wheelchair-move", Relay.buttonType.Toggle),
            // new Relay(13, "Relay 11", "fa-solid fa-wifi", Relay.buttonType.Toggle),
            // new Relay(19, "Relay 12", "fa-solid fa-wind", Relay.buttonType.Toggle),
            // new Relay(16, "Relay 13", "fa-solid fa-wine-bottle", Relay.buttonType.Toggle),
            // new Relay(26, "Relay 14", "fa-solid fa-wine-glass", Relay.buttonType.Toggle),
            // new Relay(20, "Relay 15", "fa-solid fa-wrench", Relay.buttonType.Toggle),
            // new Relay(21, "Relay 16", "fa-solid fa-yin-yang", Relay.buttonType.Toggle),
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
                            TextWrapping = TextWrapping.Wrap,
                            TextAlignment = TextAlignment.Center
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
        
        // //aux text animation
        // var transform = new TranslateTransform();
        // AuxPlayButtonText.RenderTransform = transform;
        //
        // AuxPlayButtonText.AttachedToVisualTree += async (_, __) =>
        // {
        //     await Task.Delay(100); // Wait for layout pass
        //
        //     double containerWidth = 180;
        //     double textWidth = AuxPlayButtonText.Bounds.Width;
        //
        //     if (textWidth > containerWidth)
        //     {
        //         var animation = new Animation
        //         {
        //             Duration = TimeSpan.FromSeconds(10),
        //             IterationCount = new IterationCount(2147483647),
        //             Children =
        //             {
        //                 new KeyFrame
        //                 {
        //                     Cue = new Cue(0),
        //                     Setters =
        //                     {
        //                         new Setter
        //                         {
        //                             Property = TranslateTransform.XProperty,
        //                             Value = containerWidth - 170
        //                         }
        //                     }
        //                 },
        //                 new KeyFrame
        //                 {
        //                     Cue = new Cue(0.5),
        //                     Setters =
        //                     {
        //                         new Setter
        //                         {
        //                             Property = TranslateTransform.XProperty,
        //                             Value = -textWidth + 130
        //                         }
        //                     }
        //                 },
        //                 new KeyFrame
        //                 {
        //                     Cue = new Cue(1),
        //                     Setters =
        //                     {
        //                         new Setter
        //                         {
        //                             Property = TranslateTransform.XProperty,
        //                             Value = containerWidth - 170
        //                         }
        //                     }
        //                 }
        //             }
        //         };
        //
        //         await animation.RunAsync(AuxPlayButtonText, CancellationToken.None);
        //     }
        // };

        AuxController auxController = new AuxController();

        // Dictionary<string, string> dummysounds = new Dictionary<string, string>();
        // dummysounds["sound of je moeder"] = "b";
        // dummysounds["c"] = "d";
        // dummysounds["e"] = "f";
        // dummysounds["g"] = "h";
        // dummysounds["i"] = "j";
        // dummysounds["k"] = "l";
        // dummysounds["m"] = "n";
        // dummysounds["o"] = "p";
        // dummysounds["q"] = "r";
        // dummysounds["s"] = "t";
        // dummysounds["u"] = "v";
        // dummysounds["w"] = "x";
        // dummysounds["y"] = "z";
        // dummysounds["z"] = "a";
        // dummysounds["b"] = "c";
        // dummysounds["d"] = "e";
        // dummysounds["f"] = "g";
        // dummysounds["h"] = "i";
        // dummysounds["j"] = "k";
        // dummysounds["l"] = "m";
        //
        // auxController.Sounds = dummysounds;
        
        RefreshAuxButtons(auxController);
        
        AuxNextButton.AddHandler(Button.PointerReleasedEvent, (sender, e) =>
        {
            AuxPage++;
            RefreshAuxButtons(auxController);
        }, handledEventsToo: true); 
        
        AuxPrevButton.AddHandler(Button.PointerReleasedEvent, (sender, e) =>
        {
            AuxPage--;
            RefreshAuxButtons(auxController);
        }, handledEventsToo: true);  
        
        AuxStopButton.AddHandler(Button.PointerReleasedEvent, (sender, e) =>
        { 
            auxController.StopAllSound();
        }, handledEventsToo: true);

        PartyController partyController = new PartyController(
            relays,
            new List<Relay>
            {
                new Relay(27, "", "", Relay.buttonType.Toggle),
                new Relay(22, "", "", Relay.buttonType.Toggle),
                new Relay(23, "", "", Relay.buttonType.Toggle),
                new Relay(5, "", "", Relay.buttonType.Toggle)
            },
            new List<Relay>
            {
                new Relay(17, "", "", Relay.buttonType.Toggle),
                new Relay(24, "", "", Relay.buttonType.Toggle),
                new Relay(25, "", "", Relay.buttonType.Toggle)
            }
        );
        
        PartyButton.AddHandler(Button.ClickEvent, (sender, e) =>
        {
            if (partyController.Active)
            {
                partyController.Deactivate();
                PartyButton.Background = new SolidColorBrush(Color.Parse("#373F51"));
            }
            else
            {
                partyController.Activate();
                PartyButton.Background = new SolidColorBrush(Color.Parse("#00A6FF"));
            }
        }, handledEventsToo: true);
    }
    
}
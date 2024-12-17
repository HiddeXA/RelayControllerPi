using System;
using System.Windows.Input;
using Avalonia.Media;
using Gui.Commands.Args;
using Library;

namespace Gui.Commands;

public class RelayCommand : ICommand
{
    public bool CanExecute(object? parameter)
    {
        if (parameter is RelayCommandArgs)
        {
            return true;
        }
        return false;
    }

    public void Execute(object? parameter)
    {
        if (parameter is RelayCommandArgs args)
        {
            args.Relay.Toggle();
            Console.WriteLine("Relay toggled");
            if (args.Relay.Status())
            {
                args.Button.Background = new SolidColorBrush(Color.Parse("#fca311"));
            }
            else
            {
                args.Button.Background = new SolidColorBrush(Color.Parse("#373F51"));
            }
        }
    }
    

    public event EventHandler? CanExecuteChanged;
}
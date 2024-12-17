using System;
using System.Windows.Input;
using Library;

namespace Gui.Commands;

public class RelayCommand : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        if (parameter is Relay relay)
        {
            relay.Toggle();
            Console.WriteLine("Relay toggled");
        }
    }
    

    public event EventHandler? CanExecuteChanged;
}
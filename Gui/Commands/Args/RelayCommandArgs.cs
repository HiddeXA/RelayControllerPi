using Avalonia.Controls;
using Library;

namespace Gui.Commands.Args;

public class RelayCommandArgs
{
    public Relay Relay { get; set; }
    public Button Button { get; set; }

    public RelayCommandArgs(Relay relay, Button button)
    {
        Relay = relay;
        Button = button;
    }
}
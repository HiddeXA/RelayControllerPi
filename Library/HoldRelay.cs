using System.Device.Gpio;

namespace Library;

public class HoldRelay : Relay
{
    public HoldRelay(int pin, string name, string icon) : base(pin, name, icon)
    {
    }
    
}
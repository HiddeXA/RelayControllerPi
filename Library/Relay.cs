using System.Device.Gpio;

namespace Library;

public class Relay
{
    public int Pin { get; set; }
    public string Name { get; set; }
    
    public string Icon { get; set; }

    private GpioController Gpio;
    
    public Relay(int pin, string name, string icon)
    {
        Pin = pin;
        Name = name;
        Icon = icon;
        try
        {
            Gpio = new GpioController();
            Gpio.OpenPin(Pin, PinMode.Input);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public void Activate()
    {
        Gpio.ClosePin(Pin);
        Gpio.OpenPin(Pin, PinMode.Output);
        Gpio.Write(Pin, PinValue.High);
    }
    
    public void Deactivate()
    {
        Gpio.ClosePin(Pin);
        Gpio.OpenPin(Pin, PinMode.Input);
    }
    
    public void Toggle()
    {
        if (Status())
        {
            Deactivate();
            Console.WriteLine($"{Name} is now inactive");
        } else
        {
            Activate();
            Console.WriteLine($"{Name} is now active");
        }
    }
    
    public bool Status()
    {
        return Gpio.GetPinMode(Pin) == PinMode.Output;
    }
}
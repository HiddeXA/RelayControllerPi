using System.Device.Gpio;

namespace Library;

public class Relay
{
    public int Pin { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public buttonType ButtonType { get; set; }
    public enum buttonType
    {
        Toggle,
        Hold
    }

    private GpioController Gpio;
    
    public Relay(int pin, string name, string icon, buttonType buttonType)
    {
        Pin = pin;
        Name = name;
        Icon = icon;
        ButtonType = buttonType;
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
        try
        {
            Gpio.ClosePin(Pin);
            Gpio.OpenPin(Pin, PinMode.Output);
            Gpio.Write(Pin, PinValue.High);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        Console.WriteLine($"{Name} is now active");
    }
    
    public void Deactivate()
    {
        try
        {
            Gpio.ClosePin(Pin);
            Gpio.OpenPin(Pin, PinMode.Input);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        Console.WriteLine($"{Name} is now inactive");
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
        try
        {
            return Gpio.GetPinMode(Pin) == PinMode.Output;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}
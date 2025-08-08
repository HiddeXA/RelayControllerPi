using System.Device.Gpio;

namespace Library;

public class Relay
{
    public List<int> Pins { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
    public bool Active { get; set; }
    public buttonType ButtonType { get; set; }
    public enum buttonType
    {
        Toggle,
        Hold
    }

    private GpioController Gpio;
    
    public Relay(List<int> pins, string name, string icon, buttonType buttonType)
    {
        Active = false;
        Pins = pins;
        Name = name;
        Icon = icon;
        ButtonType = buttonType;
        try
        {
            Gpio = new GpioController();
            Pins.ForEach(p => Gpio.OpenPin(p, PinMode.Input));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public Relay(int pin, string name, string icon, buttonType buttonType) : this(new List<int>(), name, icon, buttonType)
    {
        Pins.Add(pin);
        Gpio.OpenPin(pin, PinMode.Input);
    }
    
    public void Activate()
    {
        try
        {
            foreach (int pin in Pins)
            {
                Gpio.ClosePin(pin);
                Gpio.OpenPin(pin, PinMode.Output);
                Gpio.Write(pin, PinValue.High);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Active = true;
    }
    
    public void Deactivate()
    {
        try
        {
            foreach (int pin in Pins)
            {
                Gpio.ClosePin(pin);
                Gpio.OpenPin(pin, PinMode.Input);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
        Active = false;
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
            return Gpio.GetPinMode(Pins.First()) == PinMode.Output;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}
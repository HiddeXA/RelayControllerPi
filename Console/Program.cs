
using Library;

while (true)
{
    Console.WriteLine("which pin do you want to activate?");
    int pin = Convert.ToInt32(Console.ReadLine());

    Relay relay = new Relay(pin, "Test Relay");
    relay.Toggle();
}

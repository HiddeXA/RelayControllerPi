
using Library;

AuxController auxController = new();

while (true)
{
    foreach (var sound in auxController.Sounds)
    {
        Console.WriteLine(sound);
    }

    Console.WriteLine("What sound do you want to play?");
    string selectedSound = Console.ReadLine();

    if (auxController.Sounds.ContainsKey(selectedSound))
    {
        auxController.PlaySound(selectedSound);
    }
    else
    {
        Console.WriteLine("could not find sound");
    }
}
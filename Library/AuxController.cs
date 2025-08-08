using System.Diagnostics;
using NetCoreAudio;

namespace Library;

public class AuxController
{
    private string MountBasePath = "/home/hidde/Relay/";
    public string LastPlayed { get; set; } = "";
    public Dictionary<string, string> Sounds { get; set; } = new();
    private Player audioPlayer = new ();

    public AuxController()
    {
        if (!Directory.Exists(MountBasePath))
        {
            Console.WriteLine($"Mount base path {MountBasePath} not found.");
            return;
        }

        audioPlayer.SetVolume(100);
        
        string[] drives = Directory.GetDirectories(MountBasePath);
        
        foreach (var drive in drives)
        {
            Console.WriteLine($"Scanning drive: {drive}");

            string[] mp3Files = Directory.GetFiles(drive, "*.wav", SearchOption.AllDirectories);

            foreach (var file in mp3Files)
            {
                if (!file.Contains("._"))
                {
                    Console.WriteLine(file);
                    Sounds.Add(Path.GetFileNameWithoutExtension(file) ,file);
                }
                Console.WriteLine($"Found WAV: {file}");
            }

            if (mp3Files.Length == 0)
            {
                Console.WriteLine("No WAV files found.");
            }
        }
    }

    public void PlaySound(string soundName)
    {
        Console.WriteLine(Sounds[soundName]);
        audioPlayer.Play(Sounds[soundName]);
    }

    public void StopAllSound()
    {
        audioPlayer.Stop();
    }

    public bool IsPlaying()
    {
        return audioPlayer.Playing;
    }
}
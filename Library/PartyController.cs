namespace Library;

public class PartyController
{
    public List<Relay> AllRelays { get; set; }
    public List<Relay> OnRelays { get; set; }
    public List<Relay> SycleRelays { get; set; }
    public bool Active { get; set; }
    public int SycleDelay = 200;


    public PartyController(List<Relay> allRelays, List<Relay> sycleRelays, List<Relay> onRelays)
    {
        AllRelays = allRelays;
        SycleRelays = sycleRelays;
        OnRelays = onRelays;
    } 

    async public void Activate()
    {
        Active = true;
        foreach (var relay in OnRelays)
        {
            relay.Activate();
        }
        try
        {
            while (Active)
            {
                foreach (var relay in SycleRelays)
                {
                    if (!Active)
                    {
                        continue;
                    }
                    relay.Activate();
                    await Task.Delay(SycleDelay);
                    relay.Deactivate();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
       
    }

    public void Deactivate()
    {
        Active = false;
        foreach (var relay in AllRelays)
        {
            if (relay.Active)
            {
                relay.Activate();
                continue;
            }
            relay.Deactivate();
        }
    }

    public void Toggle()
    {
        if (Active)
        {
            Deactivate();
        }
        else
        {
            Activate();
        }
    }
}
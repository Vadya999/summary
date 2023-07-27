using System;
using System.Collections.Generic;

[Serializable]
public class SerializabeLevel
{
    public List<int> payZoneStates;
    public List<SerializablSkateShelf> shelfs;
    public List<SerilizableConvyor> conveyour;
    public bool spawnMoneyCollected;
    public SerializableTutorial tutorial;

    public SerializabeLevel()
    {
        payZoneStates = new List<int>();
        spawnMoneyCollected = false;
        tutorial = new SerializableTutorial();
        shelfs = new List<SerializablSkateShelf>();
        conveyour = new List<SerilizableConvyor>();
    }
}

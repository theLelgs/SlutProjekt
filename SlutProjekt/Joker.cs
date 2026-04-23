class Joker
{
    public int jokerID;
    public int savedValue;
    public List<int> triggerValues;
    public string triggerSuit;
    public List<string> triggerHands;
    public string name;
    public string description;
    public Joker(int ID)
    {
        jokerID=ID;
        description=GetJokerDescription(jokerID);
        StreamReader sr = new("JokerData.txt");
        string line = "";
        while (line.Split(":")[0]!=jokerID.ToString())
        {
            line = sr.ReadLine();
        }
        name=line.Split(":")[1];

        string valuestring = line.Split(":")[2];
        string[] values = valuestring.Split("|");
        List<int> dummyIntList = [];
        foreach (string value in values)
        {
            if (int.TryParse(value, out int valueInt))
            {
                dummyIntList.Add(valueInt);
            }
        }
        triggerValues=dummyIntList;

        triggerSuit = line.Split(":")[3];

        string handString = line.Split(":")[4];
        string[] hands = handString.Split("|");
        List<string> dummyStringList=[];
        foreach(string hand in hands)
        {
            dummyStringList.Add(hand);
        }
        triggerHands=dummyStringList;

        sr.Close();
    }

    public static (Joker joker, int chips, int mult, Dictionary<string, (int chips, int mult, int level, (int chipsBuff, int multBuff))> handDictionary) TriggerJoker(Joker joker, int chips, int mult, string playedHand, Dictionary<string, (int chips, int mult, int level, (int chipsBuff, int multBuff))> handDictionary)
    {
        Console.WriteLine("    "+joker.name);
        switch(joker.jokerID)
        {
            // 0 = Test Joker
            case 0:
                {
                    mult+=joker.savedValue;
                    joker.savedValue+=10;
                    break;
                }
            case 1:
                {
                    mult+=3;
                    break;
                }
            case 2:
                {                    
                    mult+=3;
                    break;
                }
            case 3:
                {                    
                    mult+=3;
                    break;
                }
            case 4:
                {
                    mult+=3;                    
                    break;
                }
            case 5:
                {
                    mult+=3;                    
                    break;
                }
            case 6:
                {
                    mult+=8;
                    break;
                }
            case 7:
                {
                    mult+=12;
                    break;
                }
            case 8:
                {
                    mult+=10;
                    break;
                }
            case 9:
                {
                    mult+=12;
                    break;
                }
            case 10:
                {
                    mult+=10;
                    break;
                }
            case 11:
                {
                    chips+=50;
                    break;
                }
            case 12:
                {
                    chips+=100;
                    break;
                }
            case 13:
                {
                    chips+=80;
                    break;
                }
            case 14:
                {
                    chips+=100;
                    break;
                }
            case 15:
                {
                    chips+=80;
                    break;
                }
            case 45:
                {
                    if (Random.Shared.Next(4)==0)
                    {
                        Card.UpgradeHand(handDictionary, playedHand);
                        Console.WriteLine($"Hand {playedHand} upgraded! +{handDictionary[playedHand].Item4.chipsBuff} Chips and +{handDictionary[playedHand].Item4.multBuff} mult!");
                    }
                    break;
                }
            
            default:
                {
                    break;
                }
        }
        return (joker, chips, mult, handDictionary);
    }
    public static string GetJokerDescription(int jokerID)
    {
        StreamReader sr = new("JokerDescriptions.txt");
        string line = sr.ReadLine();
        while (line.Split(":")[0]!=jokerID.ToString())
        {
            line = sr.ReadLine();
        }
        sr.Close();
        return line.Split(":")[1];
    }
}

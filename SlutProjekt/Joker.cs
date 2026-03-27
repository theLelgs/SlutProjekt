class Joker
{
    public int jokerID;
    public int? savedValue;
    public int? triggerValue;
    public string triggerSuit;

    public static (int chips, int mult) TriggerJoker(int jokerID, List<Joker> jokers, int chips, int mult, Dictionary<string, int> suitDictionary, List<string> validHands)
    {
        switch(jokerID)
        {
            case 1:
                {
                    mult+=4;
                    break;
                }
            case 2:
                {
                    if (suitDictionary.TryGetValue("Diamonds", out int value))
                    {
                        mult+=4*value;
                    }
                    break;
                }
            case 3:
                {
                    if (suitDictionary.TryGetValue("Hearts", out int value))
                    {
                        mult+=4*value;
                    }
                    break;
                }
            case 4:
                {
                    if (suitDictionary.TryGetValue("Spades", out int value))
                    {
                        mult+=4*value;
                    }
                    break;
                }
            case 5:
                {
                    if (suitDictionary.TryGetValue("Clubs", out int value))
                    {
                        mult+=4*value;
                    }
                    break;
                }
            case 6:
                {
                    if (validHands.Contains("Pair"))
                    {
                        mult+=8;
                    }
                    break;
                }
            case 7:
                {
                    if (validHands.Contains("Three of a Kind"))
                    {
                        mult+=12;
                    }
                    break;
                }
            case 8:
                {
                    if (validHands.Contains("Two Pair"))
                    {
                        mult+=10;
                    }
                    break;
                }
            case 9:
                {
                    if (validHands.Contains("Straight"))
                    {
                        mult+=12;
                    }
                    break;
                }
            case 10:
                {
                    if (validHands.Contains("Flush"))
                    {
                        mult+=10;
                    }
                    break;
                }
            case 11:
                {
                    if (validHands.Contains("Pair"))
                    {
                        chips+=50;
                    }
                    break;
                }
            case 12:
                {
                    if (validHands.Contains("Three of a Kind"))
                    {
                        chips+=100;
                    }
                    break;
                }
            case 13:
                {
                    if (validHands.Contains("Two Pair"))
                    {
                        chips+=80;
                    }
                    break;
                }
            case 14:
                {
                    if (validHands.Contains("Straight"))
                    {
                        chips+=100;
                    }
                    break;
                }
            case 15:
                {
                    if (validHands.Contains("Flush"))
                    {
                        chips+=80;
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
        return (chips, mult);
    }
}

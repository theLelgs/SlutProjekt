class Card{
    public int value;
    public string suit;
    // public string Edition;
    public static Dictionary<string, (int chips, int mult, int level, (int chipsBuff, int multBuff))> UpgradeHand(Dictionary<string, (int chips, int mult, int level, (int chipsBuff, int multBuff))> handDictionary,string hand)
{
    handDictionary[hand]=(handDictionary[hand].chips+handDictionary[hand].Item4.chipsBuff,handDictionary[hand].mult+handDictionary[hand].Item4.multBuff, handDictionary[hand].level+1, handDictionary[hand].Item4);

    return handDictionary;
}
}
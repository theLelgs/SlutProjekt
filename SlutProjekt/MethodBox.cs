class MethodBox{
    public static List<Card> CreateDeck(string deckType)
    {
        List<Card> dummyDeck = [];
        List<string> Suits = ["Spades", "Clubs", "Diamonds", "Hearts"];
        if (deckType == "Standard")
        {
            for (int i = 0; i < 4; i++)
            {
                for (int CardValue = 2; CardValue <= 14; CardValue++)
                {
                    Card dummyCard = new(){Value=CardValue, Suit=Suits[i]};
                    dummyDeck.Add(dummyCard);
                }
            }
        }
        return dummyDeck;
    }
}
class Deck{

    List<Card> cards;

    static List<Card> CreateDeck(string deckname)
    {
        List<Card> dummyDeck = [];
        List<string> Suits = ["Spades", "Clubs", "Diamonds", "Hearts"];
        if (deckname == "Standard")
        {
            for (int CardValue = 2; CardValue <= 14; CardValue++)
            {
                for (int i = 0; i < 4; i++)
                {
                    Card dummyCard = new(){Value=CardValue, Suit=Suits[i]};
                    dummyDeck.Add(dummyCard);
                }
            }
        }
        return dummyDeck;
    }
}
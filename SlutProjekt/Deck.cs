class Deck{
    public List<Card> cards = [];
    public Deck(string deckType)
    {
        List<string> Suits = ["Spades", "Clubs", "Diamonds", "Hearts"];
        if (deckType == "Standard")
        {
            for (int i = 0; i < 4; i++)
            {
                for (int CardValue = 2; CardValue <= 14; CardValue++)
                {
                    Card dummyCard = new(){Value=CardValue, Suit=Suits[i]};
                    cards.Add(dummyCard);
                }
            }
        }
        if (deckType == "Random")
        {
            for (int i = 0; i < 52; i++)
            {
                Card dummyCard = new(){Value=Random.Shared.Next(2,15), Suit=Suits[Random.Shared.Next(4)]};
                cards.Add(dummyCard);
            }
        }
    }
}
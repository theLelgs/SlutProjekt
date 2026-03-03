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
    }
}
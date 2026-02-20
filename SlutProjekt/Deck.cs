class Deck{

    public List<Card> cards = [];
    public string deckType;

    public Deck()
    {
        cards = MethodBox.CreateDeck(deckType);
    }
    
}
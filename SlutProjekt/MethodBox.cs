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
    public static List<Card> SortDescending(List<Card> inputCards)
    {
        List<Card> dummyList = [];
        for (int cardValue = 14; cardValue > 1; cardValue--)
        {
            foreach(Card card in inputCards)
            {
                if (card.Value==cardValue)
                {
                    dummyList.Add(card);
                }
            }
        }
        return dummyList;
    }
    public static List<Card> SortAscending(List<Card> inputCards)
    {
        List<Card> dummyList = [];
        for (int cardValue = 2; cardValue <= 14; cardValue++)
        {
            foreach(Card card in inputCards)
            {
                if (card.Value==cardValue)
                {
                    dummyList.Add(card);
                }
            }
        }
        return dummyList;
    }
    public static List<Card> SortBySuit(List<Card> inputCards)
    {
        List<Card> dummyList = [];
        List<string> suits = ["Spades", "Clubs", "Diamonds", "Hearts"];
        foreach (string suit in suits)
        {
            List<Card> dummyList2 = [];
            foreach (Card card in inputCards)
            {
                if (card.Suit==suit)
                {
                    dummyList2.Add(card);
                }
            }
            dummyList.AddRange(SortDescending(dummyList2));
        }
        return dummyList;
    }
    public static bool ListsAreTheSame(List<Card> isSelected, List<Card> hand)
    {
        foreach(Card card in hand)
        {
            if(!isSelected.Contains(card))
            {
                return false;
            }
        }
        return true;
    }

}
class Sort{
    public static List<Card> Descending(List<Card> inputCards)
    {
        List<Card> dummyList = [];
        for (int cardValue = 14; cardValue > 1; cardValue--)
        {
            foreach(Card card in inputCards)
            {
                if (card.value==cardValue)
                {
                    dummyList.Add(card);
                }
            }
        }
        return dummyList;
    }
    public static List<Card> Ascending(List<Card> inputCards)
    {
        List<Card> dummyList = [];
        for (int cardValue = 2; cardValue <= 14; cardValue++)
        {
            foreach(Card card in inputCards)
            {
                if (card.value==cardValue)
                {
                    dummyList.Add(card);
                }
            }
        }
        return dummyList;
    }
    public static List<Card> Suit(List<Card> inputCards)
    {
        List<Card> dummyList = [];
        List<string> suits = ["Spades", "Clubs", "Diamonds", "Hearts"];
        foreach (string suit in suits)
        {
            List<Card> dummyList2 = [];
            foreach (Card card in inputCards)
            {
                if (card.suit==suit)
                {
                    dummyList2.Add(card);
                }
            }
            dummyList.AddRange(Descending(dummyList2));
        }
        return dummyList;
    }
}
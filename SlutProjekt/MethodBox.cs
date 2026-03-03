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
    public static List<Card> Sort(List<Card> inputCards, string sortMethod)//Sorterar en lista med kort. Alternativ är "Ascending", "Descending" och "Suit".
{
    List<Card> dummyList = [];
    switch(sortMethod)
    {
        case "Descending":
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
            break;
        case "Ascending":
            for (int cardValue = 2; cardValue < 14; cardValue++)
            {
                foreach(Card card in inputCards)
                {
                    if (card.Value==cardValue)
                    {
                        dummyList.Add(card);
                    }
                }
            }
            break;
        case "Suit":
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
                dummyList.AddRange(Sort(dummyList2, "Descending"));
            }

            break;
        default:
            Console.WriteLine("Invalid sortMethod");
            dummyList=inputCards;
            break;     
    }
    return dummyList;
}

}
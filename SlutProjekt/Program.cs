string input = "";

//Run start: 
Deck currentDeck = new()
{
    cards=MethodBox.CreateDeck("Standard")
};

int handSize = 7;

List<Card> hand = [];
List<Card> selectedCards=[];

while (true)
{
    (hand, currentDeck) = DrawCard(Math.Min(handSize-hand.Count,currentDeck.cards.Count), hand, currentDeck);
    
    if (input == "Suit")
    {
        hand=MethodBox.SortBySuit(hand);
    }
    if (input == "Descending")
    {
        hand=MethodBox.SortDescending(hand);
    }
    if (input == "Ascending")
    {
        hand=MethodBox.SortAscending(hand);
    }
    
    foreach (Card card in hand)
    {
        if (selectedCards.Contains(card))
        {
            Console.Write("    ");
        }
        Console.WriteLine($"Hand: {card.Value} of {card.Suit}");
    }
    Console.WriteLine("");
    input = Console.ReadLine();
    if (input == "toggleLast")
    {
        if (selectedCards.Contains(hand[^1]))
        {
            selectedCards.Remove(hand[^1]);
        }
        else
        {
            selectedCards.Add(hand[^1]);
        }
    }
    if (input == "toggleAll")
    {
        if (MethodBox.ListsAreTheSame(selectedCards, hand))
        {
            selectedCards.Clear();
        }
        else{
            foreach (Card card in hand)
            {
                if (!selectedCards.Contains(card))
                {
                    selectedCards.Add(card);
                }
            }
        }
    }
    if (input == "discard")
    {
        List<Card> dummyList = [];
        foreach (Card card in selectedCards)
        {
            hand.Remove(card);
            dummyList.Add(card);
        }
        foreach (Card card in dummyList)
        {
            selectedCards.Remove(card);
        }
    }
}


static (List<Card> newHand, Deck newDeck) DrawCard(int cardsToDraw, List<Card> hand, Deck deck)
{
    List<Card> newHand = hand;
    Deck newDeck = deck;
    for (int cardsDrawn = 0; cardsDrawn < cardsToDraw; cardsDrawn++)
    {
        Card randomCard = deck.cards[Random.Shared.Next(deck.cards.Count)];
        newHand.Add(randomCard);
        newDeck.cards.Remove(randomCard);
    }
    return (newHand, newDeck);
}



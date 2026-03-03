string input = "";

//Run start: 
Deck currentDeck = new("Standard");


int handSize = 7;

List<Card> hand = [];
List<Card> selectedCards=[];

while (true)
{
    (hand, currentDeck.cards) = DrawCard(Math.Min(handSize-hand.Count,currentDeck.cards.Count), hand, currentDeck);
    
    if (input == "Suit")
    {
        hand=Sort.Suit(hand);
    }
    if (input == "Descending")
    {
        hand=Sort.Descending(hand);
    }
    if (input == "Ascending")
    {
        hand=Sort.Ascending(hand);
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


static (List<Card> newHand, List<Card> newDeckCards) DrawCard(int cardsToDraw, List<Card> hand, Deck deck)
{
    List<Card> newHand = hand;
    List<Card> newDeckCards = deck.cards;
    for (int cardsDrawn = 0; cardsDrawn < cardsToDraw; cardsDrawn++)
    {
        Card randomCard = deck.cards[Random.Shared.Next(deck.cards.Count)];
        newHand.Add(randomCard);
        newDeckCards.Remove(randomCard);
    }
    return (newHand, newDeckCards);
}



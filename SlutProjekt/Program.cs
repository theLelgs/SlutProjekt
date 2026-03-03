// using Raylib_cs;
// Raylib.InitWindow(800,600,"Balatro");


string input = "";

//Run start: 
Deck deck = new("Standard");
int handSize = 7;

//Ante start:
Deck tempDeck = new(null);
foreach (Card card in deck.cards)
{
    tempDeck.cards.Add(card);
}
List<Card> hand = [];
List<Card> selectedCards=[];

while (true)
{
    (hand, tempDeck.cards) = DrawCard(Math.Min(handSize-hand.Count,tempDeck.cards.Count), hand, tempDeck);
    
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
    if (input.Split(":")[0]=="toggle"&&int.TryParse(input.Split(":")[1], out int toToggle)&&toToggle<=hand.Count)
    {
        List<Card> cardsToToggle = [];
        for(int i = 0; i<toToggle;i++)
        {
            cardsToToggle.Add(hand[i]);
        }

        bool willSelect = false;
        foreach(Card card in cardsToToggle)
        {
            if (!selectedCards.Contains(card))
            {
                willSelect=true;
            }
        }

        if (willSelect)
        {
            selectedCards.AddRange(cardsToToggle.Except(selectedCards));
        }
        else
        {
            selectedCards=selectedCards.Except(cardsToToggle).ToList();
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

    if (input == "play"&&selectedCards.Count!=0)
    {
        string handType;
        //Look for how many instances of each value, add to dictionary
        Dictionary<int, int> dummyDictionary = [];
        foreach (Card card in selectedCards)
        {
            if (dummyDictionary.Keys.Contains(card.Value))
            {
                dummyDictionary[card.Value]++;
            }
            else
            {    
                dummyDictionary.Add(card.Value,1);
            }
        }
        foreach (int value in dummyDictionary.Keys)
        {
            Console.WriteLine($"{value}, {dummyDictionary[value]}");
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
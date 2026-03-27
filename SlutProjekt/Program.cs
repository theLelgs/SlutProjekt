using Raylib_cs;

Raylib.InitWindow(800,600,"Balatro");
Raylib.SetTargetFPS(60);

string input = "";

//Run start: 
// Deck deck = new("Standard");
Deck deck = new("Random");
int handSize = 7;

//Ante start:
Deck tempDeck = new(null);
foreach (Card card in deck.cards)
{
    tempDeck.cards.Add(card);
}
List<Card> hand = [];
List<Card> selectedCards=[];



Dictionary<string, (int chips, int mult, int level, (int chipsBuff, int multBuff))> handDictionary = new()
{
    {"High Card", (5, 1, 1, (10,1))},
    {"Pair", (10, 2, 1, (15, 1))},
    {"Two Pair", (20, 2, 1, (20, 1))},
    {"Three of a Kind", (30, 3, 1, (20, 2))},
    {"Straight", (30, 4, 1, (30, 3))},
    {"Flush", (35, 4, 1, (15, 2))},
    {"Full House", (40, 4, 1,(25, 2))},
    {"Four of a Kind", (60, 7, 1, (30, 3))},
    {"Straight Flush", (100, 8, 1,(40, 4))},
    {"Five of a Kind", (120, 12, 1, (35, 3))},
    {"Flush House", (140, 14, 1, (40, 4))},
    {"Flush Five", (160, 16, 1, (50, 3))}
};




bool gameRunning = true;
while (gameRunning && !Raylib.WindowShouldClose())
{
    Raylib.BeginDrawing();
    
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
        (hand, selectedCards) = RemoveCards(hand, selectedCards);
    }

    if (input == "play"&&selectedCards.Count!=0)
    {
    
        //Look for how many instances of each value, add to dictionary

        Console.WriteLine("Played hand: " + FindBestHand(selectedCards));
        Console.WriteLine($"Score: {GetScore(selectedCards, handDictionary)}");
        
        //Remove the played cards
        (hand, selectedCards) = RemoveCards(hand, selectedCards);
    }

    if (deck.cards.Count==0&&hand.Count==0)
    {
        Console.WriteLine("You lose lmao, deck ran out");
        Console.ReadKey();
        gameRunning=false;
    }
    Raylib.EndDrawing();
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
static string FindBestHand(List<Card> selectedCards)
{
    Dictionary<int, int> valueDictionary = [];
    Dictionary<string, int> suitDictionary = [];
    foreach (Card card in selectedCards)
    {
        if (valueDictionary.ContainsKey(card.Value))
        {
            valueDictionary[card.Value]++;
        }
        else
        {    
            valueDictionary.Add(card.Value, 1);
        }
        if (suitDictionary.ContainsKey(card.Suit))
        {
            suitDictionary[card.Suit]++;
        }
        else
        {
            suitDictionary.Add(card.Suit, 1);
        }
    }

    List<string> validHands = GetHands(valueDictionary, suitDictionary, selectedCards);
        
    //
    List<string> handPriority = ["Flush Five", "Flush House", "Five of a Kind","Straight Flush", "Four of a Kind", "Full House", "Flush", "Straight", "Three of a Kind", "Two Pair", "Pair"];
    foreach (string hand in handPriority)
    {
        if (validHands.Contains(hand))
        {
            return hand;
        }
    }
    return "High Card";
}
static List<string> GetHands(Dictionary<int,int> valueDictionary, Dictionary<string, int> suitDictionary, List<Card> selectedCards)
{
    List<string> validHands = [];
    validHands=LookForBasicHands(validHands, valueDictionary, suitDictionary, selectedCards);
    
    //Check for Two Pair
    int pairs = 0;
    foreach (int value in valueDictionary.Values)
    {
        if (value==2)
        {
            pairs++;
        }
    }
    if (pairs==2)
    {
        validHands.Add("Two Pair");
    }

    //Look for Straight
    List<int> numbersForStraight = [14,2,3,4,5,6,7,8,9,10,11,12,13,14];
    int numInARow = 0;
    foreach (int value in numbersForStraight)
    {
        if (valueDictionary.ContainsKey(value))
        {
            numInARow++;
        }
        else
        {
            numInARow=0;
        }
        if(numInARow>=5)
        {
            validHands.Add("Straight");
            break;
        }
    }

    //Look for combo-hands
    validHands=LookForComboHands(validHands);
    return validHands;
}
static List<string> LookForBasicHands(List<string> validHands, Dictionary<int, int> valueDictionary, Dictionary<string, int> suitDictionary, List<Card> selectedCards)
{
    if (valueDictionary.ContainsValue(2))
    {
        validHands.Add("Pair");
    }
    if (valueDictionary.ContainsValue(3))
    {
        validHands.Add("Three of a Kind");
    }
    if (valueDictionary.ContainsValue(4))
    {
        validHands.Add("Four of a Kind");
    }
    if (valueDictionary.ContainsValue(5))
    {
        validHands.Add("Five of a Kind");
    }
    if (suitDictionary.Count==1&&selectedCards.Count>=5)
    {
        validHands.Add("Flush");
    }
    return validHands;
}
static List<string> LookForComboHands(List<string> validHands)
{
    if (validHands.Contains("Three of a Kind")&&validHands.Contains("Pair"))
    {
        validHands.Add("Full House");
    }
    if (validHands.Contains("Flush")&&validHands.Contains("Five of a Kind"))
    {
        validHands.Add("Flush Five");
    }
    if (validHands.Contains("Flush")&&validHands.Contains("Full House"))
    {
        validHands.Add("Flush House");
    }
    if (validHands.Contains("Straight")&&validHands.Contains("Flush"))
    {
        validHands.Add("Straight Flush");
    }
    return validHands;
}
static (List<Card> newHand, List<Card> newSelectedCards) RemoveCards(List<Card> hand, List<Card> selectedCards)
{
    foreach (Card card in selectedCards)
    {
        hand.Remove(card);
    }
    selectedCards=[];
    return (hand, selectedCards);
}
static int GetScore(List<Card> cardsPlayed, Dictionary<string, (int chips, int mult, int level, (int chipsBuff, int multBuff))> handDictionary) 
{
    string handPlayed = FindBestHand(cardsPlayed);
    int chips = handDictionary[handPlayed].chips;
    int mult = handDictionary[handPlayed].mult;
    List<int?> cardScore = [2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11];
    foreach (Card card in cardsPlayed)
    {
        chips+=(int)cardScore[card.Value-2];
    }

    return chips*mult;
}






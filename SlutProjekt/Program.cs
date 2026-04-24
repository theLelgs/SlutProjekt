string input = "";

//Run start: 
// Deck deck = new("Standard");
Deck deck = new("Random");
int handSize = 7;

//Ante start:
//tempDeck blir en exakt kopia av deck, används för att kunna förändra deck under matchen utan permanenta förändringar
Deck tempDeck = new(null);
foreach (Card card in deck.cards)
{
    tempDeck.cards.Add(card);
}
List<Card> hand = [];
List<Card> selectedCards=[];
List<Joker> jokers = [];


//Dictionary som sparar alla olika poker-händer, samt vad de ger och hur de uppgraderas
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

int score = 0;


bool gameRunning = true;
//Listar alla möjliga kommandon för spelaren vid starten.
PrintCommands();
while (gameRunning)
{
    //Drar kort tills antal kort når handSize (Default: 7)
    (hand, tempDeck.cards) = DrawCard(Math.Min(handSize-hand.Count,tempDeck.cards.Count), hand, tempDeck);
    
    //Listar alla kort i handen
    Console.WriteLine("\nHand: ");
    foreach (Card card in hand)
    {
        if (selectedCards.Contains(card))
        {
            Console.Write("  --");
        }
        Console.WriteLine($"    {card.value} of {card.suit}");
    }

    input = Console.ReadLine().ToLower();

    //Om spelaren skriver "help", skriv ut alla kommandon 
    if (input == "help")
    {
        PrintCommands();
    }
    
    //Om spelaren skriver "suit", sortera korten i handen efter färg
    else if (input == "suit")
    {
        hand=Sort.Suit(hand);
    }
    //Om spelaren skriver "descending", sortera korten efter värde i minskande ordning
    else if (input == "descending")
    {
        hand=Sort.Descending(hand);
    }
    //Om spelaren skriver "ascending", sortera korten efter värde i ökande ordning
    else if (input == "ascending")
    {
        hand=Sort.Ascending(hand);
    }
    
    //Om spelaren skriver "toggleLast", toggla sista kortet i handen
    else if (input == "togglelast")
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
    //Om spelaren skriver "toggleAll", toggla alla kort i handen
    else if (input == "toggleall")
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
    
    else if (input.Contains(':'))
    {        
        //Om spelaren skriver "toggle:X", toggla de första X korten
        if (input.Split(":")[0]=="toggle")
        {
            if (int.TryParse(input.Split(":")[1], out int toToggle))
            {
                if (toToggle<=hand.Count&&toToggle>0)
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
                else if (toToggle>hand.Count)
                {
                    Console.WriteLine("Cannot toggle more cards than you have");
                }
                else
                {
                    Console.WriteLine("You cannot toggle less than 1 card");
                }
            }
            else
            {
                Console.WriteLine("You did not write a valid number after \"toggle:\". Try again");
            }
        }
        //Om spelaren skriver "getJoker:X", ge spelaren jokern med ID = X
        if(input.Split(":")[0]=="getjoker")
        {
            if (int.TryParse(input.Split(":")[1], out int jokerID))
            {
                if ((jokerID>0&&jokerID<=15)||jokerID==45)
                {    
                    Joker joker = new(jokerID);
                    jokers.Add(joker);
                    Console.WriteLine($"Added {joker.name} to your joker list\nEffect: {joker.description}");
                    
                }
                else if(jokerID>15)
                {
                    Console.WriteLine("There are no jokers with an ID above 15 (except 45), choose a lower number.");
                }
                else
                {
                    Console.WriteLine("You need to input a number between 1 and 15 (except 45), choose another number");
                }
            }
            else
            {
                Console.WriteLine("You did not write a valid number after \"GetJoker:\". Try again");                
            }
        }
    }
    
    //Om spelaren skriver "discard", släng korten som är togglade
    else if (input == "discard")
    {
        (hand, selectedCards) = RemoveCards(hand, selectedCards);
    }
    //Om spelaren skriver "play", spela alla togglade kort
    else if (input == "play"&&selectedCards.Count!=0)
    {
    
        //Look for how many instances of each value, add to dictionary

        Console.WriteLine("Played hand: " + FindBestHand(selectedCards));
        int scoreToGain = GetScore(selectedCards, handDictionary, jokers);
        Console.WriteLine($"Score: {scoreToGain}");
        score += scoreToGain;
        //Remove the played cards
        (hand, selectedCards) = RemoveCards(hand, selectedCards);
    }
    else
    {
        Console.WriteLine("No valid commands found. Type \"Help\" to get a list of all valid commands.");
    }
    //Om decket är tomt, stäng av
    if (tempDeck.cards.Count==0&&hand.Count==0)
    {
        Console.WriteLine("You lose lmao, deck ran out");
        Console.WriteLine($"Total Score: {score}");
        Console.ReadKey();
        gameRunning=false;
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
static string FindBestHand(List<Card> selectedCards)
{
    (Dictionary<int, int> valueDictionary, Dictionary<string, int> suitDictionary) = MakeDictionary(selectedCards);

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
static (Dictionary<int, int>, Dictionary<string, int>) MakeDictionary(List<Card> selectedCards)
{
    (Dictionary<int, int> valueDictionary, Dictionary<string, int> suitDictionary) = ([],[]);
    foreach (Card card in selectedCards)
    {
        if (valueDictionary.ContainsKey(card.value))
        {
            valueDictionary[card.value]++;
        }
        else
        {    
            valueDictionary.Add(card.value, 1);
        }
        if (suitDictionary.ContainsKey(card.suit))
        {
            suitDictionary[card.suit]++;
        }
        else
        {
            suitDictionary.Add(card.suit, 1);
        }
    }
    return (valueDictionary, suitDictionary);
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
static int GetScore(List<Card> cardsPlayed, Dictionary<string, (int chips, int mult, int level, (int chipsBuff, int multBuff))> handDictionary, List<Joker> jokers) 
{

    List<int> triggerPostScoring = [0,1,45];
    List<Joker> toTrigger = [];
    Dictionary<List<int>, List<Joker> > triggerValues = [];
    Dictionary<List<string>, List<Joker> > triggerHands = [];
    Dictionary<string, List<Joker>> triggerSuits = [];
    Console.WriteLine("Jokers: ");
    foreach (Joker joker in jokers)
    {
        if (joker.triggerValues!=null)
        {
            if (triggerValues.TryGetValue(joker.triggerValues, out List<Joker> jokersToTrigger))
            {
                jokersToTrigger.Add(joker);
            }
            else
            {
                triggerValues.Add(joker.triggerValues, [joker]);    
            }
        }
        if (joker.triggerSuit!=null)
        {
            if (triggerSuits.TryGetValue(joker.triggerSuit, out List<Joker> jokersToTrigger))
            {
                jokersToTrigger.Add(joker);
            }
            else
            {
                triggerSuits.Add(joker.triggerSuit, [joker]);    
            }
        }
        if (joker.triggerHands.Count!=0)
        {
            if (triggerHands.TryGetValue(joker.triggerHands, out List<Joker> jokersToTrigger))
            {
                jokersToTrigger.Add(joker);
            }
            else
            {
                triggerHands.Add(joker.triggerHands, [joker]);
            }
        }
        if (triggerPostScoring.Contains(joker.jokerID))
        {
            toTrigger.Add(joker);
        }
    }
    (Dictionary<int,int> valueDictionary,  Dictionary<string, int> suitDictionary) = MakeDictionary(cardsPlayed);
    List<string> handsPlayed = GetHands(valueDictionary, suitDictionary, cardsPlayed);
    string handPlayed = FindBestHand(cardsPlayed);
    int chips = handDictionary[handPlayed].chips;
    int mult = handDictionary[handPlayed].mult;
    List<int> cardScore = [2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11];
    foreach (Card card in cardsPlayed)
    {
        chips+=cardScore[card.value-2];
        foreach (KeyValuePair<List<int>, List<Joker>> valuePair in triggerValues)
        {
            if (valuePair.Key.Contains(card.value))
            {
                for (int i = 0; i < valuePair.Value.Count; i++)
                {
                (valuePair.Value[i], chips, mult, handDictionary) = Joker.TriggerJoker(valuePair.Value[i], chips, mult, handPlayed, handDictionary);
                }
            }
        }
        if (triggerSuits.TryGetValue(card.suit, out List<Joker> jokerList))
        {
            for (int i = 0; i < jokerList.Count; i++)
            {
                (jokerList[i], chips, mult, handDictionary) = Joker.TriggerJoker(jokerList[i], chips, mult, handPlayed, handDictionary);
            }
        }
    }

    foreach (KeyValuePair<List<string>, List<Joker>> valuePair in triggerHands)
    {
        foreach (string hand in handsPlayed)
        {
            if (valuePair.Key.Contains(hand))
            {
                for(int i = 0; i < valuePair.Value.Count; i++)
                {
                    (valuePair.Value[i], chips, mult, handDictionary) = Joker.TriggerJoker(valuePair.Value[i], chips, mult, handPlayed, handDictionary);
                }
            }
        }
    }    
    for (int i = 0; i < toTrigger.Count; i++)
    {
        (toTrigger[i], chips, mult, handDictionary) = Joker.TriggerJoker(toTrigger[i], chips, mult, handPlayed, handDictionary);
    }
    Console.WriteLine($"Chips: {chips}, Mult: {mult}");
    return chips*mult;
}
static void PrintCommands(){
    Console.WriteLine("toggle:X\n    Toggles the first X cards in your hand");
    Console.WriteLine("toggleAll\n    Toggles all cards in your hand");
    Console.WriteLine("toggleLast\n    Toggles the last card in your hand");
    Console.WriteLine("Suit\n    Sorts your hand according to the cards suits");
    Console.WriteLine("Ascending\n    Sorts your hand in order of ascending value");
    Console.WriteLine("Descending\n    Sorts your hand in order of descending value");
    Console.WriteLine("Help\n    Writes this list");
    Console.WriteLine("Play\n    Plays your toggled cards");
    Console.WriteLine("Discard\n    Discards your toggled cards");
    Console.WriteLine("GetJoker:X\n    Gives you the joker with ID X. 1<=X<=15");
}   
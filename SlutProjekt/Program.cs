// Pseudokod;
// Class: Card;
// Sparar värde och färg.
// Instanser 1-13 och spader/hjärter/klöver/ruter läggs till i lista, List<Card> deck;
// 
// Dra kort från deck, lägg till i hand.
// Använd "List<Card> tempDeck = deck" under spelets gång, så man kan ta ut kort utan problem.
// 
// Ta ut kort från tempDeck, lägg till i hand. Array eller list för hand (Vill man kunna modifiera max hand-size?)
// deck-class?
// Ska man ha CreateDeck() som en metod under deck-classen?
// 
// On run start:
// Create deck
// Move deck to tempDeck
// Set ante, $, hands, discards etc.

string input = "";
List<Card> hand = [];
Deck currentDeck = new()
{
    cards=MethodBox.CreateDeck("Standard")
};
List<Card> selectedCards = [];
int handSize = 7;
List<bool> isSelected=[];
for (int i = 0; i < handSize; i++)
{
    isSelected.Add(false);
}

List<string> sortMethods = ["Ascending", "Descending", "Suit"];

while (true)
{
    (hand, currentDeck) = DrawCard(handSize-hand.Count, hand, currentDeck);
    
    if (sortMethods.Contains(input))
    {
        hand = MethodBox.Sort(hand, input);    
    }
    foreach (Card card in hand)
    {
        if (isSelected[hand.IndexOf(card)])
        {
            Console.Write("    ");
        }
        Console.WriteLine($"Hand: {card.Value} of {card.Suit}");
    }
    
    input = Console.ReadLine();
    if (input == "toggleLast")
    {
        if (isSelected[^1])
        {
            isSelected[^1]=false;
        }
        else
        {
            isSelected[^1]=true;
        }
    }
    if (input=="toggleAll")
    {
        if (isSelected.Contains(false))
        {    
            for (int i = 0; i < isSelected.Count; i++)
            {
                isSelected[i]=true;
            }
        }
        else
        {
            for (int i = 0; i < isSelected.Count; i++)
            {
                isSelected[i]=false;
            }
        }
    }
    if (input == "discard")
    {
        for (int i = 0; i < isSelected.Count; i++)
        {
            if (isSelected[i])
            {
                hand.RemoveAt(i);
                isSelected[i]=false;
            }
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

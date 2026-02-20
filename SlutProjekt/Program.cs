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



Deck Standard = new()
{
    // cards=MethodBox.CreateDeck("Standard")
    deckType="Standard"
};

foreach (Card card in Standard.cards)
{
    Console.WriteLine($"{card.Value} of {card.Suit}");
}
Console.ReadLine();

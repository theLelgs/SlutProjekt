class MethodBox{
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
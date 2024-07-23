[System.Serializable]
public class Card 
{
    public string cardName {  get; private set; }
    public int cardId {  get; private set; }

    public int currentPower;
    
    public Card()
    {
        cardName = string.Empty;
        cardId = 0;
        currentPower = 1;
    }

    public Card(string cardName, int cardId)
    {
        this.cardName = cardName;
        this.cardId = cardId;
    }
   
}

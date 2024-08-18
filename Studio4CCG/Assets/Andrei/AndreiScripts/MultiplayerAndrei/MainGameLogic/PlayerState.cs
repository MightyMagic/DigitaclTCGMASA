[System.Serializable]
public class PlayerState 
{
    //public CardInfo[] cardsInHand { get; private set; }
    public int currentHp;
    public int currentMana;
    public string playerName;

    public PlayerState()
    {

    }
    
    public PlayerState(int currentHp, int currentMana, string playerName)
    {
        this.currentHp = currentHp;
        this.currentMana = currentMana;
        this.playerName = playerName;
    }
}

[System.Serializable]
public class CardOnBoard 
{
    public CardInfo cardInfo;
    public int currentAttack;
    public int currentHP;

    public CardOnBoard() { }
    public CardOnBoard(CardInfo cardInfo, int currentAttack, int currentHP)
    {
        this.cardInfo = cardInfo;
        this.currentAttack = currentAttack;
        this.currentHP = currentHP;
    }
}

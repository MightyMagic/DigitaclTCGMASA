[System.Serializable]
public class CardOnBoardInfo 
{
    public string OwnerName;
    public CardOnBoard cardOnBoard;
    public ColumnName column;
    public int rowIndex;

    public CardOnBoardInfo() { }

    public CardOnBoardInfo(string ownerName, CardOnBoard cardOnBoard, ColumnName column, int rowIndex)
    {
        this.cardOnBoard = new CardOnBoard();
        OwnerName = ownerName;
        this.cardOnBoard.cardInfo = new CardInfo(cardOnBoard.cardInfo.Id, cardOnBoard.cardInfo.CardName);
        this.cardOnBoard.currentHP = cardOnBoard.currentHP;
        this.cardOnBoard.currentAttack = cardOnBoard.currentAttack;
        this.column = column;
        this.rowIndex = rowIndex;
    }
}

public class BoardStatePacket : BasePacket
{
    public int numberOfCards;
    public string playerName;
    public CardOnBoardInfo[] cardsOnBoard = new CardOnBoardInfo[12];

    public BoardStatePacket() { }

    public BoardStatePacket(PlayerData playerData, string playerName, int numberOfCards, CardOnBoardInfo[] cardsOnBoard): base(PacketType.BoardState, playerData)
    {
        this.numberOfCards = numberOfCards;
        this.playerName = playerName;
        this.cardsOnBoard = new CardOnBoardInfo[numberOfCards];

        for (int i = 0; i < numberOfCards; i++)
        {
            this.cardsOnBoard[i] = new CardOnBoardInfo();
            this.cardsOnBoard[i].cardOnBoard = new CardOnBoard();
            this.cardsOnBoard[i].cardOnBoard.cardInfo = new CardInfo();
        }

        for (int i = 0; i < numberOfCards; i++)
        {
            this.cardsOnBoard[i].OwnerName = cardsOnBoard[i].OwnerName;
            this.cardsOnBoard[i].cardOnBoard.cardInfo.CardName = cardsOnBoard[i].cardOnBoard.cardInfo.CardName;
            this.cardsOnBoard[i].cardOnBoard.cardInfo.Id = cardsOnBoard[i].cardOnBoard.cardInfo.Id;

            this.cardsOnBoard[i].cardOnBoard.currentAttack = cardsOnBoard[i].cardOnBoard.currentAttack;
            this.cardsOnBoard[i].cardOnBoard.currentHP = cardsOnBoard[i].cardOnBoard.currentHP;

            this.cardsOnBoard[i].rowIndex = cardsOnBoard[i].rowIndex;
            this.cardsOnBoard[i].column = cardsOnBoard[i].column;
        }
    }

    public byte[] Serialize()
    {
        base.Serialize();

        binaryWriter.Write(numberOfCards);
        binaryWriter.Write(playerName);

        for (int i = 0; i < numberOfCards; i++)
        {
            binaryWriter.Write(cardsOnBoard[i].OwnerName);
            binaryWriter.Write(cardsOnBoard[i].cardOnBoard.cardInfo.CardName);
            binaryWriter.Write(cardsOnBoard[i].cardOnBoard.cardInfo.Id);
            binaryWriter.Write(cardsOnBoard[i].cardOnBoard.currentAttack);
            binaryWriter.Write(cardsOnBoard[i].cardOnBoard.currentHP);
            binaryWriter.Write((int)cardsOnBoard[i].column);
            binaryWriter.Write(cardsOnBoard[i].rowIndex);
        }

        return serializeMemoryStream.ToArray();
    }


    public new BoardStatePacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);

        numberOfCards = binaryReader.ReadInt32();
        playerName = binaryReader.ReadString();

        cardsOnBoard = new CardOnBoardInfo[numberOfCards];

        for (int i = 0; i < numberOfCards; i++)
        {
            cardsOnBoard[i] = new CardOnBoardInfo();
            cardsOnBoard[i].cardOnBoard = new CardOnBoard();
            cardsOnBoard[i].cardOnBoard.cardInfo = new CardInfo();

            cardsOnBoard[i].OwnerName = binaryReader.ReadString();
            cardsOnBoard[i].cardOnBoard.cardInfo.CardName = binaryReader.ReadString();
            cardsOnBoard[i].cardOnBoard.cardInfo.Id = binaryReader.ReadInt32();
            cardsOnBoard[i].cardOnBoard.currentAttack = binaryReader.ReadInt32();
            cardsOnBoard[i].cardOnBoard.currentHP = binaryReader.ReadInt32();
            cardsOnBoard[i].column = (ColumnName)binaryReader.ReadInt32();
            cardsOnBoard[i].rowIndex = binaryReader.ReadInt32();
        }

        return this;
    }

}

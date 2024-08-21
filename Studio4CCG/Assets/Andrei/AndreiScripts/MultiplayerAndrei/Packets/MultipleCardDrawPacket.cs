using System.Collections.Generic;

public class MultipleCardDrawPacket : BasePacket
{
    //public CardInfo[] cards = new CardInfo[];
    public string playerName;
    public int numberOfCards;
    public CardInfo[] cards = new CardInfo[3];

    public MultipleCardDrawPacket() { }

    public MultipleCardDrawPacket(PlayerData playerData, int numberOfCards, CardInfo[] cards,string playerName): base(PacketType.MultipleCardDraw, playerData)
    { 
        this.playerName = playerName;

        this.numberOfCards = numberOfCards;

        this.cards = new CardInfo[numberOfCards];

        for(int i = 0; i < cards.Length; i++)
        {
            this.cards[i] = new CardInfo();
            this.cards[i].Id = cards[i].Id;
            this.cards[i].CardName = cards[i].CardName;
        }
    }

    public byte[] Serialize()
    {
        base.Serialize();
        binaryWriter.Write(this.playerName);
        binaryWriter.Write(this.numberOfCards);
        for(int i = 0;i < this.cards.Length; i++)
        {
            binaryWriter.Write(this.cards[i].Id);
            binaryWriter.Write(this.cards[i].CardName);
        }

        return serializeMemoryStream.ToArray();
    }

    public new MultipleCardDrawPacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);
        this.playerName = binaryReader.ReadString();
        this.numberOfCards = binaryReader.ReadInt32();

        this.cards = new CardInfo[numberOfCards];

        for(int i = 0; i < this.numberOfCards; i++)
        {
            this.cards[i] = new CardInfo();
            this.cards[i].Id= binaryReader.ReadInt32();
            this.cards[i].CardName= binaryReader.ReadString();
        }
        
        return this;
    }

    //public byte[] Serialize()
    //{
    //    base.Serialize();
    //    binaryWriter.Write(this.cardInfo.Id);
    //    binaryWriter.Write(this.cardInfo.CardName);
    //    binaryWriter.Write(this.playerName);
    //
    //    return serializeMemoryStream.ToArray();
    //}
    //
    //public new CardDrawPacket Deserialize(byte[] buffer)
    //{
    //    base.Deserialize(buffer);
    //
    //    this.cardInfo.Id = binaryReader.ReadInt32();
    //    this.cardInfo.CardName = binaryReader.ReadString();
    //    this.playerName = binaryReader.ReadString();
    //    return this;
    //}
}

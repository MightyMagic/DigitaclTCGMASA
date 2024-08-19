public class CardDrawPacket : BasePacket
{
    public CardInfo cardInfo = new CardInfo();
    public string playerName;

    public CardDrawPacket() { }

    public CardDrawPacket(PlayerData playerData, CardInfo cardInfo, string playerName) : base(PacketType.CardDraw, playerData)
    {
        this.cardInfo.CardName = cardInfo.CardName;
        this.cardInfo.Id = cardInfo.Id;
        this.playerName = playerName;
    }

    public byte[] Serialize()
    {
        base.Serialize();
        binaryWriter.Write(this.cardInfo.Id);
        binaryWriter.Write(this.cardInfo.CardName);
        binaryWriter.Write(this.playerName);

        return serializeMemoryStream.ToArray();
    }

    public new CardDrawPacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);
        this.cardInfo.Id = binaryReader.ReadInt32();
        this.cardInfo.CardName= binaryReader.ReadString();
        this.playerName = binaryReader.ReadString();
        return this;
    }
}

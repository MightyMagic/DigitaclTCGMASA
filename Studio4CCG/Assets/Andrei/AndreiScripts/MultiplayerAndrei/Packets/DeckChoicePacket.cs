public class DeckChoicePacket : BasePacket
{
    public int deckIndex;

    public DeckChoicePacket()
    {

    }

    public DeckChoicePacket(PlayerData playerData, int deckIndex): base(PacketType.DeckChoice, playerData)
    {
        this.deckIndex = deckIndex;
    }

    public new byte[] Serialize()
    {
        base.Serialize();

        binaryWriter.Write(this.deckIndex);

        return serializeMemoryStream.ToArray();
    }

    public new DeckChoicePacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);
        this.deckIndex = binaryReader.ReadInt32();

        return this;
    }
}

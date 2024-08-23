using System.Xml.Linq;

public class GameOverPacket : BasePacket
{
    public string winnerName;

    public GameOverPacket() { }

    public GameOverPacket(PlayerData playerData, string winnerName): base(PacketType.GameOver, playerData)
    {
        this.winnerName = winnerName;
    }

    public byte[] Serialize()
    {
        base.Serialize();
        binaryWriter.Write(winnerName);
        return serializeMemoryStream.ToArray();
    }

    public new GameOverPacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);

        winnerName = binaryReader.ReadString();

        return this;
    }
}

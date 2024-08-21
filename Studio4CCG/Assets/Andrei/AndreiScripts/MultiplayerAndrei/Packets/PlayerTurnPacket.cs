public class PlayerTurnPacket : BasePacket
{
    public string playersTurnName;

    public PlayerTurnPacket() { }

    public PlayerTurnPacket(PlayerData playerData, string playersTurnName): base(PacketType.PlayerTurn, playerData)
    {
        this.playersTurnName = playersTurnName;
    }

    public byte[] Serialize()
    {
        base.Serialize();
        binaryWriter.Write(playersTurnName);

        return serializeMemoryStream.ToArray();
    }

    public new PlayerTurnPacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);
        PlayerTurnPacket ptp = new PlayerTurnPacket();

        ptp.playersTurnName = binaryReader.ReadString();

        return ptp;
    }
}

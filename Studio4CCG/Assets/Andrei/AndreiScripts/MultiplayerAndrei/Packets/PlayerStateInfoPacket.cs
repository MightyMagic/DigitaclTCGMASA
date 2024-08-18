public class PlayerStateInfoPacket : BasePacket
{
    //public CardInfo[] cardsInHand { get; private set; }
    //public int currentHp { get; private set; }
    //public int currentMana { get; private set; }

    public PlayerState playerState { get; set; }

    public PlayerStateInfoPacket()
    {

    }

    public PlayerStateInfoPacket(PlayerData playerData, int currentHp, int currentMana, string playerName): base(PacketType.PlayerStateInfo, playerData)
    {
        PlayerState playerState = new PlayerState(currentHp, currentMana, playerName);
        this.playerState = playerState;
    }

    public new byte[] Serialize()
    {
        base.Serialize();
        binaryWriter.Write(playerState.currentHp);
        binaryWriter.Write(playerState.currentMana);
        binaryWriter.Write(playerState.playerName);
        return serializeMemoryStream.ToArray();
    }

    public new PlayerStateInfoPacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);

        this.playerState = new PlayerState();

        this.playerState.currentHp = binaryReader.ReadInt32();
        this.playerState.currentMana = binaryReader.ReadInt32();
        this.playerState.playerName = binaryReader.ReadString();

        return this;
    }
}

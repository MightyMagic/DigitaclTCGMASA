public class HealthPacket : BasePacket
{
    public int myHp;
    public string myName;

    public int enemyHp;
    public string enemyName;

    public HealthPacket() { }

    public HealthPacket(PlayerData playerData, int myHp, string myName, int enemyHp, string enemyName): base(PacketType.Health, playerData)
    {
        this.myHp = myHp;
        this.myName = myName;
        this.enemyHp = enemyHp;
        this.enemyName = enemyName;
    }

    public byte[] Serialize()
    {
        base.Serialize();
        binaryWriter.Write(myHp);
        binaryWriter.Write(myName);
        binaryWriter.Write(enemyHp);
        binaryWriter.Write(enemyName);

        return serializeMemoryStream.ToArray();
    }

    public new HealthPacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);

        myHp = binaryReader.ReadInt32();
        myName = binaryReader.ReadString();
        enemyHp = binaryReader.ReadInt32();
        enemyName = binaryReader.ReadString();

        return this;
    }


}

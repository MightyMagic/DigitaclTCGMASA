using UnityEngine.UIElements;

public class SceneLoadPacket : BasePacket
{
    public string SceneName { get; private set; }

    public SceneLoadPacket() { }

    public SceneLoadPacket(PlayerData playerData, string sceneName) : base(PacketType.SceneLoad, playerData)
    {
        SceneName = sceneName;
    }

    public new byte[] Serialize()
    {
        base.Serialize();
        binaryWriter.Write(SceneName);
        return serializeMemoryStream.ToArray();
    }

    public new SceneLoadPacket Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);
        SceneName = binaryReader.ReadString();
        return this;
    }
}

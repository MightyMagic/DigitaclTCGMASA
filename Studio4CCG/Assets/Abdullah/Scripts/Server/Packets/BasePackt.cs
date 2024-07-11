using System.Collections;
using System.Collections.Generic;
using System.IO;

public class BasePackt 
{
    public enum PacketType
    {
        None,
        PositionPacket,
        InstantiatePacket,
        DestroyPacket,
        SceneTransitionPacket,


        HeartbeatPacket,
        AnimationPacket,
    }
    public int PacketSize { get; private set; }
    public PacketType Type { get; private set; }
  //  public PlayerData PlayerData { get; private set; }

    protected MemoryStream writeMemory;
    protected MemoryStream readMemory;

    protected BinaryWriter binaryWriter;
    protected BinaryReader binaryReader;


    protected void BeginSerialize()
    {
        // Initialize MemoryStream for writing
        writeMemory = new MemoryStream();
        binaryWriter = new BinaryWriter(writeMemory);

        // we add the data we want to write ?
      /*  binaryWriter.Write((int)Type);
        binaryWriter.Write(PlayerData.ID);
        binaryWriter.Write(PlayerData.Name);*/

    }

    // packetSize + 4 to count for packet being stored in memory
    protected byte[] EndSerSerialize()
    {
        PacketSize = (int)writeMemory.Length + 4;
        binaryWriter.Write(PacketSize);
        return writeMemory.ToArray();


    }
}

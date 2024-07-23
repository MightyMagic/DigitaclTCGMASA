using System;
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
        TestText,
    }
    public int PacketSize { get; private set; }
    public PacketType Type { get; private set; }


    public BasePackt()
    {
        Type = PacketType.None;

    }
    //recieving data
    public BasePackt(PacketType type)
    {
        Type = type;

    }


    protected MemoryStream writeMemory;
    protected MemoryStream readMemory;

    protected BinaryWriter binaryWriter;
    protected BinaryReader binaryReader;

    
    //player ownership info 
    protected void BeginSerialize()
    {
        // Initialize MemoryStream for writing
        writeMemory = new MemoryStream();
        binaryWriter = new BinaryWriter(writeMemory);

        binaryWriter.Write((int)Type);

        // we add the data we want to write ?
    }

    // packetSize + 4 to count for packet being stored in memory
    protected byte[] EndSerSerialize()
    {
        PacketSize = (int)writeMemory.Length + 4;
        binaryWriter.Write(PacketSize);
        return writeMemory.ToArray();
    }
    public BasePackt DeSerialize(byte[] buffer, int bufferOffset)
    {
        readMemory= new MemoryStream(buffer);
        binaryReader= new BinaryReader(readMemory);
        readMemory.Seek(bufferOffset, SeekOrigin.Begin);
        Type=(PacketType)binaryReader.ReadInt32();


        return this;
    }

}

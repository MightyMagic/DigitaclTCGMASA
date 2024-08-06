using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

public class BasePackt 
{
    public enum PacketType
    {
        None,
        PositionPacket,
        InstantiatePacket,
        SceneTransitionPacket,

        DrawCardPacket,
        AnimationPacket,

        CardStatePacket,
        CardStatsPacket,

        RequestActivatePacket,
        MoveForwardPacket,
        SwitchTilePacket,
        DamageHealthPacket,
        DamageManaPacket,

        OwnershipPacket,
        FirstToPlayPacket,
        PlayerNumberPacket,

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

    
    protected void BeginSerialize()
    {
        // Initialize MemoryStream for writing
        writeMemory = new MemoryStream();
        binaryWriter = new BinaryWriter(writeMemory);
        binaryWriter.Write(PacketSize);
        binaryWriter.Write((int)Type);

        // we add the data we want to write ?
    }

    // packetSize + 4 to count for packet being stored in memory
    protected byte[] EndSerSerialize()
    {
        PacketSize = (int)writeMemory.Length;
        writeMemory.Position = 0;
        binaryWriter.Write(PacketSize);
        return writeMemory.ToArray();
    }
    public BasePackt DeSerialize(byte[] buffer, int bufferOffset)
    {
        readMemory= new MemoryStream(buffer);
        binaryReader= new BinaryReader(readMemory);
        readMemory.Seek(bufferOffset, SeekOrigin.Begin);

        PacketSize = binaryReader.ReadInt32();
        Type = (PacketType)binaryReader.ReadInt32();
        return this;
    }


}

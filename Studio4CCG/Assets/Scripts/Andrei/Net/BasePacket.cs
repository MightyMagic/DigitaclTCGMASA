using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BasePacket 
{
    public enum PacketType
    {
        PositionPacket
    }

    public PacketType type {  get; private set; }
    public PlayerData playerData { get; private set; }

    protected MemoryStream wms;
    protected BinaryWriter bw;

    protected MemoryStream rms;
    protected BinaryReader br;

    public BasePacket()
    {
        type = PacketType.PositionPacket;
        playerData = null;
    }

    public BasePacket(PlayerData playerData, PacketType type)
    {
        this.type = type;
        this.playerData = playerData;
    }

    protected void BeginSerialize()
    {
        wms = new MemoryStream();
        bw = new BinaryWriter(wms);

        bw.Write((int)type);
        bw.Write(playerData.ID);
        bw.Write(playerData.Name);
    }

    protected byte[] EndSerialize()
    {
        return wms.ToArray();
    }

    public virtual void Deserialize(byte[] buffer)
    {
        rms = new MemoryStream(buffer);
        br = new BinaryReader(rms);

        type = (PacketType)br.ReadInt32();
        playerData = new PlayerData(br.ReadString(), br.ReadString());

    }
    
}

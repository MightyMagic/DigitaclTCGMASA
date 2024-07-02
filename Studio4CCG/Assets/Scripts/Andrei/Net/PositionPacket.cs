using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PositionPacket : BasePacket
{
    public Vector3 Position {  get; private set; }

    public PositionPacket()
    {
        Position = Vector3.zero;
    }


    public PositionPacket(Vector3 position, PlayerData playerData):
        base(playerData, PacketType.PositionPacket)
    {
        Position = position;
    }
    
    public byte[] Serialize()
    {
        BeginSerialize();
        bw.Write(Position.x);
        bw.Write(Position.y);
        bw.Write(Position.z);
        return EndSerialize();
    }

    public override void Deserialize(byte[] buffer)
    {
        base.Deserialize(buffer);
        Position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
    }
}

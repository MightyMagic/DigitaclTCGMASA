using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateCardPacket : BasePackt
{

    public string PrefabName { get; private set; }
    public Vector3 Position{get;private set;}
    public Quaternion Rotation { get; private set; }

    public InstantiateCardPacket()
    {
        PrefabName = "";
        Position = Vector3.zero;
        Rotation = Quaternion.identity;

    }


    public InstantiateCardPacket(string prefab, Vector3 position, Quaternion rotation):
        base(PacketType.InstantiatePacket)
    {
        PrefabName = prefab;
        Position = position;
        Rotation = rotation;

    }


    public byte[] Serialize()
    {
        //player info
        BeginSerialize();
        // my data goes here.

        //prefab to Instantiate
        binaryWriter.Write(PrefabName);

        //it position. 
        //Maybe based on the tile position ?
        //later
        binaryWriter.Write(Position.x);
        binaryWriter.Write(Position.y);
        binaryWriter.Write(Position.z);

        //it rotation
        binaryWriter.Write(Rotation.x);
        binaryWriter.Write(Rotation.y);
        binaryWriter.Write(Rotation.z);
        binaryWriter.Write(Rotation.w);

        //add packet size for TCP
        return EndSerSerialize();
    }



}

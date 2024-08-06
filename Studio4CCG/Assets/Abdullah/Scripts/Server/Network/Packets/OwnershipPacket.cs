using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnershipPacket : BasePackt
{
    public string newOwner { get; private set; }
    public string currentCardID { get; private set; }
    // Start is called before the first frame update
    public OwnershipPacket()
    {
        //define variables
        newOwner = "";
        currentCardID = "";
    }
    //data I want to modifiy in Function. 
    public OwnershipPacket(string text, string cardID) :
        base(PacketType.OwnershipPacket)
    {
        //modify the variables
        newOwner = text;
        currentCardID = cardID;
    }

    public byte[] Serialize()
    {
        //player info
        BeginSerialize();

        // my data goes here.
        binaryWriter.Write(currentCardID);
        binaryWriter.Write(newOwner);

        // my data goes here.

        return EndSerSerialize();
    }

    public new OwnershipPacket DeSerialize(byte[] buffer, int bufferOffset)
    {
        base.DeSerialize(buffer, bufferOffset);
        // Read in the same order as it was serialized.
        currentCardID = binaryReader.ReadString();
        newOwner = binaryReader.ReadString();

        //Read in the same order as it was serialized

        return this;
    }


}




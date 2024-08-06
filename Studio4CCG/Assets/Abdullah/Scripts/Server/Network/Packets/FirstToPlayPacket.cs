using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstToPlayPacket : BasePackt
{
    public int PlayerCount { get; private set; }
    public bool FirstToPlay { get; private set; }

    // Start is called before the first frame update
    public FirstToPlayPacket()
    {
        //define variables
        PlayerCount = 0;
        FirstToPlay =false;
    }
    //data I want to modifiy in Function. 
    public FirstToPlayPacket(bool playFirst, int playerCount) :
        base(PacketType.FirstToPlayPacket)
    {
        //modify the variables
        PlayerCount = playerCount;
        FirstToPlay = playFirst;
    ;}

    public byte[] Serialize()
    {
        //player info
        BeginSerialize();

        // my data goes here.
        binaryWriter.Write(PlayerCount);
        binaryWriter.Write(FirstToPlay);


             // my data goes here.

        return EndSerSerialize();
    }

    public new FirstToPlayPacket DeSerialize(byte[] buffer, int bufferOffset)
    {
        base.DeSerialize(buffer, bufferOffset);
        // Read in the same order as it was serialized.
        PlayerCount= binaryReader.ReadInt32();
        FirstToPlay = binaryReader.ReadBoolean();

        //Read in the same order as it was serialized

        return this;
    }


}

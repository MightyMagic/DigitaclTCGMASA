using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerNumberPacket : BasePackt
{
    public int PlayerOrder { get; private set; }
    // Start is called before the first frame update
    public PlayerNumberPacket()
    {
        //define variables
        PlayerOrder = 0;
    }
    //data I want to modifiy in Function. 
    public PlayerNumberPacket(int playerOrder) :
        base(PacketType.SceneTransitionPacket)
    {
        //modify the variables
        PlayerOrder = playerOrder;
    }

    public byte[] Serialize()
    {
        //player info
        BeginSerialize();

        // my data goes here.

        binaryWriter.Write(PlayerOrder);

        // my data goes here.

        return EndSerSerialize();
    }

    public new PlayerNumberPacket DeSerialize(byte[] buffer, int bufferOffset)
    {
        base.DeSerialize(buffer, bufferOffset);
        // Read in the same order as it was serialized.

        PlayerOrder=binaryReader.ReadInt32();

        //Read in the same order as it was serialized

        return this;
    }


}

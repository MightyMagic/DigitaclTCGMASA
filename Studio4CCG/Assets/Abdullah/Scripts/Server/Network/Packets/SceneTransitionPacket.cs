using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionPacket : BasePackt
{
    public string SceneName {  get; private set; }
    // Start is called before the first frame update
    public SceneTransitionPacket()
    {
        //define variables
        SceneName ="";


    }
    //data I want to modifiy in Function. 
    public SceneTransitionPacket(string sceneName) :
        base(PacketType.SceneTransitionPacket)
    {
        
        SceneName = sceneName;
    }

    public byte[] Serialize()
    {
        //player info
        BeginSerialize();
        // my data goes here.

        binaryWriter.Write(SceneName);

        //add packet size for TCP
        return EndSerSerialize();
    }
    public new SceneTransitionPacket DeSerialize(byte[] buffer, int bufferOffset)
    {
        base.DeSerialize(buffer, bufferOffset);
        // Read in the same order as it was serialized.
        SceneName = binaryReader.ReadString();

        return this;
    }
}

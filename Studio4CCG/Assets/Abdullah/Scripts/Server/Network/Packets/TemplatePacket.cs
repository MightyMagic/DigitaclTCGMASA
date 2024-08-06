using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplatePacket : BasePackt
{
    public string Text { get; private set; }
    // Start is called before the first frame update
    public TemplatePacket()
    {
        //define variables
        Text = "";
    }
    //data I want to modifiy in Function. 
    public TemplatePacket(string text) :
        base(PacketType.SceneTransitionPacket)
    {
        //modify the variables
        Text = text;
    }

    public byte[] Serialize()
    {
        //player info
        BeginSerialize();

              // my data goes here.



             // my data goes here.

        return EndSerSerialize();
    }

    public new TemplatePacket DeSerialize(byte[] buffer, int bufferOffset)
    {
        base.DeSerialize(buffer, bufferOffset);
            // Read in the same order as it was serialized.


        
            //Read in the same order as it was serialized

        return this;
    }


}

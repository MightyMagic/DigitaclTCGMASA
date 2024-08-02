using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class TestTextPacket : BasePackt
{
    public string Text { get; private set; }

    public TestTextPacket()
    {
        Text="Hellowo World";


    }
    public TestTextPacket(string text):base(PacketType.TestText)

    {
        Text = text;
    }

    public byte[] Serialize()
    {
        //player info
        BeginSerialize();
        // my data goes here.

        //Send Text
        binaryWriter.Write(Text);

        //add packet size for TCP
        return EndSerSerialize();
    }
    public new TestTextPacket DeSerialize(byte[] buffer, int bufferOffset)
    {
        base.DeSerialize(buffer, bufferOffset);
        Text= binaryReader.ReadString();
        return this;
    }


}

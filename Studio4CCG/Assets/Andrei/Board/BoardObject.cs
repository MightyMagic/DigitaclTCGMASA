using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BoardObject 
{
    public string cardName;
    public Image image;

    public BoardObject()
    {
        cardName = string.Empty;
        //
    }

    public BoardObject(string ID, Image image)
    {
        this.cardName = ID;
        this.image = image;
    }
}

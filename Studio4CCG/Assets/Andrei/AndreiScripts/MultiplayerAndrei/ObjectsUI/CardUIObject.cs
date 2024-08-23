using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUIObject : MonoBehaviour
{
    public TextMeshProUGUI CardName;
    public TextMeshProUGUI CardDescription;

    public TextMeshProUGUI hp;
    public TextMeshProUGUI manaCost;

    public Button playCardButton;

    public int handIndex;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DisplayCard(string cardName, string cardDescription, int HP, int ManaCost)
    {
        CardName.text = cardName;
        CardDescription.text = cardDescription;
        hp.text = HP.ToString();
        manaCost.text = ManaCost.ToString();
    }
}

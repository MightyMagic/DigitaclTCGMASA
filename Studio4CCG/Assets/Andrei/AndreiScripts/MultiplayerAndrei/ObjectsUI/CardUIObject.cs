using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardUIObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CardName;
    [SerializeField] TextMeshProUGUI CardDescription;

    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI manaCost;
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

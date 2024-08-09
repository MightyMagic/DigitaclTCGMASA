using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsList : MonoBehaviour
{
    [SerializeField] List<CardSO> startingHand;
    [SerializeField] List<CardObject> startingObjects;
    //[SerializeField] List<Image> imagePlaceHolders;
    void Start()
    {
       for(int i = 0; i < startingObjects.Count; i++)
       {
            startingObjects[i].currentCard.mana = startingHand[i].mana;
            startingObjects[i].currentCard.image = startingHand[i].image;
            startingObjects[i].currentCard.power = startingHand[i].power;
       }
    }

    void Update()
    {
       // for (int i = 0; i < startingObjects.Count; i++)
       // {
       //     startingObjects[i].currentCard.mana = startingHand[i].mana;
       //     startingObjects[i].currentCard.image = startingHand[i].image;
       //     startingObjects[i].currentCard.power = startingHand[i].power;
       //
       // }
    }
}

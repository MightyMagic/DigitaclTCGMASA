using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardObject : MonoBehaviour
{
    public string gameplayManagerName;
    public GameplayManager gameplayManager;
    public CardSO currentCard;
    bool occupied = false;
    Image img;

    private void Start()
    {
        img = GetComponent<Image>();
        if(currentCard != null)
        {
            img.sprite = currentCard.image;
        }

        gameplayManager = GameObject.Find(gameplayManagerName).GetComponent<GameplayManager>();
    }

    private void Update()
    {
        if(!occupied & currentCard.image!= null)
        {
            occupied = true;
            img.sprite = currentCard.image;
        }
    }

    public void OnCardSelected()
    {
        if(occupied)
        {
            //currentCard = gameplayManager.cardObjectSelected.currentCard;
            
            //currentCard.mana = gameplayManager.cardObjectSelected.currentCard.mana;
            //currentCard.image = gameplayManager.cardObjectSelected.currentCard.image;
            //currentCard.power = gameplayManager.cardObjectSelected.currentCard.power;

            //gameplayManager.cardObjectSelected.currentCard = currentCard;

            this.gameplayManager.cardSelected.power = currentCard.power;
            this.gameplayManager.cardSelected.image = currentCard.image;
            this.gameplayManager.cardSelected.mana = currentCard.mana;

            gameplayManager.myTurn = true;
            gameplayManager.placementPhase = true;

            this.gameObject.transform.parent.gameObject.SetActive(false);

        }
    }

    //[Header("Power")]
    //int power;
    //TextMeshProUGUI powerText;
    //
    //[Header("Other")]
    //TextMeshProUGUI cardName;
    //
    //[Header("Visuals")]
    //Texture image;
    //
    //void Start()
    //{
    //    powerText = GetComponent<TextMeshProUGUI>();
    //}
    //
    //void ApplyValues(int power, string name)
    //{
    //    this.power = power;
    //    this.powerText.text = power.ToString();
    //    this.cardName.text = name;  
    //}

}

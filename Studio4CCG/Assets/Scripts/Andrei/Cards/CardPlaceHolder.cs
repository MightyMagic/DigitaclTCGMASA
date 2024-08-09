using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPlaceHolder : MonoBehaviour
{
    public int laneNumber;

    public bool occupied = false;
    public bool frontRow;

    public Color defaultColor;

    public CardSO currentCard;
    public Image cardImage;

    void Start()
    {
        defaultColor = transform.GetComponent<Renderer>().material.color;

        cardImage.transform.localScale = cardImage.transform.localScale / 2f;
    }

    void Update()
    {
        if(this.gameObject.tag == "Enemy" & !occupied)
        {
            occupied = true;
        }

        if(occupied & currentCard.image != null)
        {

        }
    }

    public void PlaceACard(CardSO card)
    {
        occupied = true;
        
        this.currentCard = card;
        cardImage.color = new Color(255, 255, 255, 1f);
        cardImage.sprite = card.image;
        currentCard.power = card.power;
        currentCard.mana = card.mana;
        currentCard.image = card.image;
        // spawn a texture of the card onto the placeholder
        // spawn a 3d model on top
    }
}

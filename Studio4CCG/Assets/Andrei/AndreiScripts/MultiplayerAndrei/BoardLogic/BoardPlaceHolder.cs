using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoardPlaceHolder : MonoBehaviour
{
    public bool isOccupied = false;
    public ColumnName column;
    public int rowIndex;

    public CardOnBoard cardOnBoard;

    //Not sure, but let it be extra rather than game not working
    public string ownerName;

    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI currentAttackText;
    public TextMeshProUGUI currentHpText;
    public TextMeshProUGUI descriptionText;

    private void Start()
    {
        ownerName = PlayerInformation.Instance.PlayerData.Name;

        if (!isOccupied)
        {
            gameObject.SetActive(false);
        }
    }

    public BoardPlaceHolder() { }

    public BoardPlaceHolder(bool isOccupied, ColumnName column, int rowIndex, CardOnBoard cardOnBoard, string ownerName)
    {
        this.isOccupied = isOccupied;
        this.column = column;
        this.rowIndex = rowIndex;
        this.cardOnBoard = cardOnBoard;
        this.ownerName = ownerName;
    }

    public void PopulatePlaceholder(CardInfo cardInfo, int attack, int hp)
    {
        if(isOccupied)
        {
            Debug.Log("Attempting to overlap placeholder");
        }
        else
        {
            isOccupied = true;
            cardOnBoard = new CardOnBoard(cardInfo, attack, hp);
        }
    }

    public void UpdateUI()
    {
        if(isOccupied)
        {
            if(cardOnBoard != null)
            {
                this.cardNameText.text = cardOnBoard.cardInfo.CardName;
                this.currentHpText.text = cardOnBoard.currentHP.ToString();
                this.currentAttackText.text = cardOnBoard.currentAttack.ToString();

                // Add description later
            }
        }
    }

}

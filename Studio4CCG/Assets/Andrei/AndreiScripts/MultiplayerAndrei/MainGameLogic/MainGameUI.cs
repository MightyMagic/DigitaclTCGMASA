using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] ClientCardDatabase cardsDB;

    [SerializeField] TextMeshProUGUI currentHp;
    [SerializeField] TextMeshProUGUI currentMana;

    [SerializeField] TextMeshProUGUI enemyName;

    public List<CardUIObject> cardsUI;

    [SerializeField] TextMeshProUGUI phaseDebug;

    private void Start()
    {
        for(int i = 0; i < cardsUI.Count; i++)
        {
            cardsUI[i].gameObject.SetActive(false);
        }
    }


    public void UpdateUI(PlayersStatesPacket psp)
    {
        bool playerFound = false;

        for (int i = 0; i < 2; i++)
        {
            if (psp.playerStates[i] != null)
            {
                Debug.Log("Comparing: '" + psp.playerStates[i].playerName.Trim() + "' with '" + PlayerInformation.Instance.PlayerData.Name.Trim() + "'");

                if (psp.playerStates[i].playerName == PlayerInformation.Instance.PlayerData.Name)
                {
                    Debug.Log("Player found: Updating UI for current player.");

                    currentHp.text = "HP: " + psp.playerStates[i].currentHp.ToString();
                    currentMana.text = "Mana: " + psp.playerStates[i].currentMana.ToString();
                    playerFound = true;
                    //break; // Exit loop once player is found
                }
                else
                {
                    Debug.Log("Enemy found: Updating UI for enemy.");

                    enemyName.text = "Your enemy is " + psp.playerStates[i].playerName;
                }
            }
            else
            {
                Debug.Log("psp.playerStates[" + i + "] is null.");
            }
        }
    }

    public void UpdateOffline(int Hp, int Mana)
    {
        currentHp.text = "HP: " + Hp.ToString();
        currentMana.text = "Mana: " + Mana.ToString();
    }

    public void UpdateCards(CardsInHandData cardsData) 
    {
        Debug.LogError("Showing latest cards, hooraaaay!!!!!");

        for (int i = 0; i < cardsUI.Count; i++)
        {
            cardsUI[i].gameObject.SetActive(false);

        }

        int lastIndex = 0;

        for(int i = 0; i < cardsData.cardsinHand.Length; i++)
        {
            if (cardsData.cardsinHand[i].Id == -1)
            {
                lastIndex = i;
                break;
            }
        }

        for(int i = 0; i < lastIndex; i++)
        {
            cardsUI[i].gameObject.SetActive(true);
            GetCardFromDB(i, cardsData.cardsinHand[i].CardName);
        }
    }

    void GetCardFromDB(int indexInHand, string dbName)
    {
        for(int i = 0; i < cardsDB.deckLists[0].CardsInDeck.Count; i++)
        {
            if(dbName == cardsDB.deckLists[0].CardsInDeck[i].CardName)
            {
                cardsUI[indexInHand].DisplayCard(cardsDB.deckLists[0].CardsInDeck[i].CardName,
                    cardsDB.deckLists[0].CardsInDeck[i].Description,
                    cardsDB.deckLists[0].CardsInDeck[i].Hp,
                    cardsDB.deckLists[0].CardsInDeck[i].ManaCost);

                break;
            }
        }
    }
}

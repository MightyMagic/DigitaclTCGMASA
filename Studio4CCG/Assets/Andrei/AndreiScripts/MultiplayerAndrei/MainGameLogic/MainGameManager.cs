using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    [Header("My cards")]
    public CardsInHandData cardsInHandData;

    [Header("Stats")]
    [SerializeField] int startHp;
    [SerializeField] int startMana;

    [SerializeField] int maximumMana;

    public int startTurnMana;
    public int currentMana;

    [Header("UI")]
    [SerializeField] MainGameUI gameUI;

    [SerializeField] GameObject deckChoiceObject;
    [SerializeField] Button deckButtonOne;
    [SerializeField] Button deckButtonTwo;

    void Start()
    {
        ClientNetworkManager.Instance.PlayersStatesUpdatedEvent += OnPlayersStateChanged;

        deckChoiceObject.SetActive(true);

        deckButtonOne.onClick.AddListener(() => ChooseDeck(0));
        deckButtonTwo.onClick.AddListener(() => ChooseDeck(1));

        ClientNetworkManager.Instance.SendPacket(new PlayerStateInfoPacket(PlayerInformation.Instance.PlayerData, 
            startHp, startMana, 
            PlayerInformation.Instance.PlayerData.Name).Serialize());
    }

    void Update()
    {
        
    }

    void OnPlayersStateChanged(PlayersStatesPacket playerStatesPacket)
    {
        //Update UI
        gameUI.UpdateUI(playerStatesPacket);
    }

    void ChooseDeck(int deckIndex)
    {
        Debug.LogWarning(PlayerInformation.Instance.PlayerData.Name + " chooses deck #" +  deckIndex);

        //Disabling buttons and closing the choice window
        deckButtonOne.onClick.RemoveAllListeners();
        deckButtonTwo.onClick.RemoveAllListeners();
        deckChoiceObject.SetActive(false);

        // send the packet to alert the server
        ClientNetworkManager.Instance.SendPacket(new DeckChoicePacket(PlayerInformation.Instance.PlayerData, deckIndex).Serialize());
        // receiving my first cards information

        //// starting the game
        //ClientNetworkManager.Instance.SendPacket(new PlayerStateInfoPacket(PlayerInformation.Instance.PlayerData,
        //    startHp, startMana,
        //    PlayerInformation.Instance.PlayerData.Name).Serialize());
    }

    void BeginningOfMyTurn()
    {

    }

    void UpdateUI()
    {

    }

    void IncreaseMana(int mana)
    {
        if (startTurnMana < maximumMana)
        {
            startTurnMana++;
        }

        currentMana = startTurnMana;
    }
}

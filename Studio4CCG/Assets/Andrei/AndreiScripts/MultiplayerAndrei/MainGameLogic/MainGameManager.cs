using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public int currentHp;

    [Header("UI")]
    public MainGameUI gameUI;

    [SerializeField] GameObject deckChoiceObject;
    [SerializeField] Button deckButtonOne;
    [SerializeField] Button deckButtonTwo;

    [SerializeField] string gameOverSceneName;


    void Start()
    {
        ClientNetworkManager.Instance.PlayersStatesUpdatedEvent += OnPlayersStateChanged;
        ClientNetworkManager.Instance.PlayerTurnEvent += IncreaseMana;
        ClientNetworkManager.Instance.GameOverEvent += GameIsOver;

        deckChoiceObject.SetActive(true);

        deckButtonOne.onClick.AddListener(() => ChooseDeck(0));
        deckButtonTwo.onClick.AddListener(() => ChooseDeck(1));

        currentHp = startHp;
        currentMana = startMana;

        ClientNetworkManager.Instance.SendPacket(new PlayerStateInfoPacket(PlayerInformation.Instance.PlayerData, 
            startHp, startMana, 
            PlayerInformation.Instance.PlayerData.Name).Serialize());
    }

    void Update()
    {
        
    }

    void GameIsOver(GameOverPacket gop)
    {
        PlayerPrefs.SetString("Winner", gop.winnerName);
        SceneManager.LoadScene(gameOverSceneName);
    }

    void OnPlayersStateChanged(PlayersStatesPacket playerStatesPacket)
    {
        // Update player stats
        for (int i = 0; i < 2; i++)
        {
            if (playerStatesPacket.playerStates[i] != null)
            {
                if (playerStatesPacket.playerStates[i].playerName == PlayerInformation.Instance.PlayerData.Name)
                {
                    Debug.Log("Player found: Updating UI for current player.");

                    currentHp = playerStatesPacket.playerStates[i].currentHp;
                    currentMana = playerStatesPacket.playerStates[i].currentMana;

                    //break; // Exit loop once player is found
                }
                else
                {
                    //Debug.Log("psp.playerStates[" + i + "] is null.");
                }
            }
        }

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

    void IncreaseMana(PlayerTurnPacket ptp)
    {
        if(ptp.playersTurnName == PlayerInformation.Instance.PlayerData.Name)
        {
            //if (startMana == 0)
            //    startMana = 1;

            if (startTurnMana < maximumMana)
            {
                startTurnMana++;
            }

            currentMana = startTurnMana;

            gameUI.UpdateOffline(currentHp, currentMana);
        }   
    }
}

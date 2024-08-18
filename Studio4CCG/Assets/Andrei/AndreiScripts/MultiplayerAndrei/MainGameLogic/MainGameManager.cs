using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int startHp;
    [SerializeField] int startMana;

    [SerializeField] int maximumMana;

    public int startTurnMana;
    public int currentMana;

    [Header("UI")]
    [SerializeField] MainGameUI gameUI;

    void Start()
    {
        ClientNetworkManager.Instance.PlayersStatesUpdatedEvent += OnPlayersStateChanged;

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

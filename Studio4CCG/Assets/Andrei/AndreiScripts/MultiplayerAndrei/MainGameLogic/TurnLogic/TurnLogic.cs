using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLogic : MonoBehaviour
{
    public bool isMyTurn = false;
    [SerializeField] TurnUI turnUI;

    [Header("Turn Conditions")]
    public bool turnJustStarted = false;
    public bool listeningToInput = false;


    private void Start()
    {
        turnUI.DeactivateTurnUI();
        ClientNetworkManager.Instance.PlayerTurnEvent += StartTurn;
    }

    private void Update()
    {
        if (isMyTurn)
        {
            //if(!turnJustStarted)
            //{
            //    turnJustStarted = true;
            //    turnUI.ActivateTurnUI();
            //    listeningToInput = true;
            //}
            //
            //if(listeningToInput)
            //{
            //    // Here I process all mouse input for placing the card
            //    ProcessInput();
            //}
        }
    }

    void ProcessInput()
    {
        // Input processing and turn actions

        // Finish the turn under condition
        //StartCoroutine(FinishTurnCoroutine());
    }

    public void StartTurn(PlayerTurnPacket ptp)
    {
        if (!isMyTurn & ptp.playersTurnName == PlayerInformation.Instance.PlayerData.Name)
        {
            isMyTurn = true;
            turnJustStarted = true;
            turnUI.ActivateTurnUI();
            
            listeningToInput = true;
        }
    }

    public void EndTurn()
    {
        turnJustStarted = false;
        listeningToInput = false;
        isMyTurn = false;
        // Send the packet to server that my turn is over
        ClientNetworkManager.Instance.SendPacket(new PlayerTurnPacket(PlayerInformation.Instance.PlayerData, PlayerInformation.Instance.PlayerData.Name).Serialize());
        turnUI.DeactivateTurnUI();
    }

    public IEnumerator FinishTurnCoroutine()
    {
        yield return new WaitForEndOfFrame();
        //EndTurn(PlayerInformation.Instance.PlayerData);
    }


}

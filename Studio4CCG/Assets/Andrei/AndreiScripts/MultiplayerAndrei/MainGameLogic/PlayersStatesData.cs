
using UnityEngine;

public class PlayersStatesData : MonoBehaviour
{
    public PlayerState[] playerStates = new PlayerState[2];
    [SerializeField] LobbyData lobbyData;


    private void Start()
    {
        //playerStates.AddRange(new PlayerState[2]);
        for (int i = 0; i < playerStates.Length; i++)
        {
            playerStates[i] = new PlayerState();
            playerStates[i].playerName = "";
        }
    }

    public void ReceivedPlayerState(PlayerState playerState)
    {
        // brute forcing that the players names are here 100%
        for(int i = 0; i < 2;i++)
        {
            if (lobbyData.PlayersData[i].Name != "")
            {
                this.playerStates[i].playerName = lobbyData.PlayersData[i].Name;
            }
        }
       

        for (int i = 0; i < playerStates.Length; i++)
        {

            if (playerStates[i].playerName == playerState.playerName)
            {
                playerStates[i].currentHp = playerState.currentHp;
                playerStates[i].currentMana = playerState.currentMana;

                //if (playerStates[i].playerName == "")
                //{
                //    playerStates[i].playerName = playerState.playerName;
                //}
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentHp;
    [SerializeField] TextMeshProUGUI currentMana;

    [SerializeField] TextMeshProUGUI enemyName;


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
}

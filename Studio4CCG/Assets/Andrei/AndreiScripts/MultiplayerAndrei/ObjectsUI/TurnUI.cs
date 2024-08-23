using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnUI : MonoBehaviour
{
    [SerializeField] Button endTurnButton;
    [SerializeField] TextMeshProUGUI phaseUI;
    private void Start()
    {
        //endTurnButton.gameObject.SetActive(false);
    }

    public void ActivateTurnUI()
    {
        endTurnButton.gameObject.SetActive(true);
        //phaseUI.text = "Your turn";
    }

    public void DeactivateTurnUI()
    {
        endTurnButton.gameObject.SetActive(false);
        //phaseUI.text = "Opponent's turn";
    }

    public void DisplayPhase(bool b, string text)
    {
        if (b)
        {
            phaseUI.gameObject.SetActive(true);
            phaseUI.text = text;
        }
        else
        {
            phaseUI.gameObject.SetActive(false);
        }
    }
}

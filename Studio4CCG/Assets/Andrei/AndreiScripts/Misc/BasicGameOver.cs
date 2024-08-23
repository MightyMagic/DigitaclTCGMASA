using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasicGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameOverText;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Winner"))
        {
            gameOverText.text = PlayerPrefs.GetString("Winner") + " won!";
        }
    }
}

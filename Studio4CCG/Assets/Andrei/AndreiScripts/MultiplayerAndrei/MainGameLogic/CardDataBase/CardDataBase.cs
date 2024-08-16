using AndreiMultiplayer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : MonoBehaviour
{
    [SerializeField] List<DeckData> deckLists;
}

[System.Serializable]
public class DeckData
{
    [SerializeField] string deckName;
    [SerializeField] List<CardScriptableObject> CardsInDeck = new List<CardScriptableObject>();
}

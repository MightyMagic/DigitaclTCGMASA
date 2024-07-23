using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Board properties")]
    [SerializeField] GameObject planeObject;
    [SerializeField] int numberOfLanes;
    [SerializeField] GameplayManager gameplayManager;

    [Header("Cards formation")]
    [SerializeField] int maxCardsPerLane;

    [SerializeField] GameObject cardObject;
    [SerializeField] GameObject enemyObject;

    [SerializeField] float distanceBetweenCards;

    [SerializeField] List<GameObject> myCardPlaceHolders = new List<GameObject>();
    [SerializeField] List<GameObject> enemyCardPlaceHolders = new List<GameObject>();

    [SerializeField] List<CardSO> enemyCards;


    void Start()
    {
        SpawnBoard();
        SpawnEnemyBoard();
    }
    void SpawnBoard()
    {
        float sizeX = planeObject.transform.localScale.x;
        float sizeZ = planeObject.transform.localScale.z;

        float placeHolderSizeX = sizeX / numberOfLanes;
        float placeHolderSizeZ = sizeZ / ((maxCardsPerLane) * 2);

        int numberOfPlaceholders = numberOfLanes * maxCardsPerLane;

        float yCoordinate = planeObject.transform.position.y + planeObject.transform.localScale.y;

        Vector3 centerOfBoard = planeObject.transform.position;
        Vector3 startPosition = centerOfBoard + new Vector3( (-sizeX / 2) + placeHolderSizeX / 2, yCoordinate, -placeHolderSizeZ / 2);

        for(int i = 0; i < numberOfPlaceholders; i++)
        {
            GameObject newCardPlace = Instantiate(cardObject, startPosition, Quaternion.identity);
            newCardPlace.transform.localScale = new Vector3(placeHolderSizeX * 0.8f, cardObject.transform.localScale.y, placeHolderSizeZ * 0.8f);
            newCardPlace.transform.name = "My card placeholder " + i;
            myCardPlaceHolders.Add(newCardPlace);

            if(i < numberOfLanes)
            {
                newCardPlace.GetComponent<CardPlaceHolder>().frontRow = true;
            }
            else
            {
                newCardPlace.GetComponent<CardPlaceHolder>().frontRow = false;
            }

            newCardPlace.GetComponent<CardPlaceHolder>().laneNumber = (i % numberOfLanes) + 1;

            if ((i % numberOfLanes == numberOfLanes - 1) && i > 0)
            {
                startPosition = centerOfBoard + new Vector3((-sizeX / 2) + placeHolderSizeX / 2, yCoordinate, -placeHolderSizeZ * ((i + 1) / numberOfLanes) - placeHolderSizeZ / 2);
            }
            else
            {
                startPosition = startPosition + new Vector3(placeHolderSizeX, 0f, 0f);
            }
        }
    }

    void SpawnEnemyBoard()
    {
        float sizeX = planeObject.transform.localScale.x;
        float sizeZ = planeObject.transform.localScale.z;

        float placeHolderSizeX = sizeX / numberOfLanes;
        float placeHolderSizeZ = sizeZ / ((maxCardsPerLane) * 2);

        int numberOfPlaceholders = numberOfLanes * maxCardsPerLane;

        float yCoordinate = planeObject.transform.position.y + planeObject.transform.localScale.y;

        Vector3 centerOfBoard = planeObject.transform.position;
        Vector3 startPosition = centerOfBoard + new Vector3((-sizeX / 2) + placeHolderSizeX / 2, yCoordinate, +placeHolderSizeZ / 2);

        for (int i = 0; i < numberOfPlaceholders; i++)
        {
            GameObject newCardPlace = Instantiate(enemyObject, startPosition, Quaternion.identity);
            newCardPlace.transform.localScale = new Vector3(placeHolderSizeX * 0.8f, cardObject.transform.localScale.y, placeHolderSizeZ * 0.8f);
            newCardPlace.transform.name = "Enemy card placeholder " + i;
            enemyCardPlaceHolders.Add(newCardPlace);

            if (i < numberOfLanes)
            {
                newCardPlace.GetComponent<CardPlaceHolder>().frontRow = true;
            }
            else
            {
                newCardPlace.GetComponent<CardPlaceHolder>().frontRow = false;
            }

            newCardPlace.GetComponent<CardPlaceHolder>().laneNumber = (i % numberOfLanes) + 1;

            if ((i % numberOfLanes == numberOfLanes - 1) && i > 0)
            {
                startPosition = centerOfBoard + new Vector3((-sizeX / 2) + placeHolderSizeX / 2, yCoordinate, placeHolderSizeZ * ((i + 1) / numberOfLanes) + placeHolderSizeZ / 2);
            }
            else
            {
                startPosition = startPosition + new Vector3(placeHolderSizeX, 0f, 0f);
            }
        }

        for(int i = 0; i < numberOfLanes; i++)
        {
            CardPlaceHolder newCardPlace = enemyCardPlaceHolders[i].GetComponent<CardPlaceHolder>();
            newCardPlace.occupied = false;
            newCardPlace.PlaceACard(enemyCards[Random.Range(0, enemyCards.Count)]);
           //newCardPlace.currentCard.power = enemyCards[Random.Range(0, enemyCards.Count)].power;
           //newCardPlace.currentCard.image = enemyCards[Random.Range(0, enemyCards.Count)].image;
           //newCardPlace.currentCard.mana = enemyCards[Random.Range(0, enemyCards.Count)].mana;
        }
    }

    public IEnumerator AttackSequence()
    {

        yield return new WaitForSeconds(0.6f);

        for (int i = 0; i < numberOfLanes; i++)
        {
            CardPlaceHolder enemyCardPlace = enemyCardPlaceHolders[i].GetComponent<CardPlaceHolder>();
            CardPlaceHolder playerCardPlace = myCardPlaceHolders[i].GetComponent<CardPlaceHolder>();

            if (enemyCardPlace.occupied & playerCardPlace.occupied)
            {
                playerCardPlace.cardImage.gameObject.transform.localScale = 2f * playerCardPlace.cardImage.gameObject.transform.localScale;
                enemyCardPlace.cardImage.gameObject.transform.localScale *= 2f;

                yield return new WaitForSeconds(1f);

                if ((int)enemyCardPlace.currentCard.power > (int)playerCardPlace.currentCard.power)
                {
                    //Destroy(playerCardPlace.gameObject);
                    playerCardPlace.cardImage.color = new Color(1, 1, 1, 0);
                    playerCardPlace.occupied = false;
                    playerCardPlace.currentCard.image = null;
                }
                else if((int)enemyCardPlace.currentCard.power < (int)playerCardPlace.currentCard.power)
                {
                    //Destroy(enemyCardPlace.gameObject);
                    enemyCardPlace.cardImage.color = new Color(i, 1, 1, 0);
                    enemyCardPlace.occupied = false;
                    enemyCardPlace.currentCard.image = null;
                }
                else
                {
                    playerCardPlace.cardImage.color = new Color(1, 1, 1, 0);
                    playerCardPlace.occupied = false;
                    //playerCardPlace.currentCard.image = null;

                    enemyCardPlace.cardImage.color = new Color(1, 1, 1, 0);
                    enemyCardPlace.occupied = false;
                    //enemyCardPlace.currentCard.image = null;
                }

                yield return new WaitForSeconds(1f);

                playerCardPlace.cardImage.gameObject.transform.localScale = 0.5f * playerCardPlace.cardImage.gameObject.transform.localScale;
                enemyCardPlace.cardImage.gameObject.transform.localScale *= 0.5f;
            }

           
            yield return new WaitForSeconds(0.5f);

            

        }

        yield return new WaitForSeconds(1f);

        gameplayManager.attackPhase = false;
        gameplayManager.placementPhase = false;
        gameplayManager.cardsInHand.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        foreach (GameObject go in myCardPlaceHolders)
        {
            Gizmos.DrawWireCube(go.transform.position, go.transform.localScale / 0.8f);
        }

        Gizmos.color = Color.red;
        foreach (GameObject go in enemyCardPlaceHolders)
        {
            Gizmos.DrawWireCube(go.transform.position, go.transform.localScale / 0.8f);
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(planeObject.transform.position, planeObject.transform.localScale);
    }
}

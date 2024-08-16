using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPhase : MonoBehaviour
{
    // Update is called once per frame
    public int tilesLeft = 0;
    bool startCheccking = false;
    TileList tileList;
    float timer;
    void Start()
    {

    }
    private void SearchNode(int i, TileNode currentNode)
    {
        TileNode searchNode = tileList.tileList[i - 1].GetComponent<TileNode>();

        switch (searchNode.occupieState)
        {

            case OccupieState.occupied:

                // CardID Owner does not Match presented OwnerID Attack.
                if (searchNode.storCard.GetComponent<BaseCard>()._ownerID != NetworkManager.instance.playerOrder)
                {
                    // ------Request attack-------//

                    Debug.Log("Request Attack on: " + searchNode.storCard.name);

                    // ------Request attack-------//

                }


                break;

            case OccupieState.empty:
                Debug.Log(currentNode.name+ " Moves Forwad");

                //---Move Card Forward---//
                MoveForwad(i,currentNode);

                break;

            case OccupieState.blocked:
                Debug.Log("Step1 Blocked");

                // ------Nothing for now------//

                break;




        }
        Debug.Log("End Search");

    }

    void RequestkHealthDamage()
    {

    }

    void MoveForwad(int i, TileNode currentNode)
    {
        TileNode searchNode = tileList.tileList[i - 1].GetComponent<TileNode>();

        //Update Position
        StartCoroutine(LerpToTile(i,searchNode.transform));


        //Update Tiles Stored Cards
        searchNode.storCard = currentNode.storCard.transform;
        currentNode.storCard = null;

        //Update States
        currentNode.occupieState = OccupieState.empty;
        searchNode.occupieState= OccupieState.occupied;
    }
    IEnumerator LerpToTile(int i,Transform newTilePos)
    {
        timer = 0.3f;
        float currentTimer = timer;
        Transform currentnode = tileList.tileList[i].GetComponent<TileNode>().storCard.parent.transform;

        while (currentTimer >= 0)
        {

            //lerp to tile
            currentnode.position = Vector3.Lerp(newTilePos.position, currentnode.position, currentTimer / timer);

            //offset position above tile
            currentnode.position = new Vector3(currentnode.position.x, currentnode.position.y + 0.1f, currentnode.position.z);


            //countdown
            currentTimer -= 1 * Time.deltaTime;
            yield return null;
        }

    }

    void ProcessTiles()
    {

        for (int i = 0, j = 0; i < tilesLeft; i++, j++)
        {
            TileNode node = tileList.tileList[i].GetComponent<TileNode>();
            //cards need to search in front of them
            if (j > 0)
            {

                //check tile node for my card
                //if empty skip
                if (j == 3)
                {

                    //reset
                    j = -1;
                }
                //Card At the first tile.
                if (node.occupieState == OccupieState.occupied && node.storCard.GetComponent<BaseCard>()._ownerID == NetworkManager.instance.playerOrder)
                {

                    //search the tile infront of me
                    //it decided to attack or move forward
                    SearchNode(i, node);

                }

            }
            else
            {

                if (node.occupieState == OccupieState.occupied && node.storCard.GetComponent<BaseCard>()._ownerID == NetworkManager.instance.playerOrder)
                {

                    // ------Request to attack directly-------//

                    Debug.Log(tileList.tileList[i].name + " Attack Directly");

                    RequestkHealthDamage();

                    // ------Request to attack directly-------//
                }

            }


        }

        //when finished disable script for my next round.

        tilesLeft = 0;
        this.enabled = false;

    }

    //enable to start processing tiles.
    private void OnEnable()
    {
        tileList = GameManager.Instance.tileList;
        tilesLeft = tileList.tileList.Count;
        ProcessTiles();

    }


}

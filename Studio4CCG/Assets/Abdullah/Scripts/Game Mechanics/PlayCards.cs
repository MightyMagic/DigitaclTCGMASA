using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCards : MonoBehaviour
{
    bool isPlayable = false;
    LayerMask tileMask=6;
    public TileList tileList;
    // Start is called before the first frame update
    void Start()
    {
        tileList = FindObjectOfType<TileList>();


    }
    void Update()
    {
        // Playable & Mouse Clicked.
        if (isPlayable && Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 20, tileMask))
            {
                isPlayable = false;
                PlayMe();
                //disable the effect.
            }
            else
            {
                isPlayable = false;
                foreach (Transform tile in tileList.tileList)
                {
                    TileNode tileNode = tile.GetComponent<TileNode>();
                    var mainModule = tile.GetComponent<ParticleSystem>().main;
                    tile.GetComponent<ParticleSystem>().Stop();

                    // Disable the loop
                    mainModule.loop = false;

                }

            }


        }
        // Disable the effect. 
        // reset.



    }




    public void ChoseWhereToPlay()
    {
        isPlayable =true;
        //highlight all availlable spot.
        foreach (Transform tile in tileList.tileList)
        {
            TileNode tileNode = tile.GetComponent<TileNode>();
            if (tileNode.occupieState == TileNode.OccupieState.empty)
            {
                var mainModule = tile.GetComponent<ParticleSystem>().main;
                tile.GetComponent<ParticleSystem>().Play();

                // Disable the loop
                mainModule.loop = true;
            }

        }


    }

    //chose a spot to play
    public void PlayMe()
    {
        //if it's not tile give a feedback sound.
        // Move The Card To The Tile.

        // Update The Tile.




    }



}

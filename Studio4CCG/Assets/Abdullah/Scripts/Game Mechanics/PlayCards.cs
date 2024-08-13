using System.Collections;
using UnityEngine;

public class PlayCards : MonoBehaviour
{
    bool isPlayable = false;
    public TileList tileList;
    float timer = 2;

    // Start is called before the first frame update
    void Start()
    {
        tileList = FindObjectOfType<TileList>();


    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        // Playable & Mouse Clicked.
        if (isPlayable && Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log("Step1");

            LayerMask tileMask = LayerMask.GetMask("Tile");

            if (Physics.Raycast(ray, out hit,Mathf.Infinity, tileMask) && hit.transform.GetComponent<TileNode>().occupieState==TileNode.OccupieState.empty)
            {
                Debug.Log("Step2");

                //Update Card Tag
                transform.tag = "Tile";
                transform.GetChild(0).gameObject.tag = "Tile";
                PlayMe(hit.collider.transform);

                //prevent the button from being triggered randomly
                isPlayable = false;
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



    //OnHand Card Button.
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
    public void PlayMe(Transform hit)
    {
        Debug.Log("play the card");

        // Move The Card To The Tile.
        StartCoroutine(LerpToTile(hit));

        // Update The Tile.
        hit.GetComponent<TileNode>().occupieState = TileNode.OccupieState.occupied;
        transform.transform.parent = tileList.transform.parent.transform;
        //reset aniamtion.
        transform.GetComponent<Animator>().SetBool("isSelected", false);
        transform.GetComponent<Animator>().SetInteger("hover",0);
        GameManager.Instance.cardsOnHand.cardsOnHand.Remove(gameObject);
        //reset
        foreach (Transform tile in tileList.tileList)
        {
            TileNode tileNode = tile.GetComponent<TileNode>();
            var mainModule = tile.GetComponent<ParticleSystem>().main;
            tile.GetComponent<ParticleSystem>().Stop();
            // Disable the loop
            mainModule.loop = false;

        }

    }

    IEnumerator LerpToTile(Transform hit)
    {
        timer = 0.3f;
        float currentTimer = timer;
        Quaternion startRotation = transform.rotation;
        while (currentTimer >= 0)
        {

            //lerp to tile
            transform.position = Vector3.Lerp(hit.position, transform.position, currentTimer / timer);

            //offset position above tile
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);

            //lerp to tile rotation which is 0
            transform.rotation = Quaternion.Lerp(Quaternion.identity, startRotation, 0.2f);

            //countdown
            currentTimer -= 1 * Time.deltaTime;
            yield return null;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    Transform selected;
    bool firsttimeSelected=true;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if (firsttimeSelected == false && hit.collider.name != selected.name)
                { 

                selected = hit.collider.transform;
                    Debug.Log("Hit " + hit.collider.name);

                    selected.GetComponent<ParticleSystem>().Play();
                    //highlight tile 
                    //highlight cards on hand & aniamtion.
                }
                else if (firsttimeSelected == true)
                {
                    firsttimeSelected = false;
                    selected = hit.collider.transform;
                    Debug.Log("First Hit " + hit.collider.name);
                    selected.GetComponent<ParticleSystem>().Play();

                }
            }



        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            

        }
    }

    void CheckCard()
    {



    }
}

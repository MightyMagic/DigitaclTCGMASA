using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    Transform selected;
    Transform deselect;
    Transform buttons;
    bool firsttimeSelected =true;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        //highlight selection
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if (firsttimeSelected == false && hit.collider.name != selected.name)
                {
                    if (hit.collider.transform.tag !="Button")
                    {
                        selected = hit.collider.transform;
                    }
                    selected.GetComponent<ParticleSystem>().Play();
                    //highlight tile 
                    //highlight cards on hand & aniamtion.
                }
                else if (firsttimeSelected == true)
                {
                    firsttimeSelected = false;
                    selected = hit.collider.transform;
                    selected.GetComponent<ParticleSystem>().Play();
                    deselect = selected;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            CheckCard();

        }
    }

    void CheckCard()
    {
        Debug.Log("CLICKED "+selected.name);
        if (deselect.name != selected.name&& selected.tag!= "Button")
        {
            buttons = deselect.GetChild(0);
            buttons.gameObject.SetActive(false);

            deselect = selected;
            //return to normal

        }

        buttons = selected.GetChild(0);
        buttons.gameObject.SetActive(true);

        Debug.Log("---BUTTON---- " + buttons.name);


        //show cards info. 
        //showbuttons
    }



}

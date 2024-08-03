using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHandsAnimation : MonoBehaviour
{
    Transform selected, deselect,active=null;
    Ray ray;
    RaycastHit hit;
    bool firsttimeSelected=true;
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit) && hit.collider != null && hit.collider.tag == "OnHands")
        {
            if (firsttimeSelected == true)
            {

                firsttimeSelected = false;
                selected = hit.collider.transform;
                active = selected;
                selected.parent.GetComponent<Animator>().SetInteger("hover", 1);
                deselect = selected;
            }
            else
            {

                selected = hit.collider.transform;
                selected.parent.GetComponent<Animator>().SetInteger("hover", 1);

                if (selected != deselect)
                {
                    deselect.parent.GetComponent<Animator>().SetInteger("hover", 0);
                    deselect = selected;
                }

            }
        

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(hit.collider.tag == "OnHands")
            {
                selected.parent.GetComponent<Animator>().SetBool("isSelected", true);

                if (!active)
                {
                    active = selected;

                }
                if (active != selected)
                {

                    active.parent.GetComponent<Animator>().SetBool("isSelected", false);
                    active.parent.GetComponent<Animator>().SetInteger("hover", 0);

                }
                active = selected;

            }
            else
            {
                active.parent.GetComponent<Animator>().SetBool("isSelected", false);    
                active.parent.GetComponent<Animator>().SetInteger("hover", 0);
                deselect.parent.GetComponent<Animator>().SetInteger("hover", 0);
                selected.parent.GetComponent<Animator>().SetInteger("hover", 0);




            }

        }
    
    
    }


}
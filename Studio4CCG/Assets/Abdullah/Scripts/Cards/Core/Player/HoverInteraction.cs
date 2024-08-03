using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoverInteraction : MonoBehaviour
{
     Transform selected;
     Transform deselect;
    bool isFirst=true;
    Ray ray;
    float timer;
    void Start()
    {
        selected= transform;
        deselect = transform;

    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //mouse position
        timer += 1 * Time.deltaTime;
        HighlightTiles();

    }

     void HighlightTiles()
    {
        RaycastHit hit;

        //highlight selection
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                if (hit.collider.name != selected.name)
                {
                     if (isFirst==false && hit.collider.transform.tag == "Tile" || hit.collider.transform.tag == "Deck")
                    {
                        timer = 0;

                        selected = hit.collider.transform;
                        selected.GetComponent<ParticleSystem>().Play();
                        deselect.GetComponent<ParticleSystem>().Stop();
                        Debug.Log("selected+++" + selected.name);
                        Debug.Log("stopped---"+ deselect.name);
                        deselect = selected;

                    }
                    else if(isFirst && hit.collider.transform.tag == "Tile" || hit.collider.transform.tag == "Deck")
                    {
                        Debug.Log("First time");
                        timer = 0;
                        deselect = hit.collider.transform;
                        isFirst = false;

                    }


                }
            }


        }
        if (timer >= 3)
        {
            selected.GetComponent<ParticleSystem>().Stop();

            timer = 0;
        }

    }




}

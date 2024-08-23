using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSettings : MonoBehaviour
{
   public GameObject settingUI;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(settingUI.activeSelf==true)
            {
                settingUI.active = false;

            }

            else
            {
                settingUI.active = true;
            }

        }
    }
}

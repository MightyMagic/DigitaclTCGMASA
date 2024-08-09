using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicExploration : MonoBehaviour
{
    [SerializeField] GameObject characterObject;
    [SerializeField] GameObject cameraObject;

    Vector3 initialPosition;

    [SerializeField] Transform startCameraPos;
    [SerializeField] Transform charCameraPosition;

    void Start()
    {
        HideCharacter();
        //RevealCharacter();
    }

    void Update()
    {
        
    }

    public void RevealCharacter()
    {
        characterObject.SetActive(true);

       cameraObject.gameObject.SetActive(true);

       cameraObject.transform.position = charCameraPosition.position;
       cameraObject.transform.rotation = charCameraPosition.rotation;

       cameraObject.transform.parent = charCameraPosition;

        characterObject.transform.position = initialPosition;
    }

    public void HideCharacter()
    {
        characterObject.SetActive(false);
        cameraObject.gameObject.SetActive(true);
        
        cameraObject.transform.position = startCameraPos.position;
        cameraObject.transform.rotation = startCameraPos.rotation;
        
        cameraObject.transform.parent = startCameraPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellObject : MonoBehaviour
{
    [SerializeField] GameObject defaultObject;

    public Color avaliableColor;
    public Color lockedColor;
    public Color defaultColor;

    GameObject currentTerrain;
    GameObject startObject;
    public bool occupied = false;
    void Start()
    {
        startObject = Instantiate(defaultObject, transform.position, Quaternion.identity);
        startObject.transform.parent = transform;
        ChangeColorOfTile(defaultColor);

        this.GetComponent<BoxCollider>().size = new Vector3(defaultObject.transform.localScale.x, 1f, defaultObject.transform.localScale.x);
    }

   
    void Update()
    {
        
    }

    public void PlaceTerrain(GameObject terrainObject, Vector3 rotation)
    {
        Destroy(startObject);

        Vector3 spawnPoint = transform.position;
        currentTerrain = Instantiate(terrainObject, spawnPoint, Quaternion.Euler(rotation));
        currentTerrain.transform.parent = transform;


        occupied = true;
    }

    public void DestroyTerrain()
    {
        Destroy(currentTerrain);

        startObject = Instantiate(defaultObject, transform.position, Quaternion.identity);
        startObject.transform.parent = transform;

        occupied= false;
    }

    public void ChangeColorOfTile(Color color)
    {
        if (transform.GetChild(0))
        {
            Renderer renderer = transform.GetChild(0).gameObject.GetComponent<Renderer>();
            renderer.material.color = color;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TerrainConfigurator : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TerrainBuilder terrainBuilder;

    [SerializeField] List<TerrainType> terrainTypes;

    int localIndex = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if(terrainBuilder.placingTerrain)
        {
            text.gameObject.SetActive(true);
            text.text = "Current tile is: " + terrainTypes[localIndex].name;
        }
        else
        {
            text.text = "";
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            localIndex = localIndex + terrainTypes.Count;

            localIndex = (localIndex - 1) % terrainTypes.Count;
            terrainBuilder.terrainIndex = localIndex;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            localIndex = (localIndex + 1) % terrainTypes.Count;
            terrainBuilder.terrainIndex = localIndex;
        }
    }
}

[System.Serializable]
public class TerrainType
{
    public int index;
    public string name;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] int xCells;
    [SerializeField] int zCells;

    [SerializeField] GameObject defaultTerrain;

    [SerializeField] GridCellObject cellObject;

    void Start()
    {
        Vector3 spawnPoint = Vector3.zero;

        for (int i = -xCells / 2; i < xCells / 2; i++)
        {
            for(int j = -zCells / 2; j < zCells / 2; j++)
            {
                spawnPoint = Vector3.zero;
                spawnPoint += new Vector3(i * defaultTerrain.transform.localScale.x * 1.4f, 0f, j * defaultTerrain.transform.localScale.z * 1.4f);

                Instantiate(cellObject, spawnPoint, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        
    }
}

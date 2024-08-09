using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndreiScripts
{
    public class TerrainBuilder : MonoBehaviour
    {
        public bool placingTerrain = false;

        [SerializeField] LayerMask cellLayer;
        [SerializeField] LayerMask backgroundLayer;

        GridCellObject cellObject;
        public Color defaultColor;

        bool pointingAtCell = false;
        public bool exploring = false;
        [SerializeField] BasicExploration exploration;

        [SerializeField] List<GameObject> terrainObjects;
        public int terrainIndex = 0;

        [SerializeField] GameObject hintUI;
        void Start()
        {
            hintUI.SetActive(true);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T) && !placingTerrain & !exploring)
            {
                placingTerrain = true;
                hintUI.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.E) && !placingTerrain & !exploring)
            {
                exploring = true;
                exploration.RevealCharacter();

            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    exploring = false;
                    exploration.HideCharacter();
                }
            }

            if (placingTerrain)
            {
                TerrainInteraction();
            }

        }

        public void TerrainInteraction()
        {

            TrackMouse();


            if (cellObject != null)
            {
                if (!cellObject.occupied)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        GameObject chosenTerrain = terrainObjects[0];
                        cellObject.PlaceTerrain(terrainObjects[terrainIndex], Vector3.zero);
                        placingTerrain = false;
                        hintUI.SetActive(true);

                        //defaultColor = chosenTerrain.GetComponent<Renderer>().material.color;
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        cellObject.DestroyTerrain();
                        placingTerrain = false;
                        hintUI.SetActive(true);
                        //defaultColor = cellObject.transform.GetChild(0).GetComponent<Renderer>().material.color;
                    }
                }
            }
        }

        public void TrackMouse()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.black);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, cellLayer))
            {

                Debug.Log("Hit object name: " + hit.transform.name);
                GameObject currentTarget = hit.transform.gameObject;
                if (currentTarget.GetComponent<GridCellObject>() != null & pointingAtCell == false)
                {
                    cellObject = currentTarget.GetComponent<GridCellObject>();
                    //defaultColor = cellObject.transform.GetChild(0).GetComponent<Renderer>().material.color;
                    pointingAtCell = true;

                    if (cellObject.occupied)
                    {
                        cellObject.ChangeColorOfTile(cellObject.lockedColor);
                    }
                    else
                    {
                        cellObject.ChangeColorOfTile(cellObject.avaliableColor);
                    }
                }
            }
            else
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, backgroundLayer))
                {
                    if (pointingAtCell)
                    {
                        pointingAtCell = false;
                        if (cellObject != null)
                        {
                            cellObject.ChangeColorOfTile(cellObject.defaultColor);
                        }
                        cellObject = null;
                    }
                }
            }
        }
    }
}

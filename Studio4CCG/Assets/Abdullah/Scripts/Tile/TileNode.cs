using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class TileNode : MonoBehaviour
{
    public ParticleSystem particleSystem { get; private set; }
    public bool playerTiles= true;
    public Transform storCard;
    public OccupieState occupieState = OccupieState.empty;
    void Start()
    {

        particleSystem = GetComponentInChildren<ParticleSystem>();

    }



}

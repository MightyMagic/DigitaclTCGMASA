using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileNode : MonoBehaviour
{
    public ParticleSystem particleSystem { get; private set; }

    public enum OccupieState
    {
        empty,
        blocked,
        occupied,
    }
    public OccupieState occupieState = OccupieState.empty;
    void Start()
    {

        particleSystem = GetComponentInChildren<ParticleSystem>();

    }



    // Start is called before the first frame update



}

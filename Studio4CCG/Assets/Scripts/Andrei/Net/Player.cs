using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] NetworkEvents events;

    void ChangePosition(Vector3 position)
    {
        Debug.Log("New position is " +  position.x + " " + position.y + " " + position.z);
    }

    void Start()
    {
        events.onPositionReceived += ChangePosition;
    }
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        events.onPositionReceived -= ChangePosition;
    }
}

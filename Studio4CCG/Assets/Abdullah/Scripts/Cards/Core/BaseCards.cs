using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "----Cards/NewCard")]
public class BaseCards : ScriptableObject
{
    public IntValue[] myValue = new IntValue[4];
    public StatesList statesList;
    public IBaseEffect BaseEffect;

}

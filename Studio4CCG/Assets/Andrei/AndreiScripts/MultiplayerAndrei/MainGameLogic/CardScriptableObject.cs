using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndreiMultiplayer
{
    [CreateAssetMenu]
    public class CardScriptableObject: ScriptableObject 
    {
        [SerializeField] string Name;
        [SerializeField] string Description;
        [SerializeField] CardClass CardClass;

        [SerializeField] int ManaCost;
        [SerializeField] int Hp;
        [SerializeField] int Attack;

        [SerializeField] CardEffect OnSummonEffect;
        [SerializeField] CardEffect EndOfTurnEffect;
    }
}

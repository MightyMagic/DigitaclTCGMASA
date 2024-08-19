using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndreiMultiplayer
{
    [CreateAssetMenu]
    public class CardScriptableObject: ScriptableObject 
    {
        public string CardName;
        public int CardId;
        public string Description;
        public CardClass CardClass;
        
        public int ManaCost;
        public int Hp;
        public int Attack;
       
        public CardEffect OnSummonEffect;
        public CardEffect EndOfTurnEffect;
    }
}

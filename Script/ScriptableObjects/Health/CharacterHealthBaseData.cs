using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace piiilk_ARPGDemo.HealthBase
{
    [CreateAssetMenu(fileName = "BaseHealthData", menuName = "ScriptableObjects/CharacterHealthBaseData", order = 1)]
    public class CharacterHealthBaseData : ScriptableObject
    {
        [SerializeField]private float _maxHealth;
        [SerializeField]private float _maxStrength;
        
        public float MaxHealth=>_maxHealth;
        public float MaxStrength => _maxStrength;
    }

}
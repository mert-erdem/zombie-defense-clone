using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponScriptableObject", order = 1)]
    public class WeaponSO : ScriptableObject
    {
        public int damage;
        public float fireRate;
        public float range;
        public float fireConeRange = 0f; // shotgun etki alanı gibi
    }
}

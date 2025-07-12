using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int weaponLevel;
    public List<WeaponStat> stats;

    [System.Serializable]
    public class WeaponStat
    {
        public float speed;
        public float damage;
        public float amount;
        public float range;
    }
}

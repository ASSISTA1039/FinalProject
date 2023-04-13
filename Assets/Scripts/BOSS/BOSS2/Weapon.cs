using System;
using Core.Combat2.Projectiles;
using UnityEngine;

namespace Core.Combat2
{
    [Serializable]
    public class Weapon
    {
        public Transform weaponTransform;
        public AbstractProjectile projectilePrefab;
        public float horizontalForce = 5.0f;
        public float verticalForce = 4.0f;
    }
}
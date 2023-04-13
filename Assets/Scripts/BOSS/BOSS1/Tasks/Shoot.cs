using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Combat;
using BehaviorDesigner.Runtime.Tasks;

namespace Core.AI
{
    public class Shoot : EnemyAction
    {
        public List<Weapon> weapons;
        public bool shakeCamera;

        public override TaskStatus OnUpdate()
        {
            foreach (var weapon in weapons)
            {
                var projectile = Object.Instantiate(weapon.projectilePrefab, weapon.weaponTransform.position,
                    Quaternion.identity);

                var force = new Vector2(weapon.horizontalForce * body.transform.localScale.x, 0);
                projectile.SetForce(force);

                if(shakeCamera)
                {
                    CinemachineShake.Instance.ShakeCamera(2f, 1f);
                }
            }
            return TaskStatus.Success;
        }
    }
}

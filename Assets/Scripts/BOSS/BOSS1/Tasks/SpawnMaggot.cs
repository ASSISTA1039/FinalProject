using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AI
{
    public class SpawnMaggot : EnemyAction
    {
        private BOSS1 maggot;

        public override void OnStart()
        {
            maggot = GetComponent<BOSS1>();
            maggot.maxHealth = 10;
            maggot.currentHealth = 10;
        }

        public override TaskStatus OnUpdate()
        {
            if (maggot.currentHealth > 0) return TaskStatus.Running;
            else return TaskStatus.Success;
        }

    }
}
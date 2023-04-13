using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.AI2
{
    public class RestoreHealth : EnemyAction
    {
        public SharedInt healthRestore;
        public BOSS2 boss;
        public override void OnAwake()
        {
        }
        public override TaskStatus OnUpdate()
        {
            if(boss)
            {
                boss.currentHealth = (float)healthRestore.Value;
            }
            return TaskStatus.Success;
        }
    }
}

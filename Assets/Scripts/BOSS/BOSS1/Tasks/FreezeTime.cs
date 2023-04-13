using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AI
{
    public class FreezeTime : EnemyAction
    {
        public SharedFloat Duraction = 0.1f;

        public override TaskStatus OnUpdate()
        {
            GameManager.Instance.FreezeTime(Duraction.Value);
            return TaskStatus.Success;
        }
    }
}

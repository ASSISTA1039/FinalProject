using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AI
{
    public class OverAutoTalk : EnemyAction
    {
        public CMChange cm;
        public SharedFloat Duraction = 0.1f;

        public override TaskStatus OnUpdate()
        {
            if (cm)
                cm.ChangeState();
            return TaskStatus.Success;
        }
    }
}


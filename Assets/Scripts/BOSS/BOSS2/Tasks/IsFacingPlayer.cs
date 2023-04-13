using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.AI2
{
    public class IsFacingPlayer : EnemyAction
    {
        public override TaskStatus OnUpdate()
        {
            return body.transform.position.x > player.transform.position.x ? TaskStatus.Failure : TaskStatus.Success;
        }
    }
}


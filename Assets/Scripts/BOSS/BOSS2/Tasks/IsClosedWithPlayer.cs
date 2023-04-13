using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AI2
{
    public class IsClosedWithPlayer : Conditional
    {
        public SharedFloat Distance_withplayer;

        public GameObject player;
        public GameObject boss;

        public override TaskStatus OnUpdate()
        {
            return player && Mathf.Abs(player.transform.position.x - boss.transform.position.x) < 10f ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
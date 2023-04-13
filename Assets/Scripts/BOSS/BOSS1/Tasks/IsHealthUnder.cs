using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.AI
{
    public class IsHealthUnder : Conditional
    {
        public SharedInt HealthTreshold;

        public override TaskStatus OnUpdate()
        {
            return BOSS1.isDead ? TaskStatus.Success : TaskStatus.Failure;
        }
    } 
}

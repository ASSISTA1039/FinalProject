using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.AI2
{
    public class IsHealthUnder_Boss2 : Conditional
    {
        public SharedFloat HealthTreshold;

        public override TaskStatus OnUpdate()
        {
            Debug.Log(HealthTreshold.Value);
            return HealthTreshold.Value <= 0 ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}

using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AI2
{
    public class HasAnyBat : Conditional
    {
        public SharedInt count_bat;

        public override TaskStatus OnUpdate()
        {
            return count_bat.Value > 0 ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
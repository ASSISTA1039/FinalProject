using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.AI2
{
    public class NormalState : Conditional
    {

        public override TaskStatus OnUpdate()
        {
            return BOSS2.isHalf ? TaskStatus.Failure : TaskStatus.Success; 
        }
    }
}
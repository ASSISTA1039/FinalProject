using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Core.AI2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTalk_Boss : EnemyAction
{
    public override TaskStatus OnUpdate()
    {
        AutoTalk1.start_autoTalk = true;
        return TaskStatus.Success;
    }
}

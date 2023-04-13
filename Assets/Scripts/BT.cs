/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

unsafe public class BehaviorTree
{
    BehaviorTree* bt = BehaviorTreeDesigner()
        .repeater()
        .selector()
        .sequence() //如果生命低于0
            .condition(IsHealthZero)
            .action(Death)
        .sequence() //如果生命低于一半
            .condition(IsHealthHalf)
            .action(BecomeBerserk)
        .sequence() //如果在修复状态
            .condition(ISShakingLightVisible)
            .action(Resting)
            .action(SpawnFallingRocks)
        .sequence() //如果玩家处于检测范围
            .action(FaceToPlayer)
            .randomSelector()
                .sequence()
                    .action(AttackSkill1)
                .sequence()
                    .action(AttackSkill2)
        .sequence() //否则
            .action(FaceToPlayer)
            .action(MoveToRandomPosition)
        .end()；

}
*/
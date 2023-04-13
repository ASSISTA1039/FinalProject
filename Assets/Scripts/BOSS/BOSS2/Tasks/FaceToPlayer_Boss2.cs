using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.AI2
{
    public class FaceToPlayer_Boss2 : EnemyAction
    {
        private float baseScaleX;

        public override void OnAwake()
        {
            base.OnAwake();
            baseScaleX = body.transform.localScale.x;
        }

        public override TaskStatus OnUpdate()
        {
            var scale = body.transform.localScale;
            scale.x = body.transform.position.x > player.transform.position.x ? baseScaleX : -baseScaleX;
            body.transform.localScale = scale;
            return TaskStatus.Success;
        }
    }
}

using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.AI2
{
    public class CameraShake_BOSS : EnemyAction
    {
        public float cameraShakeIntensity = 5f;
        public override TaskStatus OnUpdate()
        {
            CinemachineShake.Instance.ShakeCamera(cameraShakeIntensity, 0.5f);
            return TaskStatus.Success;
        }
    }
}

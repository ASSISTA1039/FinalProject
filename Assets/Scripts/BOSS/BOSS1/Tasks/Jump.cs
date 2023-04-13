using BehaviorDesigner.Runtime.Tasks;
using Core.Camera;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AI
{
    public class Jump : EnemyAction
    {
        public float horizontalForce = 5.0f;
        public float jumpForce = 10.0f;

        public float bulidupTime;
        public float jumpTime;

        public string animationTriggerName;
        public bool shakeCameraOnLanding;

        private bool hasLanded;

        private Tween buildupTween;
        private Tween jumpTween;

        public override void OnStart()
        {
            buildupTween = DOVirtual.DelayedCall(bulidupTime, StartJump, false);
            animator.SetTrigger(animationTriggerName);
        }

        private void StartJump()
        {
            var direction = player.transform.position.x < body.transform.position.x ? -1 : 1;
            body.AddForce(new Vector2(horizontalForce * direction, jumpForce), ForceMode2D.Impulse);

            jumpTween = DOVirtual.DelayedCall(jumpTime, () =>
             {
                 hasLanded = true;
                 if (shakeCameraOnLanding)
                 {
                     Debug.Log("hello");
                     CinemachineShake.Instance.ShakeCamera(3f,.5f);
                 }
             }, false);
        }

        public override TaskStatus OnUpdate()
        {
            return hasLanded ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            buildupTween?.Kill();
            jumpTween?.Kill();
            hasLanded = false;
        }
    }
}

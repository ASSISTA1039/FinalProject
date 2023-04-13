using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.AI2
{
    public class IsTrueBodyAppear : Conditional
    {
        public SpriteRenderer render_shakingLight;
        public float frezeTime;
        private float timer;
        public bool isFrezing = false;
        public override TaskStatus OnUpdate()
        {
            if(render_shakingLight.enabled && !isFrezing)
            {
                isFrezing = true;
                timer = Time.time;
                timer += frezeTime*Time.deltaTime;
            }
            if (timer <= Time.time)
                isFrezing = false;
            return (render_shakingLight.enabled && !isFrezing) ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
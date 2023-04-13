using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.AI2
{
    public class RandomAppear : EnemyAction
    {
        public Transform leftpoint;
        public Transform rightpoint;
        public override TaskStatus OnUpdate()
        {
            body.transform.position = 
                new Vector3(Random.Range(leftpoint.position.x,rightpoint.position.x),
                            leftpoint.position.y, 
                            leftpoint.position.z);
            render.enabled = true;
            return TaskStatus.Success;
        }
    }
}
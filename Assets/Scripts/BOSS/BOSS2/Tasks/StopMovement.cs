using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.AI2
{
    public class StopMovement : EnemyAction
    {
        public Rigidbody2D rb;
        public override TaskStatus OnUpdate()
        {
            rb.velocity = Vector2.zero;
            return TaskStatus.Success;
        }
    }
}
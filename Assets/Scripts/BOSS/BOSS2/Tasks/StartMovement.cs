using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
namespace Core.AI2
{
    public class StartMovement : EnemyAction
    {
        public Rigidbody2D rb;
        public override TaskStatus OnUpdate()
        {
            rb.velocity = transform.localScale.x * 5 * Vector2.right;

            return TaskStatus.Success;
        }
    }
}
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.AI2
{ 
    public class TiredMode : EnemyAction
    {
        public BoxCollider2D bodycollider;
        public Rigidbody2D rb;
        public static bool tired = false;
        public override TaskStatus OnUpdate()
        {
            rb.velocity = Vector2.zero;
            render.enabled = true;
            bodycollider.enabled = false;
            tired = true;
            animator.SetTrigger("Reply");

            return TaskStatus.Success;
        }
    }
}
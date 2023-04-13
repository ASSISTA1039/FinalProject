using BehaviorDesigner.Runtime.Tasks;
using Core.Combat;
using UnityEngine;

namespace Core.AI
{
    public class EnemyConditional : Conditional
    {
        protected Rigidbody2D body;
        protected Animator animator;
        protected BOSS1 destructable;
        protected GameObject player;

        public override void OnAwake()
        {
            body = GetComponent<Rigidbody2D>();
            player = GameObject.Find("Player");
            destructable = GetComponent<BOSS1>();
            animator = gameObject.GetComponentInChildren<Animator>();
        }
    }
}
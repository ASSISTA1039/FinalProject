using BehaviorDesigner.Runtime.Tasks;
using Core.Combat2;
using UnityEngine;

namespace Core.AI2
{
    public class EnemyConditional : Conditional
    {
        protected Rigidbody2D body;
        protected Animator animator;
        protected BOSS2 destructable;
        protected GameObject player;

        public override void OnAwake()
        {
            body = GetComponent<Rigidbody2D>();
            player = GameObject.Find("Player");
            destructable = GetComponent<BOSS2>();
            animator = gameObject.GetComponentInChildren<Animator>();
        }
    }
}
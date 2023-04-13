using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI2
{
    public class EnemyAction : Action
    {
        protected Rigidbody2D body;
        protected SpriteRenderer render;
        protected Animator animator;
        protected BOSS2 destructable;
        protected GameObject player;
        public override void OnAwake()
        {
            body = gameObject.GetComponentInChildren<Rigidbody2D>();
            render = gameObject.GetComponentInChildren<SpriteRenderer>();
            player = GameObject.Find("Player");
            destructable = GetComponent<BOSS2>();
            animator = gameObject.GetComponentInChildren<Animator>();
            
        }
    }
}
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.AI
{
    public class EnemyAction : Action
    {
        protected Rigidbody2D body;
        protected Animator animator;
        protected BOSS1 destructable;
        protected GameObject player;

        public override void OnAwake()
        {
            body = gameObject.GetComponentInChildren<Rigidbody2D>();
            player = GameObject.Find("Player");
            destructable = GetComponent<BOSS1>();
            animator = gameObject.GetComponentInChildren<Animator>();
        }
    }
}
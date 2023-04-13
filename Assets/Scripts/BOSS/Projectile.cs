using Core.Util;
using UnityEngine;
using Core.AI;
using Core.AI2;

namespace Core.Combat.Projectiles
{
    public class Projectile : AbstractProjectile
    {
        public LayerMask groundLayer;
        public ParticleSystem smokeEffect;

        private AttackDetails attackDetails;

        public override void SetForce(Vector2 force)
        {
            this.force = force;
            GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            smokeEffect.Play();
        }

        private void Update()
        {
            if(towardsBoss && TiredMode.tired)
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                gameObject.transform.position = Vector2.MoveTowards(transform.position, boss.transform.position, 0.1f);
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                towardsBoss = false;
                DestroyProjectile();
            }
            if (collision.gameObject.CompareTag("Attack"))
            {
                towardsBoss = true;
            }
            if (collision.gameObject.CompareTag("Player") && (collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D"))
            {
                if (playerHealth != null && playerHealth.rope.enabled == false && playerHealth.rope1.enabled == false)
                    playerHealth.DamagePlayer(damage);
                towardsBoss = false;
                DestroyProjectile();
            }
            if (collision.gameObject.CompareTag("Enemy") && towardsBoss)
            {
                attackDetails.damageAmount = damage;
                collision.gameObject.GetComponentInParent<BOSS2>().Damage(attackDetails);
                //到时候给boss1单独写一个，在复制一个预制体
                towardsBoss = false;
                DestroyProjectile();
            }
            else if(collision.gameObject.CompareTag("Enemy") && !towardsBoss)
            {
                towardsBoss = false;
                DestroyProjectile();
            }
        }
    }
}
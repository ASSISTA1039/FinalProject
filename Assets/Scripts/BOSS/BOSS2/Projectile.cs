using Core.Util;
using UnityEngine;

namespace Core.Combat2.Projectiles
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
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ground"))
                DestroyProjectile();
            if (collision.gameObject.CompareTag("Enemy"))
            {
                attackDetails.damageAmount = damage;
                collision.transform.parent.SendMessage("Damage", attackDetails);
                DestroyProjectile();
            }
        }
    }
}
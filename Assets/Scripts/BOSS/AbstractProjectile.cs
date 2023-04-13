using System;
using Core.Util;
using UnityEngine;

namespace Core.Combat.Projectiles
{
    public abstract class AbstractProjectile : MonoBehaviour
    {
        public float damage;
        public ParticleSystem explosionEffect;
        public AudioClip splatterSound;

        public GameObject Shooter { get; set; }
        public PlayerHealth playerHealth;
        public GameObject boss;
        public bool towardsBoss;
        protected Vector2 force;
        private AttackDetails attackDetails;

        public event Action<AbstractProjectile> OnProjectileDestroyed;
    
        public abstract void SetForce(Vector2 force);
        private void OnEnable()
        {
            playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
            
            boss = GameObject.Find("Boss2").transform.Find("Dark_Knight").gameObject; ;
            towardsBoss = false;
        }
        protected void DestroyProjectile()
        {
            OnProjectileDestroyed?.Invoke(this);

            if (splatterSound != null)
                SoundManager.Instance.PlaySoundAtLocation(splatterSound, transform.position, 0.75f);

            EffectManager.Instance.PlayOneShot(explosionEffect, transform.position);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Can't shoot yourself
            if (collision.gameObject == Shooter)
                return;

            if (collision.gameObject.CompareTag("Player") && (collision.GetType().ToString() == "UnityEngine.BoxCollider2D" || collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D"))
            {
                if (playerHealth != null && playerHealth.rope.enabled == false && playerHealth.rope1.enabled == false)
                    playerHealth.DamagePlayer(damage);
                DestroyProjectile();
            }
/*            if (collision.gameObject.CompareTag("Attack"))
            {
                towardsBoss = true;
            }*/

        }
    }
}
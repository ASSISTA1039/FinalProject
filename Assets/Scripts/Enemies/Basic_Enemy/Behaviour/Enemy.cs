using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float health;
    public float damage;
    public GameObject dropCoin;
    public GameObject dropEnergy;
    private PlayerHealth playerHealth;
    public GameObject bloodEffect;

    // Start is called before the first frame update
    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Instantiate(dropCoin, transform.position, Quaternion.identity);
            Instantiate(dropEnergy, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Damage(AttackDetails damage)
    {
        health -= (int)damage.damageAmount;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            if (playerHealth != null && playerHealth.rope.enabled == false && playerHealth.rope1.enabled == false)
                collision.gameObject.transform.SendMessage("DamagePlayer", damage);
        }
    }
    
/*    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            canGenBlock = false;
        }
    }*/
    
    public void DestorySelf()
    {
        Destroy(gameObject);
    }
}

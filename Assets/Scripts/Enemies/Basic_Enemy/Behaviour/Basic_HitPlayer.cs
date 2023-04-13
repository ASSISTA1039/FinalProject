using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_HitPlayer : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public float damage;
    public float beilv;
    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    private void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.SendMessage("DamagePlayer", damage);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            other.gameObject.transform.SendMessage("DamagePlayer", damage);
        }
    }
}

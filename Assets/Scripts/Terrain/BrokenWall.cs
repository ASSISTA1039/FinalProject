using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    public Rigidbody2D[] rigidbodies;
    private BoxCollider2D _collider;
    private int playerfacing;
    private float attackTime_brokenWall;
    private bool hasbroken;
    private void Start()
    {
        _collider = gameObject.GetComponent<BoxCollider2D>();
        /*foreach (var rb in rigidbodies)
        {
            rb.gameObject.SetActive(false);
        }*/
    }
    private void Update()
    {
        if(!_collider.enabled && hasbroken)
        {
            Die();
            GetComponent<AudioSource>().Play();
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBody") && collision.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            playerfacing = collision.gameObject.GetComponentInParent<Transform>().rotation.y >= 0 ? 1 : -1;
            Debug.Log(attackTime_brokenWall);
            if (attackTime_brokenWall == 2)
            {
                _collider.enabled = false;
                hasbroken = true;
                attackTime_brokenWall = 0;
            }
            else
            {
                attackTime_brokenWall++;
                //voice
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
    private void Die()
    {
        hasbroken = false;
        float deathspeedx = 3;
        float deathspeedy = 0;
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].bodyType = RigidbodyType2D.Dynamic;
            if (i >= 2)
                rigidbodies[i].velocity = new Vector2((deathspeedx+1f) * playerfacing, deathspeedy);
        }
    }
}

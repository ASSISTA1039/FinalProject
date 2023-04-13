using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector2 dir;
    private PlayerHealth playerHealth;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        //dir = new Vector2(-3f,0);
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

        Invoke("Shot", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Shot()
    {
        this.GetComponent<Rigidbody2D>().velocity = dir.normalized * 10;
        Invoke("Clear", 5);
    }


    public void Clear()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Wall")
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (playerHealth != null)
                    collision.gameObject.transform.SendMessage("DamagePlayer", damage);
            }
            Destroy(gameObject);
        }

    }
}

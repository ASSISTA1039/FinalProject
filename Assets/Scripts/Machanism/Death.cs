using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GrapplingRope rope;
    public GrapplingRope1 rope1;
    public Rigidbody2D rb2d;
    public PlayerHealth health;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            rope.enabled = false;
            rope1.enabled = false;
            health.KillPlayer();
        }
    }
}

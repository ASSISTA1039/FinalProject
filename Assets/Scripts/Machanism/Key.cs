using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //public Transform m_transform;
    public Transform player;
    Rigidbody2D rb;
    public static bool tracePlayer;
    public Vector2 deltaPosition;
    // Start is called before the first frame update
    void Start()
    {
        tracePlayer = false;
        player = GameObject.Find("Player").GetComponent<Transform>();
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.angularDrag = 0;


        deltaPosition = new Vector2(1, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (tracePlayer)
        {
            Vector2 pos = player.transform.position;
            Vector3 dest;
            if (player.rotation.y >= 0f)
            {
                dest = new Vector3(pos.x - deltaPosition.x, pos.y + deltaPosition.y, 0);
            }
            else
            {
                dest = new Vector3(pos.x + deltaPosition.x, pos.y + deltaPosition.y, 0);
            }

            rb.velocity = dest - transform.position;
        }
    }
}


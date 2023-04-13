using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveall : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + new Vector2(100, 0), Time.deltaTime);


    }

    private void FixedUpdate()
    {
        //rb.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + new Vector2(100, 0), Time.deltaTime);

    }
}

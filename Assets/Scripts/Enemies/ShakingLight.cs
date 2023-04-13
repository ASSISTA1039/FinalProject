using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingLight : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy_Controller"))
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}

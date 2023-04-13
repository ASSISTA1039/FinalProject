using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_BatController : MonoBehaviour
{
    Rigidbody2D rig;
    public Vector2 dir;
    public GameObject boss;
    bool isOnWall;
    public GameObject bloodEffect;
    public bool dead;

    public SharedInt count;
    public static int bat_CurrentCount;
    private BehaviorTree behavior;
    // Start is called before the first frame update

    private void Awake()
    {
        bat_CurrentCount = 8;
    }
    void OnEnable()
    {
        dead = false;
        rig = GetComponent<Rigidbody2D>();
        boss = GameObject.Find("Boss2");
        behavior = boss.GetComponent<BehaviorTree>();
        count = bat_CurrentCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnWall)
        {
            rig.velocity = dir * 3;
        }
        if(boss)
        {
            if(dead)
            {
                bat_CurrentCount--;
                count = bat_CurrentCount;
                behavior.SetVariable("FinalHealth_FromBat", count); 
                Instantiate(bloodEffect, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            rig.velocity = Vector2.zero;
            isOnWall = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            rig.velocity = Vector2.zero;
            isOnWall = true;
        }
        if(collision.CompareTag("PlayerBody"))
        {
            dead = true;
        }
    }
}

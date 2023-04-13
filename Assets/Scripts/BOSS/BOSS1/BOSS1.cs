using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS1 : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    private float lastDamageTime;
    public GameObject aliveGO { get; private set; }
    public int facingDirection { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public int lastDamageDirection { get; private set; }

    private Vector2 velocityWorkspace;
    public GameObject bloodEffect;

    public static bool isDead;

    public void Start()
    {
        facingDirection = 1;
        currentHealth = maxHealth;

        aliveGO = transform.Find("False_Knight").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();

    }

    public void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;

        currentHealth -= attackDetails.damageAmount;

        Instantiate(bloodEffect, new Vector3(aliveGO.transform.position.x, aliveGO.transform.position.y+2.3f, aliveGO.transform.position.z), Quaternion.identity);

        if (attackDetails.position.x > aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }
}

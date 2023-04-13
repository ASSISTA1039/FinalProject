using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS2 : MonoBehaviour
{
    [Header("-- System Data --")]
    [SerializeField] SaveItem bagsave;

    public SharedFloat healthHold;
    private BehaviorTree behavior;
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
    public static bool isHalf;

    public GameEvent _event;


    private void Awake()
    {
        if (_event.hasaved)
            Destroy(gameObject);
    }

    public void Start()
    {
        behavior = GetComponent<BehaviorTree>();
        
        isHalf = false;
        facingDirection = 1;
        currentHealth = maxHealth;
        Boss_HealthBar.HealthCurrent = currentHealth;
        Boss_HealthBar.HealthMax = currentHealth;
        aliveGO = transform.Find("Dark_Knight").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        healthHold = currentHealth;
    }
    private void Update()
    {
        healthHold = currentHealth;
        behavior.SetVariable("HealthTreshold", healthHold);
        Boss_HealthBar.HealthCurrent = currentHealth;
    }
    public void Damage(AttackDetails attackDetails)
    {
        lastDamageTime = Time.time;

        currentHealth -= attackDetails.damageAmount;

        Instantiate(bloodEffect, new Vector3(aliveGO.transform.position.x, aliveGO.transform.position.y + 2.3f, aliveGO.transform.position.z), Quaternion.identity);

        if (attackDetails.position.x > aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }
        if (currentHealth <= maxHealth / 2)
        {
            isHalf = true;
            isDead = false;
        }
        if (currentHealth <= 0)
        {
            _event.hasaved = true;
            bagsave.Save();
            isHalf = false;
            isDead = true;
        }
    }
}

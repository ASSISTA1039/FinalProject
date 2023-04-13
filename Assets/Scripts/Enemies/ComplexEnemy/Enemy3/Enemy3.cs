using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyController
{
    public E3_IdleState idleState { get; private set; }
    //public E3_MoveState moveState { get; private set; }
    public E3_PlayerDetectedState playerDetectedState { get; private set; }
    public E3_ChargeState chargeState { get; private set; }
    public E3_LookForPlayerState lookForPlayerState { get; private set; }
    //public E3_MeleeAttackState meleeAttackState { get; private set; }
    public E3_DeadState deadState { get; private set; }
    //public E3_AbleState ableState { get; private set; }
    public E3_DisappearState disappearState { get; private set; }
    public E3_RangeAttackState rangeattackState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedData;
    [SerializeField]
    private D_ChargeState chargeStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    //[SerializeField]
    //private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    //[SerializeField]
    //private D_AbleState ableStateData;
    [SerializeField]
    private D_DisappearState disappearStateData;
    [SerializeField]
    private D_RangeAttackState rangeattackStateData;


    [SerializeField]
    private Transform rangeAttackPosition;

    [SerializeField]
    private Transform[] transforms;
    [SerializeField]
    private Transform[] transforms_temp;

    [Space]
    private float frezeTime = 3f;
    private float startTime;
    private float tempHealth;
    private CircleCollider2D collider2d;
    public override void Start()
    {
        base.Start();
        tempHealth = enemyData.maxHealth;
        collider2d = gameObject.GetComponent<CircleCollider2D>();
        idleState = new E3_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E3_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        chargeState = new E3_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new E3_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        //meleeAttackState = new E3_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        deadState = new E3_DeadState(this, stateMachine, "dead", deadStateData, this);
        //ableState = new E3_AbleState(this, stateMachine, "able", ableStateData, this);
        disappearState = new E3_DisappearState(this, stateMachine, "disappear", disappearStateData, this);
        rangeattackState = new E3_RangeAttackState(this, stateMachine, "rangeattack", rangeAttackPosition, rangeattackStateData, this);


        stateMachine.Initialize(idleState);

    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(rangeAttackPosition.position, rangeattackStateData.attackRadius);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && collider.GetType().ToString() == "UnityEngine.BoxCollider2D" && startTime < Time.time)
        {
            stateMachine.ChangeState(rangeattackState);
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            startTime = Time.time + frezeTime;
        }
    }

    public override void Update()
    {
        //base.Update();
        CheckPlayer(collider2d);
        if (startTime < Time.time)
        {
            stateMachine.currentState.LogicUpdate();
            this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
        if(currentHealth<tempHealth)
        {
            float x = Random.Range(transforms[1].position.x, transforms_temp[0].position.x);
            float y = Random.Range(transforms[1].position.y, transforms_temp[0].position.y);
            float z = Random.Range(transforms[1].position.z, transforms_temp[0].position.z);
            if((x <= transforms[0].position.x 
                && y <= transforms[0].position.y)||
                (x >= transforms_temp[1].position.x
                && y>= transforms_temp[1].position.y))
            {
                gameObject.transform.position = new Vector3(x, y, z);
            }
        }
        tempHealth = currentHealth;
    }
}

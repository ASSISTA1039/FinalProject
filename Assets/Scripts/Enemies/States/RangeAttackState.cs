using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    protected D_RangeAttackState stateData;

    protected AttackDetails attackDetails;

    public GameObject prefabBullet;

    protected Transform player;

    public RangeAttackState(EnemyController entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DeleteCollider()
    {
        base.DeleteCollider();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    private void Start()
    {

    }

    public override void Enter()
    {
        base.Enter();
        prefabBullet = Resources.Load("Prefabs/AttackSkill/Range_Bullet") as GameObject;
        Debug.Log(prefabBullet);
        player = GameObject.Find("Player").GetComponent<Transform>();
        
        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = entity.aliveGO.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}

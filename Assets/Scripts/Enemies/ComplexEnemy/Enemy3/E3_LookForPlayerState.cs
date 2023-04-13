using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_LookForPlayerState : LookForPlayerState
{
    private Enemy3 enemy;
    private GameObject player;
    private float distance;
    private Transform alive;

    public E3_LookForPlayerState(EnemyController entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.FindGameObjectWithTag("Player");
        alive = enemy.GetComponentInChildren<Transform>();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        /*if (!enemy.gameObject.GetComponent<CircleCollider2D>().enabled)
            distance = player.transform.position.x - enemy.transform.position.x;
        if (distance >= 0)
        {
            EnemyController.facingDirection = 1 * Mathf.Abs(EnemyController.facingDirection);
            alive.rotation = new Quaternion(0f, 0f, 0f, alive.rotation.w);
        }
        else
        {
            EnemyController.facingDirection = -1 * Mathf.Abs(EnemyController.facingDirection);
            alive.rotation = new Quaternion(0f, 180f, 0f, alive.rotation.w);
        }*/
        /*if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else */
        if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

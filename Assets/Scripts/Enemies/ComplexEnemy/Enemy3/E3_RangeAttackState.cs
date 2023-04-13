using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_RangeAttackState : RangeAttackState
{
    private Enemy3 enemy;
    private Enemy3 enemy31;
    private string v;
    private D_RangeAttackState rangeattackStateData;
    private Enemy3 enemy32;
    private float sofarTime;
    private float frezeTime =4f;
    private bool isShoted = false;
    public E3_RangeAttackState(EnemyController entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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
        
        sofarTime = Time.time;
        if (isAnimationFinished && isShoted)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            isShoted = false;
/*            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }*/
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            if (sofarTime<Time.time)
            {
                GameObject bullet = Instantiate(prefabBullet);
                bullet.transform.position = enemy.transform.position;
                bullet.GetComponent<Bullet>().dir = (new Vector3(player.transform.position.x, player.transform.position.y + 2.6f, player.transform.position.z)
                                                    - bullet.transform.position);
                sofarTime = Time.time + frezeTime;
                isShoted = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_AppearState : AppearState
{
    private Enemy2 enemy;

    public E2_AppearState(EnemyController entity, FiniteStateMachine stateMachine, string animBoolName, D_AppearState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isAppearTimeOver)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

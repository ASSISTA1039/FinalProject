using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DisableState : DisableState
{
    private Enemy2 enemy;

    public E2_DisableState(EnemyController entity, FiniteStateMachine stateMachine, string animBoolName, D_DisableState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isPlayerUpper)
            stateMachine.ChangeState(enemy.appearState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

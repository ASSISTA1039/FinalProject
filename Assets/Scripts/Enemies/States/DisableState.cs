using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableState : State
{
    protected D_DisableState stateData;

    protected bool isPlayerUpper = false;

    public DisableState(EnemyController entity, FiniteStateMachine stateMachine, string animBoolName, D_DisableState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerUpper = entity.CheckUpper();
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

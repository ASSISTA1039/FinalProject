using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearState : State
{
    protected D_DisappearState stateData;

    protected bool isDisappearTimeOver;


    public DisappearState(EnemyController entity, FiniteStateMachine stateMachine, string animBoolName, D_DisappearState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        isDisappearTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.disappearTime)
        {
            isDisappearTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

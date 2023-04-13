using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearState : State
{
    protected D_AppearState stateData;

    protected bool isAppearTimeOver;


    public AppearState(EnemyController entity, FiniteStateMachine stateMachine, string animBoolName, D_AppearState stateData) : base(entity, stateMachine, animBoolName)
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
        isAppearTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.appearTime)
        {
            isAppearTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

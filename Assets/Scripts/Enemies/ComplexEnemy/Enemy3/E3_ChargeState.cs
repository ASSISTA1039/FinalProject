﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_ChargeState : ChargeState
{
    private Enemy3 enemy;

    public E3_ChargeState(EnemyController entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

    public override void LogicUpdate()
    {
        base.LogicUpdate();

/*        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.rangeattackState);
        }
        else if (!isDectectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        else*/ if (isChargeTimeOver)
        {
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
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
}

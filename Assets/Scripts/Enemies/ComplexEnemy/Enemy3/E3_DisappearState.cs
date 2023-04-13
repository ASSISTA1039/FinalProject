using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_DisappearState : DisappearState
{
    private Enemy3 enemy;

    public E3_DisappearState(EnemyController entity, FiniteStateMachine stateMachine, string animBoolName, D_DisappearState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        //enemy.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, Mathf.Lerp(255, 0, 0.2f));
        //Debug.Log(enemy.gameObject.GetComponentInChildren<SpriteRenderer>().color.a);
        if (isDisappearTimeOver)
        {
            //enemy.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(255,255,255,255);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}

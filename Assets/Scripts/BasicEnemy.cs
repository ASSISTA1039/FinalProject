/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    private Enemy2 enemy;
    protected bool isPlayerUpper = false;
    public FiniteStateMachine stateMachine;
    protected EnemyController entity;
    //stun
    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementStopped;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRange;
    //检测
    //protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performLongRangeAction;
    //protected bool performCloseRangeAction;
    protected bool isDetectingLedge;

    //lookplayer
    protected bool turnImmediately;
    //protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;

    protected int amountOfTurnsDone;

    //站立
    protected bool isIdleTimeOver;

    //charge
    //protected bool isPlayerInMinAgroRange;
    protected bool isDectectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;

    //chuxian
    protected bool isAppearTimeOver;
    private object 出现状态;

    // Update is called once per frame
    void Update()
    {
        //Disappear state
        if (the current enemy is underground)
        {
            if (Detects player passing overhead)
                entern "appear state";
        }


        //Stunned state
        if (Stun time ends)
        {
            if(The direction of damage to the enemy comes from the opposite direction)
                The enemy turns；
            if (Player enters "attack range")
            {
                Enter the "attack state";
            }
            else if (Player enters "detection range")
            {
                Enter the "charge state";
            }
            else
            {
                Enter the "look-around state";
            }
        }
        //Charge state
        if (If the distance between the enemy and the player is reduced in this update()
             && the enemy is not stationary)
        {
            if (Player enters attack range)
            {
                Enter the "attack state";
            }
            else if (Detects the wall || does not detect the floor)
            {
                Enter the "look-around state";
            }
            else if (If the charge time is over)
            {
                if (If the player is in detection range)
                {
                    Enter the "Locked Player state";
                }
                else
                {
                    Enter the "look-around state";
                }
            }
        }
        //Locked Player state
        if (The enemy is currently on the ground 
            && The player enters detection range 
            && the enemy's current speed is 0)
        {
            if (Player enters "attack range")
            {
                Enter the "attack state";
            }
            else if (Player enters "detection range")
            {
                Enter the "charge state";
            }
            else if (Detects that the ground is empty, i.e.has gone to the end of the ground)
            {
                Turn and enter the "disappear state"；
            }
            else if (Player out of detection range)
            {
                Enter the "look-around state";
            }
        }

        //环顾四周状态
        if (检测到玩家)
        {
            进入“锁定玩家状态”;
        }
        else if (状态时间结束且并没找到玩家)
        {
            进入“消失状态”;
        }

        //站立
        if (站立时间结束)
        {
            进入“不可见状态”；
        }




        //appear
        if (如果appear时间结束)
        {
            进入“站立状态”；
        }
    }
}
*/
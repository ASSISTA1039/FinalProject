using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangeAttackStateData", menuName = "Data/State Data/Range Attack State")]
public class D_RangeAttackState : ScriptableObject
{
    public float attackRadius = 0.5f;
    public float attackDamage = 2f;

    public LayerMask whatIsPlayer;
}

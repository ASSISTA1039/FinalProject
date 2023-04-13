using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    [Header("ID")]
    public int itemid;

    [Header("Attribute")]
    public float buff_HP;
    public float buff_Energy;
    public float buff_Attack;
    public float buff_downHP2Attack;

    [Header("Skill")]
    public bool doubleGrappling;
    public bool Graplling2Enemy;

    [Header("EnemyDrop")]
    public bool fireToPlayer;


    [TextArea]
    public string itemInfo;

    public bool equip;
    public bool pickup;
}

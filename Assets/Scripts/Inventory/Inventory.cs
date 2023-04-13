using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class Inventory : ScriptableObject
{
    [SerializeField]
    List<int> keys = new List<int>();
    [SerializeField]
    List<Item> values = new List<Item>();

    public List<int> Keys { get => keys; set => keys = value; }
    public List<Item> Values { get => values; set => values = value; }
}


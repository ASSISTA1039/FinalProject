using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MemoryLibrary", menuName = "MemoryLibrary/New GoodMemory")]
public class GoodMemory : ScriptableObject
{
    [SerializeField]
    List<int> index = new List<int>();
    [SerializeField]
    List<Memory> good_memoryList = new List<Memory>();

    public List<int> Index { get => index; set => index = value; }
    public List<Memory> Good_memoryList { get => good_memoryList; set => good_memoryList = value; }
}

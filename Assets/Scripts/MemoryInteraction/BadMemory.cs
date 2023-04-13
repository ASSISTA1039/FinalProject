using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MemoryLibrary", menuName = "MemoryLibrary/New BadMemory")]
public class BadMemory : ScriptableObject
{
    [SerializeField]
    List<int> index = new List<int>();
    [SerializeField]
    List<Memory> bad_memoryList = new List<Memory>();

    public List<int> Index { get => index; set => index = value; }
    public List<Memory> Bad_memoryList { get => bad_memoryList; set => bad_memoryList = value; }
}

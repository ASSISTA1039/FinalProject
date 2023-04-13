using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Record", menuName = "RecordLibrary/New Record")]
public class Record : ScriptableObject
{
    [Header("ID")]
    public int Recordid;

    [Header("MemoryInfo")]
    public Sprite recordImage;
    [TextArea]
    public string RecordInfo;

    public bool havePickup;
}

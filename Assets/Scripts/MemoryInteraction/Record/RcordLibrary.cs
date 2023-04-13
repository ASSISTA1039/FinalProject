using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New RecordLibrary", menuName = "RecordLibrary/New RcordLibrary")]
public class RcordLibrary : ScriptableObject
{
    [SerializeField]
    List<int> index = new List<int>();
    [SerializeField]
    List<Record> recordList = new List<Record>();

    public List<int> Index { get => index; set => index = value; }
    public List<Record> RecordList { get => recordList; set => recordList = value; }
}

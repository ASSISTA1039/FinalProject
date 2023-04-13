using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Save", menuName = "SaveLibrary/New Save")]
public class PaySave : ScriptableObject
{
    [Header("ID")]
    public int Saveid;

    public bool hasaved;
    public bool isPaying;
}

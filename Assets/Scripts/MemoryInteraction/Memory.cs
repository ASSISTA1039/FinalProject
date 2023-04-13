using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Memory", menuName = "MemoryLibrary/New Memory")]
public class Memory : ScriptableObject
{

    [Header("ID")]
    public int memoryid;

    [Header("MemoryInfo")]
    public string memoryName;
    public bool goodmemory;
    public Sprite memoyImage;
    //public PlayerPrefs memoryObject;

    [TextArea]
    public string memoryInfo;

    public bool haveread;
}
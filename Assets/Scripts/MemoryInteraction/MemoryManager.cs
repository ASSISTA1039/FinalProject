using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryManager : MonoBehaviour
{
    static MemoryManager instance;
    static DictionaryScript_GoodMemory my_goodMemory;
    static DictionaryScript_BadMemory my_badMemory;
    public GoodMemory mygoodmemorybag;
    public BadMemory mybadmemorybag;
    public Text MemoryInfromation;

    public List<GameObject> gmemorys = new List<GameObject>();//管理生成的16个slots
    public List<GameObject> bmemorys = new List<GameObject>();//管理生成的16个slots

    void Awake()
    {
        my_goodMemory = GetComponent<DictionaryScript_GoodMemory>();
        my_badMemory = GetComponent<DictionaryScript_BadMemory>();

        if (instance != null)
            Destroy(this);
        instance = this;

        foreach (var Key in mygoodmemorybag.Index)
        {
            instance.gmemorys[Key].GetComponent<MemorySlot>().slotImage.gameObject.SetActive(true);
        }
        foreach (var Key in mybadmemorybag.Index)
        {
            instance.bmemorys[Key].GetComponent<MemorySlot>().slotImage.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        instance.MemoryInfromation.text = "";
    }

    public static void UpdateItemInfo(string memoryDescription)
    {
        instance.MemoryInfromation.text = memoryDescription;
    }

    public static void CompareMemory(Memory memory)
    {
        if(memory.goodmemory)
        {
            instance.gmemorys[memory.memoryid].GetComponent<MemorySlot>().slotImage.sprite = memory.memoyImage;
            instance.gmemorys[memory.memoryid].GetComponent<MemorySlot>().slotImage.gameObject.SetActive(true);
        }
            
        else
        {
            instance.bmemorys[memory.memoryid].GetComponent<MemorySlot>().slotImage.sprite = memory.memoyImage;
            instance.bmemorys[memory.memoryid].GetComponent<MemorySlot>().slotImage.gameObject.SetActive(true);
        }
    }
}

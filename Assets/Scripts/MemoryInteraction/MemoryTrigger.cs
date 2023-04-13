using UnityEngine;
using UnityEngine.Events;

public class MemoryTrigger : MonoBehaviour
{
    public GoodMemory goodmemoryBag;
    public BadMemory badmemoryBag;
    public Memory memory;

    [Header("TIMEEVENT")]
    public UnityEvent action;

    [Header("Tip")]
    private GameObject pickupTip;

    private void Awake()
    {
        if (goodmemoryBag.Good_memoryList.Contains(memory))
        {
            Destroy(gameObject);
        }
        else if (badmemoryBag.Bad_memoryList.Contains(memory))
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        pickupTip = gameObject.transform.Find("PickupTip").gameObject;
        pickupTip.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pickupTip.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.E) && 
            other.gameObject.CompareTag("Player"))
        {
            if (memory.goodmemory)
            {
                if (!goodmemoryBag.Good_memoryList.Contains(memory))
                {
                    other.GetComponent<DictionaryScript_GoodMemory>().OnBeforeSerialize();
                    other.GetComponent<DictionaryScript_GoodMemory>().DeserializeDictionary(memory.memoryid, memory);
                    memory.haveread = true;
                    AddNewMemory();
                    action.Invoke();
                }
            }
            else
            {
                if (!badmemoryBag.Bad_memoryList.Contains(memory))
                {
                    other.GetComponent<DictionaryScript_BadMemory>().OnBeforeSerialize();
                    other.GetComponent<DictionaryScript_BadMemory>().DeserializeDictionary(memory.memoryid, memory);
                    memory.haveread = true;
                    AddNewMemory();
                    action.Invoke();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pickupTip.SetActive(false);
    }

    public void AddNewMemory()
    {
        MemoryManager.CompareMemory(memory);
    }

    public void MemoryDestory()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PointToSave : MonoBehaviour
{
    [Header("-- System Data --")]
    [SerializeField] SaveItem bagsave;

    [Header("SavePointInfo")]
    public PaySave payToSave;
    [SerializeField]
    private bool isEntered;

    [Header("-- Player Data --")]
    [SerializeField] PlayerData playerData;
    public static bool changingEquip_SavePoint;
    public static bool hasaved = false;
    private Transform self_transform;
    private Vector3 save_pos;

    [Header("Memory")]
    public Memory memory;
    public GoodMemory goodmemoryBag;
    public BadMemory badmemoryBag;

    [Header("TIMER EVENT")]
    public UnityEvent timerAction;
    public UnityEvent PayAction;

    [Header("Tip")]
    private GameObject restTip;

    [Header("Interact")]
    bool caninteract;

    private void Start()
    {
        self_transform = GetComponent<Rigidbody2D>().transform;
        restTip = gameObject.transform.Find("RestTip").gameObject;
        restTip.SetActive(false);
    }

    private void Update()
    {
        if(isEntered && Input.GetKey(KeyCode.E) && !payToSave.hasaved)
        {
            Debug.Log("Ask IF YOU HAVE 5 SHAKINGLIGHT");
            payToSave.isPaying = true;
            PayAction.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
            restTip.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
/*            if(Input.GetKey(KeyCode.E) && caninteract)
        {
            caninteract = true;
        }*/
            /*caninteract && */
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            isEntered = true;
            changingEquip_SavePoint = true;
            if (payToSave.hasaved)
            {
                if (goodmemoryBag.Good_memoryList.Contains(memory) || badmemoryBag.Bad_memoryList.Contains(memory))
                {
                    PlayerHealth.temp_health = HealthBar.HealthMax;
                    PlayerEnegy.temp_energy = EnegyBar.EnegyMax;
                    bagsave.Save();
                    playerData.Save();
                    playerData.Save_ShakingLight();
                    Debug.Log(playerData.Position);
                }
                else
                {
                    if(memory.goodmemory)
                    {
                        Debug.Log("GIVE YOU WATCH A TALK");
                        collision.GetComponent<DictionaryScript_GoodMemory>().OnBeforeSerialize();
                        collision.GetComponent<DictionaryScript_GoodMemory>().DeserializeDictionary(memory.memoryid, memory);
                        memory.haveread = true;
                        bagsave.Save();
                        AddNewMemory();
                        timerAction.Invoke();
                    }
                    else
                    {
                        Debug.Log("GIVE YOU WATCH A TALK");
                        collision.GetComponent<DictionaryScript_BadMemory>().OnBeforeSerialize();
                        collision.GetComponent<DictionaryScript_BadMemory>().DeserializeDictionary(memory.memoryid, memory);
                        memory.haveread = true;
                        bagsave.Save();
                        AddNewMemory();
                        timerAction.Invoke();
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            changingEquip_SavePoint = false;
        }
        restTip.SetActive(false);
        caninteract = false;
        isEntered = false;
    }

    public void AddNewMemory()
    {
        MemoryManager.CompareMemory(memory);
    }
}

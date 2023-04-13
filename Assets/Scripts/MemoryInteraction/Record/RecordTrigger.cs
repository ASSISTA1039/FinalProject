using UnityEngine;
using UnityEngine.Events;

public class RecordTrigger : MonoBehaviour
{
    public RcordLibrary recordBag;
    public Record record;

    [Header("TIMEEVENT")]
    public UnityEvent action;

    [Header("Tip")]
    private GameObject pickupTip;

    private void Awake()
    {
        if (recordBag.RecordList.Contains(record))
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
            if (!recordBag.RecordList.Contains(record))
            {
                other.GetComponent<DictionaryScript_Record>().OnBeforeSerialize();
                other.GetComponent<DictionaryScript_Record>().DeserializeDictionary(record.Recordid, record);
                record.havePickup = true;
                AddNewRecord();
                action.Invoke();
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pickupTip.SetActive(false);
    }


    public void AddNewRecord()
    {
        RecordManager.CompareRecord(record);
    }

    public void RecordDestory()
    {
        Destroy(gameObject);
    }
}

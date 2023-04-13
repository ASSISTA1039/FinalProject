using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordManager : MonoBehaviour
{
    static RecordManager instance;
    static DictionaryScript_Record my_record;
    public RcordLibrary myrecordbag;
    public Text RecordInfromation;

    public List<GameObject> records = new List<GameObject>();


    void Awake()
    {
        my_record = GetComponent<DictionaryScript_Record>();

        if (instance != null)
            Destroy(this);
        instance = this;

        foreach (var Key in myrecordbag.Index)
        {
            instance.records[Key].GetComponent<RecordSlot>().slotImage.gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        instance.RecordInfromation.text = "";
    }

    public static void UpdateRecordInfo(string recordDescription)
    {
        instance.RecordInfromation.text = recordDescription;
    }

    public static void CompareRecord(Record record)
    {
        instance.records[record.Recordid].GetComponent<RecordSlot>().slotImage.sprite = record.recordImage;
        instance.records[record.Recordid].GetComponent<RecordSlot>().slotImage.gameObject.SetActive(true);
    }
}

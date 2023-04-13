using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryScript_Record : MonoBehaviour, ISerializationCallbackReceiver
{
    

    [SerializeField]
    private RcordLibrary dictionaryData;
    [SerializeField]
    public List<int> keys = new List<int>();
    [SerializeField]
    public List<Record> values = new List<Record>();

    private Dictionary<int, Record> myDictionary = new Dictionary<int, Record>();

    public bool modifyValues;

    public int currentcount;

    public void OnBeforeSerialize()
    {
        if (modifyValues == false)
        {
            keys.Clear();
            values.Clear();
            for (int i = 0; i < Mathf.Min(dictionaryData.Index.Count, dictionaryData.RecordList.Count); i++)
            {
                keys.Add(dictionaryData.Index[i]);
                values.Add(dictionaryData.RecordList[i]);
            }
            currentcount = Mathf.Min(dictionaryData.Index.Count, dictionaryData.RecordList.Count);
        }
    }

    public void OnAfterDeserialize()
    {

    }

    public void DeserializeDictionary()
    {
        myDictionary = new Dictionary<int, Record>();
        dictionaryData.Index.Clear();
        dictionaryData.RecordList.Clear();
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryData.Index.Add(keys[i]);
            dictionaryData.RecordList.Add(values[i]);
            myDictionary.Add(keys[i], values[i]);
        }
        modifyValues = false;
    }

    public void DeserializeDictionary(int key, Record value)
    {
        myDictionary = new Dictionary<int, Record>();
        dictionaryData.Index.Clear();
        dictionaryData.RecordList.Clear();
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryData.Index.Add(keys[i]);
            dictionaryData.RecordList.Add(values[i]);
            myDictionary.Add(keys[i], values[i]);
        }
        dictionaryData.Index.Add(key);
        dictionaryData.RecordList.Add(value);
        myDictionary.Add(key, value);
        modifyValues = false;
    }

    public void DeleteDictionary(int key, Record value)
    {
        myDictionary = new Dictionary<int, Record>();
        dictionaryData.Index.Clear();
        dictionaryData.RecordList.Clear();
        keys.Remove(key);
        values.Remove(value);
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryData.Index.Add(keys[i]);
            dictionaryData.RecordList.Add(values[i]);
            myDictionary.Add(keys[i], values[i]);
        }
        modifyValues = false;
    }

    public void DestoryDictionary()
    {
        myDictionary = new Dictionary<int, Record>();
        dictionaryData.Index.Clear();
        dictionaryData.RecordList.Clear();
        modifyValues = false;
    }

    public void ResetPickUp_Attribute()
    {
        for (int i = 0; i < dictionaryData.RecordList.Count; i++)
        {
            if (!dictionaryData.RecordList[i])
                return;
            dictionaryData.RecordList[i].havePickup = false;
        }
    }
}

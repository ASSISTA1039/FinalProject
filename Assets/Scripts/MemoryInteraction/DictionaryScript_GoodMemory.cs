﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryScript_GoodMemory : MonoBehaviour, ISerializationCallbackReceiver
{
    

    [SerializeField]
    private GoodMemory dictionaryData;
    [SerializeField]
    public List<int> keys = new List<int>();
    [SerializeField]
    public List<Memory> values = new List<Memory>();

    private Dictionary<int, Memory> myDictionary = new Dictionary<int, Memory>();

    public bool modifyValues;

    public int currentcount;

    public void OnBeforeSerialize()
    {
        if (modifyValues == false)
        {
            keys.Clear();
            values.Clear();
            for (int i = 0; i < Mathf.Min(dictionaryData.Index.Count, dictionaryData.Good_memoryList.Count); i++)
            {
                keys.Add(dictionaryData.Index[i]);
                values.Add(dictionaryData.Good_memoryList[i]);
            }
            currentcount = Mathf.Min(dictionaryData.Index.Count, dictionaryData.Good_memoryList.Count);
        }
    }

    public void OnAfterDeserialize()
    {

    }

    public void DeserializeDictionary()
    {
        Debug.Log("DESERIALIZATION");
        myDictionary = new Dictionary<int, Memory>();
        dictionaryData.Index.Clear();
        dictionaryData.Good_memoryList.Clear();
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryData.Index.Add(keys[i]);
            dictionaryData.Good_memoryList.Add(values[i]);
            myDictionary.Add(keys[i], values[i]);
        }
        modifyValues = false;
    }

    public void DeserializeDictionary(int key, Memory value)
    {
        Debug.Log("DESERIALIZATION");
        myDictionary = new Dictionary<int, Memory>();
        dictionaryData.Index.Clear();
        dictionaryData.Good_memoryList.Clear();
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryData.Index.Add(keys[i]);
            dictionaryData.Good_memoryList.Add(values[i]);
            myDictionary.Add(keys[i], values[i]);
        }
        dictionaryData.Index.Add(key);
        dictionaryData.Good_memoryList.Add(value);
        myDictionary.Add(key, value);
        modifyValues = false;
    }

    public void DeleteDictionary(int key, Memory value)
    {
        Debug.Log("DESERIALIZATION");
        myDictionary = new Dictionary<int, Memory>();
        dictionaryData.Index.Clear();
        dictionaryData.Good_memoryList.Clear();
        keys.Remove(key);
        values.Remove(value);
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryData.Index.Add(keys[i]);
            dictionaryData.Good_memoryList.Add(values[i]);
            myDictionary.Add(keys[i], values[i]);
        }
        modifyValues = false;
    }

    public void DestoryDictionary()
    {
        Debug.Log("DESERIALIZATION");
        myDictionary = new Dictionary<int, Memory>();
        dictionaryData.Index.Clear();
        dictionaryData.Good_memoryList.Clear();
        modifyValues = false;
    }

    public void Resethadread_Attribute()
    {
        for (int i = 0; i < dictionaryData.Good_memoryList.Count; i++)
        {
            if (!dictionaryData.Good_memoryList[i])
                return;
            dictionaryData.Good_memoryList[i].haveread = false;
        }
    }

    public void PrintDictionary()
    {
        foreach (var pair in myDictionary)
        {
            Debug.Log("Key: " + pair.Key + " Value: " + pair.Value);
        }
    }
}

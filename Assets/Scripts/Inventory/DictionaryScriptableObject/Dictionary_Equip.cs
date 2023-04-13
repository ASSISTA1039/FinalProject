using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dictionary_Equip : MonoBehaviour, ISerializationCallbackReceiver
{
    

    [SerializeField]
    private Inventory dictionaryData;
    [SerializeField]
    public List<int> keys = new List<int>();
    [SerializeField]
    public List<Item> values = new List<Item>();

    private Dictionary<int, Item> myDictionary = new Dictionary<int, Item>();

    public bool modifyValues;

    public int currentcount;

    public void OnBeforeSerialize()
    {
        if (modifyValues == false)
        {
            keys.Clear();
            values.Clear();
            for (int i = 0; i < Mathf.Min(dictionaryData.Keys.Count, dictionaryData.Values.Count); i++)
            {
                keys.Add(dictionaryData.Keys[i]);
                values.Add(dictionaryData.Values[i]);
            }
            currentcount = Mathf.Min(dictionaryData.Keys.Count, dictionaryData.Values.Count);
        }
    }

    public void OnAfterDeserialize()
    {

    }

    public void DeserializeDictionary()
    {
        myDictionary = new Dictionary<int, Item>();
        dictionaryData.Keys.Clear();
        dictionaryData.Values.Clear();
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryData.Keys.Add(keys[i]);
            dictionaryData.Values.Add(values[i]);
            myDictionary.Add(keys[i], values[i]);
        }
        modifyValues = false;
    }

    public void DeserializeDictionary(int key, Item value)
    {
        myDictionary = new Dictionary<int, Item>();
        dictionaryData.Keys.Clear();
        dictionaryData.Values.Clear();
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryData.Keys.Add(keys[i]);
            dictionaryData.Values.Add(values[i]);
            myDictionary.Add(keys[i], values[i]);
        }
        dictionaryData.Keys.Add(key);
        dictionaryData.Values.Add(value);
        myDictionary.Add(key, value);
        modifyValues = false;
    }

    public void DeleteDictionary(int key, Item value)
    {
        myDictionary = new Dictionary<int, Item>();
        dictionaryData.Keys.Clear();
        dictionaryData.Values.Clear();
        keys.Remove(key);
        values.Remove(value);
        for (int i = 0; i < Mathf.Min(keys.Count, values.Count); i++)
        {
            dictionaryData.Keys.Add(keys[i]);
            dictionaryData.Values.Add(values[i]);
            myDictionary.Add(keys[i], values[i]);
        }
        modifyValues = false;
    }

    public void DestoryDictionary()
    {
        myDictionary = new Dictionary<int, Item>();
        dictionaryData.Keys.Clear();
        dictionaryData.Values.Clear();
        modifyValues = false;
    }

    public void ResetPickUp_Attribute()
    {
        for (int i = 0; i < dictionaryData.Values.Count; i++)
        {
            if (!dictionaryData.Values[i])
                return;
            dictionaryData.Values[i].pickup = false;
            dictionaryData.Values[i].equip = false;
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

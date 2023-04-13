using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.Events;

public class SaveItem : MonoBehaviour
{
    //public Inventory myEquip;
    public Inventory myInventory;
    public GoodMemory myGoodMemory;
    public BadMemory myBadMemory;
    public RcordLibrary myRcordLibrary;
    public GameEvent[] myevent;
    public PaySave[] pay;
    public UnityEvent action;
    public UnityEvent deserializeinventory;
    public UnityEvent deserializeGoodMemory;
    public UnityEvent deserializeBadMemory;
    public UnityEvent deserializeRcord;

    public Item[] items;

    private void Awake()
    {
        Load();
        for (int i =0;i<myInventory.Values.Count;i++)
        {
            myInventory.Values[i] = items[myInventory.Keys[i]];
            items[myInventory.Keys[i]].pickup = true;
        }
/*        for (int i = 0; i < myEquip.Values.Count; i++)
        {
            if (!myEquip.Values[i])
            {
                myEquip.Values[i] = items[myEquip.Keys[i]];
                items[myEquip.Keys[i]].equip = true;
            }
        }*/
        Save();
    }
    public void  DestoryItem()
    {
        foreach(var _item in items)
        {
            _item.pickup = false;
            _item.equip = false;
        }
        myInventory.Keys.Clear();
        myInventory.Values.Clear();
    }    
    public void Save()
    {
        Debug.Log(Application.persistentDataPath + "/game_BagData");
        if(!Directory.Exists(Application.persistentDataPath + "/game_BagData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_BagData");
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file1 = File.Create(Application.persistentDataPath + "/game_BagData/inventory.txt");
        //FileStream file5 = File.Create(Application.persistentDataPath + "/game_BagData/equip.txt");
        FileStream file2 = File.Create(Application.persistentDataPath + "/game_BagData/goodMemory.txt");
        FileStream file3 = File.Create(Application.persistentDataPath + "/game_BagData/badMemory.txt");
        FileStream file4 = File.Create(Application.persistentDataPath + "/game_BagData/rcordLibrary.txt");
        action.Invoke();
        var json1 = JsonUtility.ToJson(myInventory);
        var json2 = JsonUtility.ToJson(myGoodMemory);
        var json3 = JsonUtility.ToJson(myBadMemory);
        var json4 = JsonUtility.ToJson(myRcordLibrary);
        //var json5 = JsonUtility.ToJson(myEquip);

        formatter.Serialize(file1, json1);
        formatter.Serialize(file2, json2);
        formatter.Serialize(file3, json3);
        formatter.Serialize(file4, json4);
        //formatter.Serialize(file5, json5);

        file1.Close();
        file2.Close();
        file3.Close();
        file4.Close();
        //file5.Close();
        foreach(var _event in myevent)
        {
            FileStream file = File.Create(Application.persistentDataPath + "/game_BagData/" + _event.ToString() + ".txt");
            var json = JsonUtility.ToJson(_event);
            formatter.Serialize(file, json);
            file.Close();
        }

        foreach (var _pay in pay)
        {
            FileStream file = File.Create(Application.persistentDataPath + "/game_BagData/" + _pay.ToString() + ".txt");
            var json = JsonUtility.ToJson(_pay);
            formatter.Serialize(file, json);
            file.Close();
        }
    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + "/game_BagData/inventory.txt"))
        {
            FileStream file1 = File.Open(Application.persistentDataPath + "/game_BagData/inventory.txt", FileMode.Open);
            //deserializeinventory.Invoke();
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file1), myInventory);

            file1.Close();
        }
/*        if (File.Exists(Application.persistentDataPath + "/game_BagData/equip.txt"))
        {
            FileStream file5 = File.Open(Application.persistentDataPath + "/game_BagData/equip.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file5), myEquip);

            file5.Close();
        }*/
        if (File.Exists(Application.persistentDataPath + "/game_BagData/goodMemory.txt"))
        {
            FileStream file2 = File.Open(Application.persistentDataPath + "/game_BagData/goodMemory.txt", FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file2), myGoodMemory);

            file2.Close();
        }
        if (File.Exists(Application.persistentDataPath + "/game_BagData/badMemory.txt"))
        {
            FileStream file3 = File.Open(Application.persistentDataPath + "/game_BagData/badMemory.txt", FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file3), myBadMemory);

            file3.Close();
        }
        if (File.Exists(Application.persistentDataPath + "/game_BagData/rcordLibrary.txt"))
        {
            FileStream file4 = File.Open(Application.persistentDataPath + "/game_BagData/rcordLibrary.txt", FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file4), myRcordLibrary);

            file4.Close();
        }

        foreach (var _event in myevent)
        {
            if (File.Exists(Application.persistentDataPath + "/game_BagData/" + _event.ToString() + ".txt"))
            {
                FileStream file = File.Open(Application.persistentDataPath + "/game_BagData/" + _event.ToString() + ".txt", FileMode.Open);

                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), _event);

                file.Close();
            }
        }

        foreach (var _pay in pay)
        {
            if (File.Exists(Application.persistentDataPath + "/game_BagData/" + _pay.ToString() + ".txt"))
            {
                FileStream file = File.Open(Application.persistentDataPath + "/game_BagData/" + _pay.ToString() + ".txt", FileMode.Open);

                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), _pay);

                file.Close();
            }
        }
    }
}

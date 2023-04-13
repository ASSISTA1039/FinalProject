using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    static Dictionary_Inventory my_inventory;
    public Inventory myBag;
    //public GameObject slotGrid;
    // public Slot slotPrefab;
    public GameObject emptySlot;
    public Text itemInfromation;

    public List<GameObject> slots = new List<GameObject>();//管理生成的16个slots

    void Awake()
    {
        my_inventory = GetComponent<Dictionary_Inventory>();
        
        if (instance != null)
            Destroy(this);
        instance = this;

        foreach(var Key in myBag.Keys)
        {
            instance.slots[Key].GetComponent<Slot>().slotImage.gameObject.SetActive(true);
            if (instance.slots[Key].GetComponent<Slot>().slotItem.equip)
            {
                instance.slots[Key].GetComponent<Slot>().slotImage.color = new Color(instance.slots[Key].GetComponent<Slot>().slotImage.color.r, instance.slots[Key].GetComponent<Slot>().slotImage.color.g, instance.slots[Key].GetComponent<Slot>().slotImage.color.b, 0.3f);
            }

        }
    }

    private void OnEnable()
    {
        //RefreshItem();
        instance.itemInfromation.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInfromation.text = itemDescription;
    }

    public static void CompareItem(Item item)
    {
        //instance.slots[item.itemid].GetComponent<Slot>().slotImage.sprite = item.itemImage;
        instance.slots[item.itemid].GetComponent<Slot>().slotImage.gameObject.SetActive(true);
        //Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        //newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        //newItem.slotItem = item;
        //newItem.slotImage.sprite = item.itemImage;
        //newItem.slotNum.text = item.itemHeld.ToString();
    }

    /*    public static void RefreshItem()
        {
            //循环删除slotGrid下的子集物体
            for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
            {
                if (instance.slotGrid.transform.childCount == 0)
                    break;
                Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
                instance.slots.Clear();
            }

            //重新生成对应myBag里面的物品的slot
            for (int i = 0; i < instance.myBag.itemList.Count; i++)
            {
                // CreateNewItem(instance.myBag.itemList[i]);
                instance.slots.Add(Instantiate(instance.emptySlot));
                instance.slots[i].transform.SetParent(instance.slotGrid.transform);
                instance.slots[i].GetComponent<Slot>().slotID = i;
                instance.slots[i].GetComponent<Slot>().SetupSlot(instance.myBag.itemList[i]);
            }
        }*/
}

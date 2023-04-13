using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemOnWorld : MonoBehaviour
{
    [Header("ItemInfo")]
    public Item thisItem;
    public string itemInfo;
    public Inventory playerInventory;
    [Header("-- System Data --")]
    [SerializeField] SaveItem bagsave;
    [Header("Tip")]
    private GameObject pickupTip;
    public GameObject tipCanvas;
    public Text itemtext;
    private void Awake()
    {
        if (playerInventory.Values.Contains(thisItem))
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pickupTip = gameObject.transform.Find("PickupTip").gameObject;
        pickupTip.SetActive(false);
        itemInfo = thisItem.itemInfo;
        tipCanvas = GameObject.Find("Tip_Item_Canvas").transform.Find("Panel").gameObject;
        itemtext = tipCanvas.transform.Find("Info").gameObject.GetComponent<Text>();
        bagsave = GameObject.Find("SaveItem").GetComponent<SaveItem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pickupTip.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKey(KeyCode.E) && other.gameObject.CompareTag("Player"))
        {
            if (!playerInventory.Values.Contains(thisItem))
            {
                other.GetComponent<Dictionary_Inventory>().OnBeforeSerialize();
                other.GetComponent<Dictionary_Inventory>().DeserializeDictionary(thisItem.itemid, thisItem);
                thisItem.pickup = true;
                AddNewItem();
                tipCanvas.SetActive(true);
                itemtext.text = itemInfo;
                bagsave.Save();
                Invoke("StopTipCanvas", 2f);
                //音效播放！！！
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pickupTip.SetActive(false);
    }
    public void AddNewItem()
    {
        InventoryManager.CompareItem(thisItem);
        /*        if (!playerInventory.Values.Contains(thisItem))
                {
                    playerInventory.Values[thisItem.itemid]=thisItem;
                    InventoryManager.CompareItem(thisItem);
                }*/
    }

    public void StopTipCanvas()
    {
        tipCanvas.SetActive(false);
        Destroy(gameObject);
    }
}

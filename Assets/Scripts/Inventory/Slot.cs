using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yuliang.UI.Dark;
public class Slot : MonoBehaviour
{
    [Header("ItemInfo")]
    public int slotID;//空格ID 等于 物品ID
    public Item slotItem;
    public Image slotImage;
    public string slotInfo;
    public GameObject itemInSlot;
    public bool ifchanged;
    [Space]
    [Header("ChangeItem")]
    public GameObject equip;
    public GameObject inventory;
    private Dictionary_Inventory inventorynote;
    private Dictionary_Inventory equipnote;


    private void Start()
    {
        equipnote = equip.GetComponent<Dictionary_Inventory>();
        inventorynote = inventory.GetComponent<Dictionary_Inventory>();
        equipnote.OnBeforeSerialize();
        if(transform.parent.gameObject == equip)
        {
            if(equipnote.keys.Contains(slotID))
                slotItem = equipnote.values[equipnote.keys.IndexOf(slotID)];
        }
        if (slotItem)
        {
            ifchanged = slotItem.equip;
            slotInfo = slotItem.itemInfo;
        }
        if(ifchanged && transform.parent.gameObject == inventory)
        {
            slotImage.color = new Color(slotImage.color.r, slotImage.color.g, slotImage.color.b, 0.3f);
        }
        if (ifchanged && transform.parent.gameObject == equip)
        {
            slotImage.gameObject.SetActive(true);
            slotImage.sprite = slotItem.itemImage;
        }

    }
/*    private void Update()
    {
        if (ifchanged && transform.parent.gameObject == inventory)
        {
            slotInfo = "";
            InventoryManager.UpdateItemInfo(slotInfo);
        }
    }*/
    public void ItemOnClicked()
    {
        if (slotItem && !slotItem.equip && slotItem.pickup)
        {
            slotInfo = slotItem.itemInfo;
            InventoryManager.UpdateItemInfo(slotInfo);
        }
        if(slotItem && transform.parent.gameObject == equip)
        {
            slotInfo = slotItem.itemInfo;
            InventoryManager.UpdateItemInfo(slotInfo);
        }
    }

    public void Right_Inventory_ItemOnClicked()
    {
        if (PointToSave.changingEquip_SavePoint)
        {
            if (slotItem && slotItem.pickup && !ifchanged)
            {
                equipnote.OnBeforeSerialize();
                if (equipnote.currentcount < 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!equip.GetComponent<Transform>().
                            GetChild(i).gameObject.
                            GetComponent<Slot>().slotItem)
                        {
                            HealthBuff();
                            EnergyBuff();
                            AttackBuff();
                            DoubleGrapplingBuff();
                            FireToPlayerBuff();
                            equipnote.DeserializeDictionary(i, slotItem);
                            equip.GetComponent<Transform>().
                                GetChild(i).gameObject.
                                GetComponent<Slot>().slotItem = slotItem;
                            //这个ID待定，可能以后会改为索引值
                            //equip.GetComponent<Transform>().
                            //    GetChild(i).gameObject.
                            //    GetComponent<Slot>().slotID = ;
                            equip.GetComponent<Transform>().
                                GetChild(i).gameObject.
                                GetComponent<Slot>().slotImage.sprite = slotItem.itemImage;
                            equip.GetComponent<Transform>().
                                GetChild(i).gameObject.
                                GetComponent<Slot>().slotImage.gameObject.SetActive(true);
                            equip.GetComponent<Transform>().
                                GetChild(i).gameObject.
                                GetComponent<Slot>().slotInfo = slotItem.itemInfo;
                            slotImage.color = new Color(slotImage.color.r, slotImage.color.g, slotImage.color.b, 0.3f);
                            slotInfo = "";
                            ifchanged = true;
                            slotItem.equip = true;
                            InventoryManager.UpdateItemInfo(slotInfo);
                            return;
                        }
                    }
                }
            }
        }
    }

    public void Right_Equip_ItemOnClicked()
    {
        if (PointToSave.changingEquip_SavePoint)
        {
            if (slotItem)
            {
                EnergyDe();
                HealthDe();
                AttackDe();
                SingleGrapplingBuff();
                FireToPlayerBuff();
                equipnote.OnBeforeSerialize();
                equipnote.DeleteDictionary(slotID, slotItem);

                inventory.GetComponent<Transform>().
                    GetChild(slotItem.itemid).gameObject.
                    GetComponent<Slot>().slotImage.color =
                    new Color(slotImage.color.r, slotImage.color.g, slotImage.color.b, 1);
                inventory.GetComponent<Transform>().
                    GetChild(slotItem.itemid).gameObject.
                    GetComponent<Slot>().slotInfo = slotItem.itemInfo;
                inventory.GetComponent<Transform>().
                    GetChild(slotItem.itemid).gameObject.
                    GetComponent<Slot>().ifchanged = false;
                slotItem.equip = false;
                ifchanged = false;
                slotItem = null;
                //这个ID待定，可能以后会改为索引值
                slotImage.sprite = null;
                slotImage.gameObject.SetActive(false);
                slotInfo = "";
                InventoryManager.UpdateItemInfo(slotInfo);
            }
        }
    }

    public void SetupSlot(Item item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        slotImage.sprite = item.itemImage;
        slotInfo = item.itemInfo;

    }

    public void HealthBuff()
    {
        PlayerHealth.temp_health += slotItem.buff_HP;
        HealthBar.HealthMax = PlayerHealth.temp_health;
        HealthBar.HealthCurrent = PlayerHealth.temp_health;
    }

    public void EnergyBuff()
    {
        PlayerEnegy.temp_energy += slotItem.buff_Energy;
        EnegyBar.EnegyMax = PlayerEnegy.temp_energy;
        EnegyBar.EnegyCurrent = PlayerEnegy.temp_energy;
    }

    public void AttackBuff()
    {
        PlayerAttack.temp_damage += slotItem.buff_Attack;
    }
    public void HealthDe()
    {
        PlayerHealth.temp_health -= slotItem.buff_HP;
        HealthBar.HealthMax = PlayerHealth.temp_health;
        HealthBar.HealthCurrent = PlayerHealth.temp_health;
    }

    public void EnergyDe()
    {
        PlayerEnegy.temp_energy -= slotItem.buff_Energy;
        EnegyBar.EnegyMax = PlayerEnegy.temp_energy;
        EnegyBar.EnegyCurrent = PlayerEnegy.temp_energy;
    }

    public void AttackDe()
    {
        PlayerAttack.temp_damage -= slotItem.buff_Attack;
    }

    public void DoubleGrapplingBuff()
    {
        GrapplingGun1.canDoubleGrappling = slotItem.doubleGrappling;
    }

    public void SingleGrapplingBuff()
    {
        if(slotItem.doubleGrappling)
            GrapplingGun1.canDoubleGrappling = false;
    }

    public void FireToPlayerBuff()
    {

    }
}

using UnityEngine;
using UnityEngine.UI;


public class MemorySlot : MonoBehaviour
{
    [Header("MemoryInfo")]
    public int slotID;//Space ID is equal to the item ID
    public Memory slotMemory;
    public Image slotImage;
    public string slotInfo;


    private void Awake()
    {
        slotID = slotMemory.memoryid;

        if (slotMemory.haveread)
        {
            slotInfo = slotMemory.memoryInfo;
            slotImage.sprite = slotMemory.memoyImage;
        }
    }

    public void MemoryOnClicked()
    {
        if (slotMemory.haveread)
        {
            slotInfo = slotMemory.memoryInfo;
            MemoryManager.UpdateItemInfo(slotInfo);
        }
    }
}

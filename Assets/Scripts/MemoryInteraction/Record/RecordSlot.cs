using UnityEngine;
using UnityEngine.UI;

public class RecordSlot : MonoBehaviour
{
    [Header("MemoryInfo")]
    public int slotID;//Space ID is equal to the record ID
    public Record slotRecord;
    public Image slotImage;
    public string slotInfo;


    private void Awake()
    {
        slotID = slotRecord.Recordid;
        
        if (slotRecord.havePickup)
        {
            slotInfo = slotRecord.RecordInfo;
            slotImage.sprite = slotRecord.recordImage;
        }
            
    }

    public void RecordOnClicked()
    {
        if (slotRecord.havePickup)
        {
            slotInfo = slotRecord.RecordInfo;
            RecordManager.UpdateRecordInfo(slotInfo);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSave : MonoBehaviour
{
    [Header("-- System Data --")]
    [SerializeField] SaveItem bagsave;

    public List<PaySave> paytoSave = new List<PaySave>();
    public Memory memory;

    private void Start()
    {
        ;
    }
    public void RestSavePoint_GameObject()
    {
        foreach(var pay in paytoSave)
        {
            pay.hasaved = false;
        }
        memory.haveread = false;
        bagsave.Save();
    }
}

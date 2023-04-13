using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Appear : MonoBehaviour
{
    public GameObject box;
    public GameObject blood;
    public GameObject boss;
    private bool appeared;
    private void Start()
    {
        appeared = true;
        box.SetActive(false);
        blood.SetActive(false);
    }
    private void Update()
    {
        if (boss)
        {
            if (boss.activeInHierarchy && appeared)
            {
                box.SetActive(true);
                blood.SetActive(true);
                appeared = false;
                blood.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, 0.01f);
            }
        }
        else
        {
            box.SetActive(false);
            blood.SetActive(false);
        }
    }
}

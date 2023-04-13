using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public int startCoinQuantity;
    public TMP_Text coinQuantity;
    public static int MaxCoinQuantity;
    public static int CurrentCoinQuantity;
    private Image CoinBar;

    public PlayerData playerdata;

    void Start()
    {
        CoinBar = GetComponent<Image>();
        if(TransitionPoint.changedScene)
            playerdata = GameObject.Find("Player").GetComponent<PlayerData>();
        if (!TransitionPoint.changedScene)
        {
            if(CurrentCoinQuantity != 0)
            { }    
            else
                CurrentCoinQuantity = startCoinQuantity;
        }
        else
        {
            playerdata.Load_PLayerState();
            CurrentCoinQuantity = playerdata.Coin;
        }
        
        MaxCoinQuantity = 200;
    }


    void Update()
    {
        CoinBar.fillAmount = (float)CurrentCoinQuantity / (float)MaxCoinQuantity;
        coinQuantity.text = CurrentCoinQuantity.ToString();
    }
}

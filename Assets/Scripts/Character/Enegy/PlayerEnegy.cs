using UnityEngine;
using UnityEngine.UI;

public class PlayerEnegy : MonoBehaviour
{
    [Header("Item_Buff")]
    public Dictionary_Equip items;

    [Header("Energy Details")]
    public float enegy = 10;
    public static float temp_energy;
    public  float _temp_energy;
    public float time;
    public float dieTime;
    public float hitBoxCdTime;

    [Header("PlayerInfo")]
    public SpriteRenderer myRender;
    public GrapplingRope rope;
    public GrapplingRope1 rope1;
    private PlayerData playerdata;

    [Header("EnergyCanvas")]
    public Image box;
    public Image enegyinbox;

    [Header("SavePoint_Restore")]
    private bool shouldRestore = false;


    void Start()
    {
        playerdata = GetComponent<PlayerData>();
        items = GetComponent<Dictionary_Equip>();
        if (TransitionPoint.changedScene)
        {
            playerdata.Load_PLayerState();
            temp_energy = playerdata.Energy;

            _temp_energy = enegy;
            for (int i = 0; i < items.values.Count; i++)
            {
                _temp_energy += items.values[i].buff_Energy;
            }
            EnegyBar.EnegyCurrent = temp_energy;
            EnegyBar.EnegyMax = _temp_energy;
        }
        else
        {
            temp_energy = enegy;
            for (int i = 0; i < items.values.Count; i++)
            {
                temp_energy += items.values[i].buff_Energy;
            }
            EnegyBar.EnegyCurrent = temp_energy;
            EnegyBar.EnegyMax = temp_energy;
        }

    }


    void Update()
    {
        //Energy Judgment
        PlayerPrefs.SetFloat("EP", temp_energy);
        if (temp_energy <= 0)
        {
            temp_energy = 0;
        }
        if (temp_energy > EnegyBar.EnegyMax)
            temp_energy = EnegyBar.EnegyMax;
        EnegyBar.EnegyCurrent = temp_energy;
        if (temp_energy < EnegyBar.EnegyMax / 5)
        {
            shouldRestore = true;
            //box.color = Color.Lerp(box.color, new Color32(197, 92, 100,255), 0.1f);
            enegyinbox.color = Color.Lerp(enegyinbox.color, new Color32(197, 92, 100, 255), 0.5f);
        }
        else if(temp_energy >= EnegyBar.EnegyMax / 5)
        {
            //box.color = Color.Lerp(box.color, new Color32(146, 202, 195,255), 0.1f);
            enegyinbox.color = Color.Lerp(enegyinbox.color, new Color32(162, 238, 136, 255), 0.5f);
        }
        if(shouldRestore)
            RestoreEnegy();

    }

    //Movement skills consume energy
    public void EnegyDecrease(int enegydown)
    {
        if(temp_energy >= EnegyBar.EnegyMax/5)
            temp_energy -= enegydown;
 
    }

    //Restore energy status when energy is low
    void RestoreEnegy()
    {
        if (temp_energy < (0.5f * EnegyBar.EnegyMax))
            temp_energy = Mathf.Lerp(temp_energy, EnegyBar.EnegyMax,0.0005f);
        else if (temp_energy >= EnegyBar.EnegyMax)
        {
            temp_energy = EnegyBar.EnegyMax;
            EnegyBar.EnegyCurrent = EnegyBar.EnegyMax;
            shouldRestore = false;
        }
        else
        {
            shouldRestore = false;
        }
    }
}

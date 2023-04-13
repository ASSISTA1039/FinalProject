using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnegyBar : MonoBehaviour
{
    //public TMP_Text healthText;
    public static float EnegyCurrent;
    public static float EnegyMax;

    private Image enegyBar;

    // Start is called before the first frame update
    void Start()
    {
        enegyBar = GetComponent<Image>();
        //HealthCurrent = HealthMax;
    }

    // Update is called once per frame
    void Update()
    {
        enegyBar.fillAmount = EnegyCurrent / EnegyMax;
        //healthText.text = HealthCurrent.ToString() + "/" + HealthMax.ToString();
    }
}

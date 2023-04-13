using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamingUI_BagKeyboardEvent : MonoBehaviour
{
    public Gaming_MainPanelManager manager;
    public HorizontalLayoutGroup horizontalgroup;
    public GameObject showLiist;
    public GameObject setting;
    public GameObject helper;
    private bool isOpen = false;
    public UnityEvent action;
    [Header("-- System Data --")]
    [SerializeField] SaveItem bagsave;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !isOpen)
        {
            isOpen = !isOpen;
            bagsave.Save();
            setting.SetActive(isOpen);
            if(isOpen)
                manager.PanelAnim(0);
            else
                manager.PanelAnim(1);
            horizontalgroup.enabled = !isOpen;
            horizontalgroup.enabled = isOpen;
            showLiist.SetActive(!isOpen);
            showLiist.SetActive(isOpen);
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && isOpen)
        {
            isOpen = !isOpen;
            action.Invoke();
        }
    }
}

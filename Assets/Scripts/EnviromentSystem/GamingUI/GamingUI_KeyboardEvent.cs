using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class GamingUI_KeyboardEvent : MonoBehaviour
{
    public Gaming_MainPanelManager manager;
    public HorizontalLayoutGroup horizontalgroup;
    public GameObject showLiist;
    public GameObject setting;
    public GameObject helper;
    private bool isOpen = false;
    public UnityEvent action;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isOpen)
        {
            isOpen = !isOpen;
            setting.SetActive(isOpen);
            if (isOpen)
                manager.PanelAnim(0);
            else
                manager.PanelAnim(1);
            horizontalgroup.enabled = !isOpen;
            horizontalgroup.enabled = isOpen;
            showLiist.SetActive(!isOpen);
            showLiist.SetActive(isOpen);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            isOpen = !isOpen;
            action.Invoke();
        }
    }
}

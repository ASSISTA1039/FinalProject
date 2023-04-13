using UnityEngine;
using UnityEngine.SceneManagement;


public class Save_LoadScene : MonoBehaviour
{
    private int save_chosen;

    public void OnLoginButtonClick()
    {
        save_chosen = PlayerPrefs.GetInt("CurrentScene");
        SceneManager.LoadScene(save_chosen);
    }
}

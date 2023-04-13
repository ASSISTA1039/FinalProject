using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Chapter_LoadScene : MonoBehaviour
{
    [Header("-- Player Data --")]
    [SerializeField] PlayerData playerData;
    private int chapter_chosen;
    public ModalWindowManager load_chapter;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }
    public void OnLoginButtonClick()
    {
        TransitionPoint.changedScene = false;
        PlayerData.enterScene = false;
        if (load_chapter.chapter != -1)
        {
            PlayerData.ifsaved = false;
            chapter_chosen = load_chapter.chapter;
            //SceneManager.LoadScene(chapter_chosen);
            //SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByBuildIndex(chapter_chosen));
        }
        else
        {
            playerData.Load();
            PlayerData.ifsaved = true;
            chapter_chosen = playerData.Level;
            //playerData.Save();
            SceneManager.LoadScene(chapter_chosen);
            //SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByBuildIndex(chapter_chosen));
        }
    }
}

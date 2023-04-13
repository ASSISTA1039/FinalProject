using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    static MiniMap instance;

    public List<Image> map = new List<Image>();

    private int mapindex;

    Color mapcolor;
    void Awake()
    {

        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        Scene scene = SceneManager.GetActiveScene();

        mapindex = scene.buildIndex - 1;
        mapcolor = map[mapindex].color;
        
    }

    private void Update()
    {
        //Debug.Log(mapindex);
        map[mapindex].color = Color.Lerp(mapcolor, new Color(255, 255, 0), 1f);
    }
}

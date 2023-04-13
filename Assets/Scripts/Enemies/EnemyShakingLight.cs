using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShakingLight : MonoBehaviour
{
    private SpriteRenderer renderer;
    public float fuzhuo_Time;
    public float duration;
    private bool isNormal = true;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(renderer.enabled == true && isNormal)
        {
            duration = Time.time + fuzhuo_Time;
            isNormal = false;
        }

        if(duration<Time.time)
        {
            isNormal = true;
            renderer.enabled = false;
        }
    }
}

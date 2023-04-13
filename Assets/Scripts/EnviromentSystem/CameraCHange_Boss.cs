using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCHange_Boss : MonoBehaviour
{
    private bool isEntered;
    private BoxCollider2D _collider;

    public Transform boss_door_start;
    public Transform startL;
    public Transform endL;
    public bool start = false;
    public Transform boss_door_end;
    public Transform startR;
    public Transform endR;
    public bool end = false;
    public CMChange cm;
    public GameObject boss;

    public GameEvent _event;

    private void Start()
    {
        if (_event.hasaved)
            Destroy(gameObject);
        _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = true;
        boss.SetActive(false);
    }
    private void Update()
    {
        if(start)
        {
            boss_door_start.position = Vector2.Lerp(boss_door_start.position, startL.position, 0.02f);
            boss_door_end.position = Vector2.Lerp(boss_door_end.position, startR.position, 0.02f);
        }
        if (!boss)
            end = true;
        else
            end = false; 
        if (end)
        {
            boss_door_start.transform.position = Vector2.MoveTowards(boss_door_start.position, endL.position, 0.02f);
            boss_door_end.transform.position = Vector2.MoveTowards(boss_door_end.position, endR.position, 0.02f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            if (cm)
            {
                CMChange.autotalk_Player = true;
                CMChange.normal_Player = false;
            }

            if (cm)
                cm.ChangeState();
            isEntered = true;
            boss.SetActive(true);
            start = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            isEntered = false;
            if(!boss)
                _collider.enabled = false;
            if (cm)
                cm.ChangeState();
            if (boss)
                boss.SetActive(false);
        }
    }
}

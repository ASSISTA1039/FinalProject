using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMChange : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    public static bool normal_Player = false;
    private static bool zoom_Player = false;
    public static bool autotalk_Player = false;
    private void Awake()
    {
        //anim.Play("Normal_Player");
        anim = GetComponent<Animator>();
        autotalk_Player = false;
        zoom_Player = false;
        normal_Player = false;
    }

    /*    private void OnEnable()
        {
            FindObjectOfType<Movement>().canMove = true;
        }

        private void OnDisable()
        {
            FindObjectOfType<Movement>().canMove = false;
            FindObjectOfType<Movement>().rb.velocity = Vector2.zero;
            FindObjectOfType<Movement>().anim.SetHorizontalMovement(0, 0, 0);
        }*/

    public void ChangeState()
    {
        if (autotalk_Player)
        {
            if (normal_Player)
            {
                anim.Play("Normal_Player");
                autotalk_Player = !autotalk_Player;
            }
            else
            {
                anim.Play("AutoTalk_Player");
            }
            normal_Player = !normal_Player;
        }
        else
        {
            if (normal_Player)
            {
                anim.Play("Normal_Player");
            }
            else
            {
                anim.Play("Zoom_Player");
            }
            normal_Player = !normal_Player;
        }
    }
}

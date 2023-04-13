using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door1 : MonoBehaviour
{
    [Header("-- Player Data --")]
    [SerializeField] SaveItem bagsave;


    private Animator anim;
    public GameEvent _event;

    private void Start()
    {
        if (_event.hasaved)
            Destroy(gameObject);
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key") && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            anim.Play("Door");
            Destroy(collision.gameObject);
        }
    }

    public void _Destory()
    {
        _event.hasaved = true;
        bagsave.Save();
        Destroy(gameObject);
    }
}

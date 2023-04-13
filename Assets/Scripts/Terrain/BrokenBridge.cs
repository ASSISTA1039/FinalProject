using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BrokenBridge : MonoBehaviour
{
    [Header("-- Player Data --")]
    [SerializeField] SaveItem bagsave;
    [Header("-- Physics --")]
    private BoxCollider2D _collider;
    public Rigidbody2D[] rigidbodies;
    private int playerfacing;
    private bool hasbroken;
    public CMChange cMChange = new CMChange();
    public GameEvent _event;


    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        if (_event.hasaved)
            Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.CompareTag("Player"))
        {
            playerfacing = collision.gameObject.GetComponentInParent<Transform>().rotation.y >= 0 ? 1 : -1;
            Invoke("Action", 2f);
        }
    }


    private void Action()
    {
        cMChange.ChangeState();
        _event.hasaved = true;
        bagsave.Save();
        FindObjectOfType<Movement>().canMove = false;
        FindObjectOfType<Movement>().rb.velocity = Vector2.zero;
        FindObjectOfType<Movement>().anim.SetHorizontalMovement(0, 0, 0);
        hasbroken = true;
        Invoke("ShakeAction", 1f);
    }

    void ShakeAction()
    {
        Invoke("BrokenAction", 1f);
    }

    void BrokenAction()
    {
        FindObjectOfType<Movement>().canMove = true;
        hasbroken = false;
        _collider.enabled = false;
        float deathspeedx = 3;
        float deathspeedy = 0;
        for (int i = 0; i < rigidbodies.Length; i++)
        {
            rigidbodies[i].bodyType = RigidbodyType2D.Dynamic;
            if (i >= 2)
                rigidbodies[i].velocity = new Vector2((deathspeedx + 1f) * playerfacing, deathspeedy);
        }
    }
}

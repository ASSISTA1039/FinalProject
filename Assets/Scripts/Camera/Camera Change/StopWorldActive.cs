using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StopWorldActive : MonoBehaviour
{
    public CMChange cMChange;
    private BoxCollider2D _collider;

    public GameObject grapple;
    public GameObject grapple1;

    public Rigidbody2D rd;

    public UnityEvent timerAction;
    public float timer = 4;


    [SerializeField]
    public static bool isEntered;
    [TextArea(1, 3)]
    public string[] lines;

    public GameEvent _event;

    private void Awake()
    {
        if (_event.hasaved)
            Destroy(gameObject);
        if (GrapplingGun.finish_studygrappling)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        if(!GrapplingGun.finish_studygrappling)
        {
            //grapple.SetActive(false);
            //grapple1.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            cMChange.ChangeState();
            rd.velocity = Vector2.zero;
            rd.bodyType = RigidbodyType2D.Kinematic;
            StartCoroutine("TimedEventStart");
            _collider.enabled = false;
        }
    }

    public void TipTalk()
    {
        isEntered = true;
        if (DialogueManager.instance.dialogueBox.activeInHierarchy == false)
        {
            TipManager.instance.ShowDialogue(lines);
            grapple.SetActive(true);
            grapple1.SetActive(true);
            GrapplingGun.finish_studygrappling = true;
            Invoke("StopTip", 1.5f);
        }
    }

    public void StopTip()
    {
        rd.bodyType = RigidbodyType2D.Dynamic;
        isEntered = false;
    }

    IEnumerator TimedEventStart()
    {
        yield return new WaitForSeconds(timer);
        timerAction.Invoke();
    }

    public void StartIEnumerator()
    {
        StartCoroutine("TimedEventStart");
    }

    public void StopIEnumerator()
    {
        StopCoroutine("TimedEventStart");
    }
}

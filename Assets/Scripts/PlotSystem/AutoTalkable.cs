using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTalkable : MonoBehaviour
{
    [Header("-- System Data --")]
    [SerializeField] SaveItem bagsave;


    private bool isEntered;
    private BoxCollider2D _collider;
    public GameObject[] talkers;
    public GameObject volum;
    [TextArea(1, 3)]
    public string[] lines;
    public CMChange cm;
    [SerializeField]
    private bool hasName; // default is false

    public GameEvent _event;

    private void Start()
    {
        if (_event.hasaved)
            Destroy(gameObject);
        _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = true;
        foreach (var talk in talkers)
        {
            talk.SetActive(false);
        }
        if(volum)
            volum.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            _event.hasaved = true;
            bagsave.Save();
            if (volum)
                volum.SetActive(true);
            if (cm)
            {
                CMChange.autotalk_Player = true;
                CMChange.normal_Player = false;
            }
            DialogueManager.instance.BanPlayerMovement();
            foreach (var talk in talkers)
            {
                talk.SetActive(true);
            }
            if (cm)
                cm.ChangeState();
            isEntered = true;
            DialogueManager.talking = true;
            //_collider.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            if (volum)
                volum.SetActive(false);
            isEntered = false;
            
            if (cm)
                cm.ChangeState();
            _collider.enabled = false;
        }
    }

    private void Update()
    {
        if (cm)
        {
            if (CMChange.autotalk_Player && DialogueManager.talking && isEntered && !DialogueManager.instance.dialogueBox.activeInHierarchy)
            {
                DialogueManager.instance.ShowDialogue(lines, hasName);
            }
        }
        else
        {
            if (DialogueManager.talking && isEntered && !DialogueManager.instance.dialogueBox.activeInHierarchy)
            {
                DialogueManager.instance.ShowDialogue(lines, hasName);
            }
        }
/*        if (!DialogueManager.talking && isEntered && !DialogueManager.instance.dialogueBox.activeInHierarchy)
            _collider.enabled = false;*/
        if(_collider.enabled == false)
        {
            foreach (var talk in talkers)
            {
                talk.SetActive(false);
            }
        }
    }
}

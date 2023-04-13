using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Message : MonoBehaviour
{
    [TextArea(1, 3)]
    public string[] lines;

    [SerializeField]
    private bool hasName; // default is false

    /*    [Header("TIMEEVENT")]
        public UnityEvent action;*/

    [Header("Tip")]
    private GameObject investigateTip;

    private bool isentered = false;

    private void Start()
    {
        investigateTip = gameObject.transform.Find("investigate").gameObject;
        investigateTip.SetActive(false);
    }

    private void Update()
    {
        if (isentered && DialogueManager.talking && !DialogueManager.instance.dialogueBox.activeInHierarchy)
        {
            Debug.Log("可以说话");
            DialogueManager.instance.ShowDialogue(lines, hasName);
            isentered = false;

        }
    }

/*    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
*/
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            investigateTip.SetActive(true);
            isentered = true;
        }
        if (Input.GetKey(KeyCode.E))
        {
            DialogueManager.instance.BanPlayerMovement();
            DialogueManager.talking = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        { 
            investigateTip.SetActive(false);
            isentered = false;
        }    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour
{
    [SerializeField]
    public static bool isEntered = true;
    [TextArea(1, 3)]
    public string[] lines;

    [SerializeField]
    private bool hasName; // default is false

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isEntered = false;
        }
    }

    private void Update()
    {
        if(isEntered && Input.GetKeyDown(KeyCode.E) && DialogueManager.instance.dialogueBox.activeInHierarchy == false)
        {
            DialogueManager.instance.ShowDialogue(lines,hasName);
        }
    }
}

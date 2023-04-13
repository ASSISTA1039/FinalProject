using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tip : MonoBehaviour
{
    [SerializeField]
    public static bool isEntered;
    [TextArea(1, 3)]
    public string[] lines;


    [Header("GrapplingHook")]
    public GameObject grapple;
    public GameObject grapple1;

    private void Start()
    {
        isEntered = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            BanPlayerMovement();
            isEntered = true;
            if(DialogueManager.instance.dialogueBox.activeInHierarchy == false)
                TipManager.instance.ShowDialogue(lines);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            isEntered = false;
            Destroy(gameObject);
        }
    }
    public void BanPlayerMovement()
    {
        FindObjectOfType<Movement>().canMove = false;
        FindObjectOfType<Movement>().rb.velocity = Vector2.zero;
        FindObjectOfType<Movement>().anim.SetHorizontalMovement(0, 0, 0);
        grapple.SetActive(false);
        grapple1.SetActive(false);
    }


}

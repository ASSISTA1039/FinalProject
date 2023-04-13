using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipManager : MonoBehaviour
{
    public static TipManager instance;
    public GameObject dialogueBox; // Displayer or hide
    public Text dialogueText;


    [TextArea(1, 3)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;
    public bool isScrolling;
    [SerializeField]
    private float textSpeed;

    [Header("GrapplingHook")]
    public GameObject grapple;
    public GameObject grapple1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        dialogueText.text = dialogueLines[currentLine];
    }

    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0) && dialogueText.text == dialogueLines[currentLine])
            {
                if (isScrolling == false)
                {
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        dialogueBox.SetActive(false); //Box Hiden
                        ActivationPlayerMovement();
                    }
                }
            }
        }
    }

    public void ShowDialogue(string[] newLines)
    {
        dialogueLines = newLines;
        currentLine = 0;
        StartCoroutine(ScrollingText());
        dialogueBox.SetActive(true);
    }

    private IEnumerator ScrollingText()
    {
        isScrolling = true;
        dialogueText.text = "";

        foreach (char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter; // letter by letter show
            yield return new WaitForSeconds(textSpeed);
        }
        isScrolling = false;
    }

    public void ActivationPlayerMovement()
    {
        FindObjectOfType<Movement>().canMove = true;
        grapple.SetActive(true);
        grapple1.SetActive(true);
    }
}

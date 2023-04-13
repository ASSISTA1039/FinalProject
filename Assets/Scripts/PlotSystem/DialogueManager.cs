using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("-- System Data --")]
    [SerializeField] SaveItem bagsave;

    public static DialogueManager instance;
    public static bool talking;
    public GameObject dialogueBox; // Displayer or hide
    public Text dialogueText, nameText;

    [Header("GrapplingHook")]
    public GameObject grapple;
    public GameObject grapple1;

    [TextArea(1, 3)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;
    public bool isScrolling;
    [SerializeField]
    private float textSpeed;
    public GameObject boss_afterdead;
    public GameObject sniper;
    public List<PaySave> paytosave = new List<PaySave>();
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject); 
            }
        }
        talking = false;
        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        dialogueText.text = dialogueLines[currentLine];
    }

    private void Update()
    {
        if(dialogueBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(0) && dialogueText.text == dialogueLines[currentLine])
            {
                if(isScrolling == false)
                {
                    currentLine++;
                    if (currentLine < dialogueLines.Length)
                    {
                        talking = true;
                        CheckName();
                        //dialogueText.text = dialogueLines[currentLine];
                        StartCoroutine(ScrollingText());
                    }
                    else
                    {
                        dialogueBox.SetActive(false); //Box Hiden
                        ActivationPlayerMovement();
                        talking = false;
                        if (boss_afterdead)
                            VideoManager.instance.ShowVideo(true);
                        if(!Talkable.isEntered && sniper)
                        {
                            VideoManager.instance.ShowVideo(true);
                        }
                    }
                }
/*                else
                {
                    CheckName();
                    StartCoroutine(ScrollingText());
                }*/
            }
        }
    }

    public void ShowDialogue(string[] newLines, bool hasname)
    {
        talking = true;

        dialogueLines = newLines;
        currentLine = 0;

        CheckName();

        //dialogueText.text = dialogueLines[currentLine];//line by line show
        StartCoroutine(ScrollingText());
        dialogueBox.SetActive(true);

        nameText.gameObject.SetActive(hasname);
        BanPlayerMovement();

    }

    public void CheckName()
    {
        if(dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");// rermove "n-"
            currentLine++;
        }
    }

    private IEnumerator ScrollingText()
    {
        isScrolling = true;
        dialogueText.text = "";

        foreach(char letter in dialogueLines[currentLine].ToCharArray())
        {
            dialogueText.text += letter; // letter by letter show
            yield return new WaitForSeconds(textSpeed);
        }
        isScrolling = false;
    }

    public void Activation()
    {
        if(CoinUI.CurrentCoinQuantity>=3)
        {
            CoinUI.CurrentCoinQuantity -= 3;
            foreach(var pay in paytosave)
            {
                if (pay.isPaying)
                {
                    pay.isPaying = false;
                    pay.hasaved = true;
                    bagsave.Save();
                    ActivationPlayerMovement();
                }
            }
        }
        else
        {
            ActivationPlayerMovement();
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

    public void ActivationPlayerMovement()
    {
        FindObjectOfType<Movement>().canMove = true;
        grapple.SetActive(true);
        grapple1.SetActive(true);
    }
}

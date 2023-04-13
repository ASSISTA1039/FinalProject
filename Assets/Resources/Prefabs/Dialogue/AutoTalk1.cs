using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTalk1 : MonoBehaviour
{
    [Header("-- System Data --")]
    [SerializeField] SaveItem bagsave;

    public CMChange cm;
    public GameObject cm3;
    private SpriteRenderer sprite;
    [TextArea(1, 3)]
    public string[] lines;
    [SerializeField]
    private bool hasName; // default is false
    public GameObject[] talkers;
    public static bool start_autoTalk;

    private bool startcolorchange = false;
    private bool endcolorchange = false;

    public GameEvent _event;

    private void Start()
    {
        if (_event.hasaved)
            Destroy(gameObject);
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        foreach (var talk in talkers)
        {
            talk.SetActive(false);
        }
    }

    public void Update()
    {
        if (start_autoTalk)
        {
            DialogueManager.instance.BanPlayerMovement();
            _event.hasaved = true;
            bagsave.Save();
            foreach (var talk in talkers)
            {
                talk.SetActive(true);
            }
            if (cm)
                Destroy(cm3);

            sprite.enabled = true;
            DialogueManager.talking = true;
            start_autoTalk = false;
            startcolorchange = true;
        }
        if(startcolorchange)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
        }
/*        if(startcolorchange && CMChange.normal_Player && !DialogueManager.talking && !DialogueManager.instance.dialogueBox.activeInHierarchy)
        {
            cm.ChangeState();
        }*/
        if (cm)
        {
            if (!CMChange.autotalk_Player && !CMChange.normal_Player && DialogueManager.talking && !DialogueManager.instance.dialogueBox.activeInHierarchy)
            {
                DialogueManager.instance.ShowDialogue(lines, hasName);
            }
        }
    }
}

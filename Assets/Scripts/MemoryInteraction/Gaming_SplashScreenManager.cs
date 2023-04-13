using UnityEngine;
public class Gaming_SplashScreenManager : MonoBehaviour
{
    [Header("-- System Data --")]
    [SerializeField] SaveItem bagsave;
    [Header("RESOURCES")]
    public GameObject game_Memory_splashScreen_Start;
    public GameObject game_Memory_splashScreen_Good;
    public GameObject game_Memory_splashScreen_Good1;
    public GameObject game_Memory_splashScreen_Good2;
    public GameObject game_Memory_splashScreen_Good3;
    public GameObject game_Memory_splashScreen_Good4;
    public GameObject game_Memory_splashScreen_Good5;
    public GameObject game_Memory_splashScreen_Good6;
    public GameObject game_Memory_splashScreen_Bad1;
    public GameObject game_Memory_splashScreen_Bad2;
    public GameObject game_Memory_splashScreen_Bad3;
    public GameObject game_Memory_splashScreen_Bad4;
    public GameObject game_Memory_splashScreen_Bad5;
    public GameObject game_Memory_splashScreen_Bad6;
    private Animator game_Memory_splashScreenAnimator_Start;
    private Animator game_Memory_splashScreenAnimator_Good1;
    private Animator game_Memory_splashScreenAnimator_Good2;
    private Animator game_Memory_splashScreenAnimator_Good3;
    private Animator game_Memory_splashScreenAnimator_Good4;
    private Animator game_Memory_splashScreenAnimator_Good5;
    private Animator game_Memory_splashScreenAnimator_Good6;
    private Animator game_Memory_splashScreenAnimator_Bad1;
    private Animator game_Memory_splashScreenAnimator_Bad2;
    private Animator game_Memory_splashScreenAnimator_Bad3;
    private Animator game_Memory_splashScreenAnimator_Bad4;
    private Animator game_Memory_splashScreenAnimator_Bad5;
    private Animator game_Memory_splashScreenAnimator_Bad6;
    private Animator mainPanelsAnimator;
    private Animator homePanelAnimator;

    [Header("SETTINGS")]
    [Header("Good")]
    public bool SplashScreen_Start;
    public static bool SplashScreen_Good;
    public static bool SplashScreen_Good1;
    public static bool SplashScreen_Good2;
    public static bool SplashScreen_Good3;
    public static bool SplashScreen_Good4;
    public static bool SplashScreen_Good5;
    public static bool SplashScreen_Good6;
    [Header("Bad")]
    public static bool SplashScreen_Bad1;
    public static bool SplashScreen_Bad2;
    public static bool SplashScreen_Bad3;
    public static bool SplashScreen_Bad4;
    public static bool SplashScreen_Bad5;
    public static bool SplashScreen_Bad6;


    [Header("Start")]
    public Memory memory;

    private void Awake()
    {
        bagsave.Load();
    }
    void Start()
    {
        SplashScreen_Start = !memory.haveread;
        if (SplashScreen_Start == false)
        {
            game_Memory_splashScreen_Start.SetActive(true);
            game_Memory_splashScreenAnimator_Start = game_Memory_splashScreen_Start.GetComponent<Animator>();
            game_Memory_splashScreenAnimator_Start.Play("Splash Out");
        }

        else
        {
            game_Memory_splashScreen_Start.SetActive(true);
            memory.haveread = true;
            bagsave.Save();
        }
    }

    private void Update()
    {

        if (SplashScreen_Good == true)
        {
            game_Memory_splashScreen_Good.SetActive(true);
            TimedEvent.enableAtStart = true;
            Invoke("ClearBool", 0.1f);
        }

        //GOOD
        if (SplashScreen_Good1 == true)
        {
            game_Memory_splashScreen_Good1.SetActive(true);
            TimedEvent.enableAtStart = true;
            Invoke("ClearBool", 0.1f);
        }
        if (SplashScreen_Good2 == true)
        {
            game_Memory_splashScreen_Good2.SetActive(true);
            TimedEvent.enableAtStart = true;
            Invoke("ClearBool", 0.1f);
        }
        if (SplashScreen_Good3 == true)
        {
            game_Memory_splashScreen_Good3.SetActive(true);
            TimedEvent.enableAtStart = true;
            Invoke("ClearBool", 0.1f);
        }
        if (SplashScreen_Good4 == true)
        {
            game_Memory_splashScreen_Good4.SetActive(true);
            TimedEvent.enableAtStart = true;
            Invoke("ClearBool", 0.1f);
        }
        if (SplashScreen_Good5 == true)
        {
            game_Memory_splashScreen_Good5.SetActive(true);
            TimedEvent.enableAtStart = true;
            Invoke("ClearBool", 0.1f);
        }
        if (SplashScreen_Good6 == true)
        {
            game_Memory_splashScreen_Good6.SetActive(true);
            TimedEvent.enableAtStart = true;
            Invoke("ClearBool", 0.1f);
        }

        //BAD
        if (SplashScreen_Bad1 == true)
        {
            game_Memory_splashScreen_Bad1.SetActive(true);
            TimedEvent.enableAtStart = true;
            Invoke("ClearBool", 0.1f);
        }
        if (SplashScreen_Bad2 == true)
        {
            game_Memory_splashScreen_Bad2.SetActive(true);
            TimedEvent.enableAtStart = true;
            Invoke("ClearBool", 0.1f);
        }
        if (SplashScreen_Bad3 == true)
        {
            game_Memory_splashScreen_Bad3.SetActive(true);
            TimedEvent.enableAtStart = true;
            Invoke("ClearBool", 0.1f);
        }
        if (SplashScreen_Bad4 == true)
        {
            game_Memory_splashScreen_Bad3.SetActive(true);
            TimedEvent.enableAtStart = true;
            Invoke("ClearBool", 0.1f);
        }
        if (SplashScreen_Bad5 == true)
        {
            game_Memory_splashScreen_Bad3.SetActive(true);
            TimedEvent.enableAtStart = true;
            Invoke("ClearBool", 0.1f);
        }
        if (SplashScreen_Bad6 == true)
        {
            game_Memory_splashScreen_Bad3.SetActive(true);
            TimedEvent.enableAtStart = true;
            Invoke("ClearBool", 0.1f);
        }
    }
    public void CheckWhichMemory_Start()
    {
        SplashScreen_Start = true;
    }
    public void CheckWhichMemory_Good1()
    {
        SplashScreen_Good1 = true;
    }
    public void CheckWhichMemory_Good2()
    {
        SplashScreen_Good2 = true;
    }
    public void CheckWhichMemory_Good3()
    {
        SplashScreen_Good3 = true;
    }
    public void CheckWhichMemory_Good4()
    {
        SplashScreen_Good4 = true;
    }
    public void CheckWhichMemory_Good5()
    {
        SplashScreen_Good4 = true;
    }
    public void CheckWhichMemory_Good6()
    {
        SplashScreen_Good4 = true;
    }
    public void CheckWhichMemory_Bad1()
    {
        SplashScreen_Bad1 = true;
    }
    public void CheckWhichMemory_Bad2()
    {
        SplashScreen_Bad2 = true;
    }
    public void CheckWhichMemory_Bad3()
    {
        SplashScreen_Bad3 = true;
    }
    public void CheckWhichMemory_Bad4()
    {
        SplashScreen_Bad3 = true;
    }
    public void CheckWhichMemory_Bad5()
    {
        SplashScreen_Bad3 = true;
    }
    public void CheckWhichMemory_Bad6()
    {
        SplashScreen_Bad3 = true;
    }
    private void ClearBool()
    {
        SplashScreen_Start = false;
        SplashScreen_Good1 = false;
        SplashScreen_Good2 = false;
        SplashScreen_Good3 = false;
        SplashScreen_Good4 = false;
        SplashScreen_Good5 = false;
        SplashScreen_Good6 = false;
        SplashScreen_Bad1 = false;
        SplashScreen_Bad2 = false;
        SplashScreen_Bad3 = false;
        SplashScreen_Bad4 = false;
        SplashScreen_Bad5 = false;
        SplashScreen_Bad6 = false;
    }

    public void ClearTimeEvent()
    {
        TimedEvent.enableAtStart = true;
    }

    public void changeStartMemory()
    {
        memory.haveread = true;
        bagsave.Save();
    }
}

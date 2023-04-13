using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [Header("-- System Data --")]
    [SerializeField] SaveItem bagsave;

    public static VideoManager instance;
    public GameObject video; // Displayer or hide

    [Header("GrapplingHook")]
    public GameObject grapple;
    public GameObject grapple1;
    private bool played = false;
    public Transform boss_afterdead;
    public Transform sniper;
    public GameObject itempref;
    public GameObject player;
    public CMChange cm;

    public GameEvent _event;

    private void Awake()
    {
        if (_event.hasaved)
            Destroy(gameObject);
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
        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        played = false;
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (played && !video.GetComponent<VideoPlayer>().isPlaying)
        {
            _event.hasaved = true;
            bagsave.Save();
            Invoke("AudioCallBack", 2f);
            //StartCoroutine("AudioCallBack",video.GetComponent<VideoPlayer>());
        }
    }

    public void ShowVideo(bool isbossdead)
    {
        video.SetActive(isbossdead);
        played = true;
        video.GetComponent<VideoPlayer>().Play();
        BanPlayerMovement();

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

    private void AudioCallBack()
    {
        if (boss_afterdead && played && !video.GetComponent<VideoPlayer>().isPlaying)
        {
            played = false;
            video.SetActive(false);
            ActivationPlayerMovement();
            Instantiate(itempref, boss_afterdead.position, Quaternion.identity);
            //StartCoroutine("AudioCallBack",video.GetComponent<VideoPlayer>());
        }
        else if(sniper && played && !video.GetComponent<VideoPlayer>().isPlaying)
        {
            if (cm)
                cm.ChangeState();
            played = false;
            video.SetActive(false);
            ActivationPlayerMovement();
            Invoke("Damager", 3f);
        }
    }

    void Damager()
    {
        player.transform.SendMessage("DamagePlayer", 100f);

    }
}

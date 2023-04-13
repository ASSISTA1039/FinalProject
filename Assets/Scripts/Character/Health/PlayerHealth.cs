using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yuliang.UI.Dark;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    [Header("Item_Buff")]
    public Dictionary_Equip items;


    [Header("HealthDetails")]
    public static float health = 5;
    public static float temp_health;
    public float _temp_health;
    public float healthfinal;
    public int Blinks;
    public float time;
    public float dieTime;
    public float hitBoxCdTime;

    [Header("PlayerDetails")]
    public PlayerData playerdata;
    public SpriteRenderer myRender;
    public Animator anim;
    public Rigidbody2D rb2d;
    public BoxCollider2D boxCollider2D;
    public GrapplingRope rope;
    public GrapplingRope1 rope1;

    [Header("HealthCanvas")]
    public ScreenFlash flash;

    //public BossData[] bossDoor;
    // Start is called before the first frame update
    void Start()
    {
        items = GetComponent<Dictionary_Equip>();
        playerdata = GetComponent<PlayerData>();
        if (TransitionPoint.changedScene)
        {
            playerdata.Load_PLayerState();
            temp_health = playerdata.HP;

            _temp_health = health;
            for (int i = 0; i < items.values.Count; i++)
            {
                _temp_health += items.values[i].buff_HP;
            }
            HealthBar.HealthCurrent = temp_health;
            HealthBar.HealthMax = _temp_health;
        }
        else
        {
            temp_health = health;
            //health = PlayerPrefs.GetInt("HP");
            for (int i = 0; i < items.values.Count; i++)
            {
                temp_health += items.values[i].buff_HP;
            }
            //temp_health = playerdata.HP;
            HealthBar.HealthMax = temp_health;
            HealthBar.HealthCurrent = temp_health;
        }
        //anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        flash = GetComponent<ScreenFlash>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        healthfinal = temp_health;
        HealthBar.HealthCurrent = temp_health;
    }


    public void DamagePlayer(float damage)
    {
        flash.FlashScreen();
        temp_health -= damage;
        PlayerPrefs.SetFloat("HP", temp_health);
        //Debug.Log(health);
        if (temp_health <= 0)
        {
            temp_health = 0;
        }
        HealthBar.HealthCurrent = temp_health;
        if (temp_health <= 0)
        {
            rb2d.velocity = new Vector2(0, 0);
            //rb2d.gravityScale = 0.0f;
            GameController.isGameAlive = false;
            //anim.SetTrigger("Die");
            //Invoke("KillPlayer", dieTime);
            KillPlayer();
        }
        BlinkPlayer(Blinks, time);
        boxCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
        GameController.isGameAlive = true;
    }

    public void SetHealth(float amount)
    {
        HealthBar.HealthCurrent = amount;
        temp_health = amount;

        if (temp_health <= 0)
        {
            rb2d.velocity = new Vector2(0, 0);
            //rb2d.gravityScale = 0.0f;
            GameController.isGameAlive = false;
            //anim.SetTrigger("Die");
            //Invoke("KillPlayer", dieTime);
            KillPlayer();
        }

        BlinkPlayer(Blinks, time);
        boxCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
        GameController.isGameAlive = true;
    }

    public void KillPlayer()
    {
        Scene s = SceneManager.GetActiveScene();
        rope.enabled = false;
        rope1.enabled = false;
        playerdata.Load();
        if (playerdata.Level == s.buildIndex && s.buildIndex != 6)
        {
            rb2d.position = playerdata.Position;
        }
        else if (s.buildIndex == 6 || playerdata.Level != s.buildIndex)
        {
            rb2d.position = playerdata.Position;
            SceneManager.LoadScene(playerdata.Level);
        }
        anim.Play("Idle");
        //temp_health = 5;
        updateHPState();
        HealthBar.HealthCurrent = HealthBar.HealthMax;
/*        for(int i =0;i<bossDoor.Length;i++)
        {
            bossDoor[i].Load();
        }*/
    }
    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(1f);
        boxCollider2D.enabled = true;
    }
    void BlinkPlayer(int numBlinks,float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    IEnumerator DoBlinks(int  numBlinks,float seconds)
    {
        for(int i =0;i<numBlinks*2;i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }


    void updateHPState()
    {
        if (TransitionPoint.changedScene)
        {
            playerdata.Load_PLayerState();
            temp_health = playerdata.HP;

            _temp_health = health;
            for (int i = 0; i < items.values.Count; i++)
            {
                _temp_health += items.values[i].buff_HP;
            }
            HealthBar.HealthCurrent = temp_health;
            HealthBar.HealthMax = _temp_health;
        }
        else
        {
            temp_health = health;
            //health = PlayerPrefs.GetInt("HP");
            for (int i = 0; i < items.values.Count; i++)
            {
                temp_health += items.values[i].buff_HP;
            }
            //temp_health = playerdata.HP;
            HealthBar.HealthMax = temp_health;
            HealthBar.HealthCurrent = temp_health;
        }
    }
}

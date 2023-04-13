using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float damge;
    private PlayerHealth playerHealth;
    public float durationOfInjury;
    //持续受伤计时器
    private float durationOfInjuryer;
    private bool isDamageDurationOf = true;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    /*    void Update()
        {
            frezeTime -= Time.deltaTime;
            if (frezeTime < 0)
            {
                cantrigger = true;
                frezeTime = 2f;
            }
        }
        */
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
                other.gameObject.transform.SendMessage("DamagePlayer", damge);
        }
    }
    private void Update()
    {
        //Debug.Log(durationOfInjuryer);
        
    }
    /*private void OnTriggerStay2D(Collider2D other)
    {
        //判断是不是玩家
        //Debug.Log(isDamageDurationOf);
        if (other.CompareTag("Player") && other.GetType().ToString().Equals("UnityEngine.BoxCollider2D"))
        {
            Debug.Log(isDamageDurationOf);
            //是不是可以伤害
            if (isDamageDurationOf)
            {
                isDamageDurationOf = false;
                other.GetComponent<PlayerHealth>().DamagePlayer(damge); ;
            }
            else
            {
                //计时  
                durationOfInjuryer += Time.deltaTime;
                if (durationOfInjuryer >= durationOfInjury)
                {
                    isDamageDurationOf = true;
                    durationOfInjuryer = 0f;
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //reset
        if (other.CompareTag("Player") && other.GetType().ToString().Equals("UnityEngine.BoxCollider2D"))
        {
            isDamageDurationOf = true;
            durationOfInjuryer = 0f;
        }
    }*/
}

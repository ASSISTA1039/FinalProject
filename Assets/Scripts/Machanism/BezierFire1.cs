using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFire1 : MonoBehaviour
{
    [Header("FireDetails")]
    public float radius = 10;
    public float dropNum = 5;
    private bool canfire = true;
    private float frezeTime = 1f;
    private float startTime;


    [Header("Player")]
    public Transform player;


    [Header("LaunchThing")]
    public Coin coinPref;




    private void Start()
    {
        player = GameObject.Find("CorePos").GetComponent<Transform>();
        startTime = Time.time + frezeTime;
        Fire();
    }


    public Vector3 GetRandomPoint(float radius)
    {
        return transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
    }

    void Fire()
    {
        for(int i =0;i< dropNum;i++)
        {
            Coin coin = GameObject.Instantiate(coinPref, transform.position, Quaternion.identity);
            StartCoroutine(coin.Move(coin.transform.position, GetRandomPoint(radius), player));
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            Destroy(gameObject);
            Invoke("StopAllCoroutines",1f);

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFire: MonoBehaviour
{
    [Header("FireDetails")]
    public float radius = 10;
    private bool canfire = true;
    private float frezeTime = 1f;
    private float startTime;


    [Header("Player")]
    public Transform player;


    [Header("LaunchThing")]
    public Coin coinPref;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody2D>().transform;
        startTime = Time.time + frezeTime;
        StartFire();
    }

    private void Update()
    {
        if (startTime < Time.time)
        {
            canfire = false;
            StopAllCoroutines();
        }
    }
    public void StartFire()
    {
        StartCoroutine(FireToPlayer());
    }
    public void StopFire()
    {
        StopAllCoroutines();
    }

    public Vector3 GetRandomPoint(float radius)
    {
        return transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
    }

    IEnumerator FireToPlayer()
    {
        while (canfire)
        {
            Coin coin = GameObject.Instantiate(coinPref, transform.position, Quaternion.identity);
            StartCoroutine(coin.Move(coin.transform.position, GetRandomPoint(radius), player));
            yield return new WaitForSeconds(0.05f);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CoinUI.CurrentCoinQuantity += 1;
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

}

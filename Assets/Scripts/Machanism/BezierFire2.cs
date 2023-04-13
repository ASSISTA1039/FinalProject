using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFire2 : MonoBehaviour
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
    public Energy_ShakingLight coinPref;


    private void Start()
    {
        player = GameObject.Find("CorePos").GetComponent<Transform>();
        startTime = Time.time + frezeTime;
        Fire();
    }


    public Vector3 GetRandomPoint(float radius)
    {
        return transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), Random.Range(-radius, radius));
    }

    void Fire()
    {
        for(int i =0;i< dropNum;i++)
        {
            Energy_ShakingLight energy = GameObject.Instantiate(coinPref, transform.position, Quaternion.identity);
            StartCoroutine(energy.Move(energy.transform.position, GetRandomPoint(radius), player));
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();

            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed = 5;

    private float frezeTime = 6f;
    private float startTime;

    public void Start()
    {
        startTime = Time.time + frezeTime;
    }
    private void Update()
    {
        if (startTime < Time.time)
        {
            CoinUI.CurrentCoinQuantity += 1;

            Destroy(gameObject);
            Invoke("StopAllCoroutines", 1f);


        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public IEnumerator Move(Vector3 start, Vector3 midPoint, Transform target)
    {
        for (float i = 0; i <=1; i+= 2*Time.deltaTime)
        {
            Vector3 p1 = Vector3.Lerp(start, midPoint, i);
            Vector3 p2 = Vector3.Lerp(midPoint, target.position, i);
            Vector3 p = Vector3.Lerp(p1, p2, i);
            // Make the bullet move to point p
            yield return StartCoroutine(MoveToPoint(p));
        }
        yield return StartCoroutine(MoveToObject(target));
    }

    IEnumerator MoveToPoint(Vector3 p)
    {
        yield return null;
        while (Vector3.Distance(transform.position, p)>0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, p, Time.deltaTime * speed);
            yield return null;
        }
    }

    IEnumerator MoveToObject(Transform target)
    {
        yield return null;
        while (Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            CoinUI.CurrentCoinQuantity += 1;
            

            Destroy(gameObject);
            Invoke("StopAllCoroutines", 1f);

        }
    }

}

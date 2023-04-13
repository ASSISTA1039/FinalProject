using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{
    private float frezeTime = 20f;
    private float startTime;
    private void Start()
    {
        startTime = Time.time + frezeTime;
    }

    private void Update()
    {
        if (startTime < Time.time)
            Destroy(gameObject);
    }
}


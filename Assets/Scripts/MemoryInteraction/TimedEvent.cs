using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedEvent : MonoBehaviour
{
    [Header("TIMING (SECONDS)")]
    public float timer = 4;
    public static bool enableAtStart = false;
    private bool firstTime;
    [Header("TIMER EVENT")]
    public UnityEvent timerAction;

    void OnEnable()
    { 
            StartCoroutine("TimedEventStart");
    }

    IEnumerator TimedEventStart()
    {
        enableAtStart = false;
        yield return new WaitForSeconds(timer);
        timerAction.Invoke();
    }

    public void StartIEnumerator ()
    {
        StartCoroutine("TimedEventStart");
    }

    public void StopIEnumerator ()
    {
        StopCoroutine("TimedEventStart");
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yuliang.UI.Dark;
using UnityEngine.Events;

public class MemoryInteract : MonoBehaviour
{
    public bool canMemory;

    [Header("TIMER EVENT")]
    public UnityEvent timerAction;

    private int i;

    private void Update()
    {
        if(PointToSave.hasaved)
        {
            timerAction.Invoke();
            PointToSave.hasaved = false;
        }
    }


    IEnumerator TimedEventStart()
    {
        yield return new WaitForSeconds(0.1f);
        timerAction.Invoke();
    }
}
*/
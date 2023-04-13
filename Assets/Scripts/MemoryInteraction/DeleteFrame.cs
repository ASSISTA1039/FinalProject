using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeleteFrame : MonoBehaviour
{
    public UnityEvent action;
    
    public void ListenOnClick()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            action.Invoke();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAction_Tip : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            gameObject.SetActive(false);
        }
    }
}

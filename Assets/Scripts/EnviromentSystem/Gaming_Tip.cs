using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaming_Tip : MonoBehaviour
{
    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = gameObject.GetComponentInChildren<BoxCollider2D>();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform backDoor;

    public bool isClosedwithDoor;
    private Transform playerTransform;
    public GrapplingGun gun;
    public GrapplingGun1 gun1;
    public GrapplingRope rope;
    public GrapplingRope1 rope1;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rope.enabled && isClosedwithDoor)
        {
            gun.enabled = false;
            gun1.enabled = false;
            rope.enabled = false;
            rope1.enabled = false;
            Invoke("EnterDoor",0.3f);
            isClosedwithDoor = false;
        }
    }

    void EnterDoor()
    {
        playerTransform.position = backDoor.position;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            isClosedwithDoor = true;
            Debug.Log("进入传送门");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
            && other.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            isClosedwithDoor = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed1;
    public float speed2;
    public float waitTime;
    public Transform[] movePos;

    private int i;
    private Transform playerDefTransform;

    public GrapplingRope rope;
    public GrapplingRope1 rope1;
    //public Movement player;

    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        playerDefTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
        Debug.Log(playerDefTransform);
    }

    // Update is called once per frame
    void Update()
    {
        if (i == 0)
            transform.position = Vector2.MoveTowards(transform.position, movePos[0].position, speed1 * Time.deltaTime);
        else if (i == 1)
            transform.position = Vector2.MoveTowards(transform.position, movePos[1].position, speed2 * Time.deltaTime);
        if (Vector2.Distance(transform.position, movePos[i].position) < 0.1f)
        {
            if (waitTime < 0.0f)
            {
                if (i == 0)
                {
                    i = 1;
                }
                else
                {
                    i = 0;
                }

                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        /*        if (player.isOnGround == false && (rope.enabled == true || rope1.enabled == true))
                {
                    player.gameObject.transform.parent = gameObject.transform;
                }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") )
        {
/*            if (!Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.LeftControl))
*/          other.gameObject.transform.parent = gameObject.transform;
        }
    }
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.CompareTag("player") && other.GetType().ToString() == "UnityEngine.BoxCollider2D")
    //    {
    //        if (!Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.LeftControl))
    //            other.gameObject.transform.parent = gameObject.transform;
    //    }
    //}
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //if (!Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse1) && !Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1) && !Input.GetKey(KeyCode.LeftControl))
            other.gameObject.transform.parent = playerDefTransform;
        }
    }
}

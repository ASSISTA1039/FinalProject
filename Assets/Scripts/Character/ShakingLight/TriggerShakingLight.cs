using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShakingLight : MonoBehaviour
{
    private CapsuleCollider2D collider2D;

    [Header("PlayerBody")]
    public GameObject playerBody;

    [Header("ShakingLight")]
    public GameObject shakingLight;

    public GrapplingRope rope;
    public GrapplingRope1 rope1;

    private Color normal;
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<CapsuleCollider2D>();
        normal = shakingLight.GetComponent<SpriteRenderer>().color;

    }

    // Update is called once per frame
    void Update()
    {
        if (GrapplingGun.finish_studygrappling)
        {
            if (((Input.GetKey(KeyCode.Mouse0) && (!Input.GetKey(KeyCode.LeftControl)))
       || (Input.GetKey(KeyCode.Mouse1) && Input.GetKey(KeyCode.LeftControl))
       || (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.LeftControl) && !GrapplingGun1.canDoubleGrappling)) && !DialogueManager.talking)
            {
                shakingLight.GetComponent<SpriteRenderer>().color = normal;

                shakingLight.SetActive(true);
                playerBody.SetActive(false);
                collider2D.enabled = false;
            }
            else
            {
                shakingLight.SetActive(false);
                playerBody.SetActive(true);
                collider2D.enabled = true;
            }
            if (rope.enabled == false && rope1.enabled == false && Input.GetKey(KeyCode.Mouse0) && (!Input.GetKey(KeyCode.LeftControl)) && !DialogueManager.talking)
            {
                Debug.Log(DialogueManager.talking);
                shakingLight.GetComponent<SpriteRenderer>().color = new Color(205f, 172f, 162f, 1f);
                /*            shakingLight.SetActive(false);
                            playerBody.SetActive(true);
                            collider2D.enabled = true;*/
            }
        }
    }
}

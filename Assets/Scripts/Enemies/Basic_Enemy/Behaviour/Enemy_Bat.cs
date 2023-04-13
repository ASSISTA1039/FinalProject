using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bat : Enemy
{
    public float speed;
    public float radius;

    private Transform playerTransform;
    private Vector3 batTansform;

    public EnemyAI ai;

    public bool isFollowPlayer;

    // Start is called before the first frame update

    private void Awake()
    {
        batTansform = transform.position;
    }
    public new void Start()
    {
        base.Start();
        isFollowPlayer = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ai = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    public new void Update()
    {
        base.Update();

        /*if(isFollowPlayer == false)
        {
            return;
        }*/

/*        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;

            if (distance <= radius)
            {
                ai.enabled = true;
                transform.position = Vector2.MoveTowards(transform.position,
                                                        new Vector3(playerTransform.position.x, playerTransform.position.y + 2, playerTransform.position.z),
                                                        speed * Time.deltaTime);
            }
            else
            {
                ai.enabled = false;
                transform.position = Vector2.MoveTowards(transform.position,
                                                        new Vector3(batTansform.x, batTansform.y, batTansform.z),
                                                        speed * Time.deltaTime);
            }
        }*/
    }
}

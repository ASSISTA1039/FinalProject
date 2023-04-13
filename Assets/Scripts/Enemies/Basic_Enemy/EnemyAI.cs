using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("TargetInfomation")]
    public Transform _player;
    public float speed = 200f;
    public float turnSpeed = 3;
    public float turnDst = 5;
    public float stoppingDst = 10;

    [Header("PathFinding")]
    bool findwalk;
    public Enemy_PathFinding findinmap;
    List<Node> path;
    int currentIndex;
    bool reachedEndOfPath = false;
    public float nextWaypointDistance = 3f;
    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    [Header("EnemyInformation")]
    Rigidbody2D rb;
    public float attackRange = 40f;
    Vector2 pos;
    Vector2 home;
    bool firststart = true;


    private void OnEnable()
    {
        //PathManager.RequestGetPath(transform.position, target.position, PathFound);
        rb = GetComponent<Rigidbody2D>();
        _player = GameObject.Find("Player").GetComponent<Transform>();
        currentIndex = 0;
        pos = rb.position;
        home = rb.position;
    }

    /*    public void PathFound(Vector3[] newPath, bool pathSuccessful)
        {
            if (pathSuccessful)
            {
                //path = new ASPath(newPath, transform.position, turnDst, stoppingDst);
                path = newPath;
                currentIndex = 0;
                if (path!=null)
                {
                    StopCoroutine("FollowPath");
                    StartCoroutine("FollowPath");
                }
            }
        }*/

    /*    IEnumerator UpdatePath()
        {

            if (Time.timeSinceLevelLoad < .3f)
            {
                yield return new WaitForSeconds(.3f);
            }
            //PathManager.RequestGetPath(transform.position, target.position, PathFound);

            float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
            Vector3 targetPosOld = target.position;

            while (true)
            {
                yield return new WaitForSeconds(minPathUpdateTime);
                if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
                {
                    //PathManager.RequestGetPath(transform.position, target.position, PathFound);
                    targetPosOld = target.position;
                }
            }
        }*/


    private void FixedUpdate()
    {
        if (Vector2.Distance(new Vector2(_player.position.x,_player.position.y+1), rb.position) < attackRange)
            findwalk = true;
        else
            findwalk = false;
        if(findwalk)
        {
            findinmap.FindingPath(transform.position, new Vector2(_player.position.x, _player.position.y + 1));
            path = findinmap.path;

            // && (Vector2)transform.position != home
            if (path.Count>0 && transform.position !=path[0]._worldPos)
            {
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }
        else
        {
            findinmap.FindingPath(transform.position, home);
            path = findinmap.path;
            if (path.Count > 0 && transform.position != path[0]._worldPos)
            {
                StopCoroutine("FollowPath");
                StartCoroutine("FollowPath");
            }
        }    
    }



    IEnumerator FollowPath()
    {
        currentIndex = 0;
        Vector3 currentWaypoint = path[0]._worldPos;
        while (true)
        {
            if (path.Count > 0 && transform.position != path[0]._worldPos)
            {
                if (transform.position == currentWaypoint)
                {
                    currentIndex++;
                    if (currentIndex >= path.Count)
                    {
                        yield break;
                    }
                    currentWaypoint = path[0]._worldPos;
                }
                rb.position = Vector3.MoveTowards(rb.position, path[0]._worldPos, speed * Time.deltaTime);
            }
            yield return null;
        }
    }
}

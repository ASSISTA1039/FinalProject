using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAroundPlayer : MonoBehaviour
{
    private Transform _transform;
    public float _distancefromcore;
    public Transform _topPoint;
    public Transform _downPoint;
    public Transform _tempPoint;
    public Animator anim;
    private int onground;
    private int dash;
    private void Start()
    {
        _transform = gameObject.transform;
        onground = Animator.StringToHash("onGround");
        dash = Animator.StringToHash("isDashing");
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.S) && anim.GetBool(onground) && !anim.GetBool(dash))
        {
            //Debug.Log("Move down");
            if(Mathf.Abs(transform.localPosition.y - _tempPoint.localPosition.y) < _distancefromcore)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, 
                                                new Vector3(_transform.localPosition.x,
                                                            _transform.localPosition.y - _distancefromcore,
                                                            _transform.localPosition.z), Time.deltaTime * 3f);


/*                _transform.position = new Vector3(_transform.position.x, 
                                                    _transform.position.y- 1f * Time.deltaTime,
                                                    _transform.position.z);*/
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Debug.Log(_transform.localPosition.y);
            if (_transform.localPosition.y < 1.4f)
            {
                /*transform.localPosition = Vector3.MoveTowards(transform.localPosition,
                                                new Vector3(_transform.localPosition.x,
                                                            _transform.localPosition.y + _distancefromcore,
                                                            _transform.localPosition.z), Time.deltaTime * 3f);*/

                _transform.localPosition = _tempPoint.localPosition;
            }
        }


        if (Input.GetKey(KeyCode.W) && anim.GetBool(onground) && !anim.GetBool(dash))
        {
            if (Mathf.Abs(transform.localPosition.y - _tempPoint.localPosition.y) < _distancefromcore)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition,
                                                new Vector3(_transform.localPosition.x,
                                                            _transform.localPosition.y + _distancefromcore,
                                                            _transform.localPosition.z), Time.deltaTime * 3f);
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            //Debug.Log(_transform.position.y);
            if (_transform.localPosition.y > 1.4f)
            {
                _transform.localPosition = _tempPoint.localPosition;
            }
        }
    }
}


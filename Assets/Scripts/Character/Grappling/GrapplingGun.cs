using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrapplingGun : MonoBehaviour
{
    [Header("ComponentsScripts")]
    public GrapplingRope rope;
    public GrapplingRope1 rope1;
    public GrapplingGun grapplingGun;
    public GrapplingGun1 grapplingGun1;

    [Header("Ceiling")]
    public static bool ON_TouchCeiling;
    public Ceiling ceiling;

    [Header("Controller")]
    public Rigidbody2D controller;
    public float m_gravity;
    public float force;


    [Header("Layers&&Laser Settings")]
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask laserLayer;
    [SerializeField] public LineRenderer laser;

    [Header("Main Camera")]
    public Camera m_camera;

    [Header("Transform")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;
    public Transform firePoint1;


    [Header("RopeRotation")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("LaunchDistance")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;


    [Header("ShakingLight")]
    public GameObject shakingLight;


    [Header("CheckSpecificSituation")]
    public bool check = true;
    public bool checkIsAir = true;
    public bool checkIsGrappling = true;
    private float grapplingTime;
    public bool canTouchFall = false;
    [SerializeField] private Vector3 cameraPositionNow;

    [Header("L0EventFinish")]
    public static bool finish_studygrappling = false;
    public GameEvent startevent;


    [Header("LaunchingDetails")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private float launchSpeed = 5.7f;
    [SerializeField] private float launchSpeed1 = 10f;
    [SerializeField] private float peak = 5.7f;
    [SerializeField] private float refresh = 5.7f;
    [SerializeField] public bool checkMovementDir = true;
    [SerializeField] private float diruaction = 5f;
    [SerializeField] private float airTime;


    [Header("FindTargetPoint")]
    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    [HideInInspector] public Vector2 distanceVector = new Vector2(0, 0);
    [HideInInspector] public Vector2 finalV;


    //private LineRenderer lineRenderer;
    [Header("Detection_Thing")]
    public bool touchJiguan;
    public bool touchGuandao;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            finish_studygrappling = false;
        else
            finish_studygrappling = true;
        rope.enabled = false;
        //lineRenderer = GetComponent<LineRenderer>();
        //shakingLight = GameObject.FindGameObjectWithTag("Attack");
        m_camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        try
        {
            laser = GameObject.Find("Laser").GetComponent<LineRenderer>();
        }
        catch (NullReferenceException)
        {
            Debug.Log("didnt find laser object");
        }
        shakingLight.SetActive(false);
        ON_TouchCeiling = false;
    }

    private void Update()
    {
        if (startevent.hasaved || finish_studygrappling)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SetGrapplePoint();

                if (!canTouchFall)
                {
                    rope.enabled = false;
                    rope1.enabled = false;
                    controller.gravityScale = m_gravity;
                    controller.velocity = Vector2.zero;
                    checkIsGrappling = true;
                    return;
                }
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                if (!canTouchFall)
                {
                    rope.enabled = false;
                    rope1.enabled = false;
                    controller.gravityScale = m_gravity;
                    controller.velocity = Vector2.down;
                    checkIsGrappling = true;
                    return;
                }
                if (rope.enabled)
                {
                    //GetComponent<AudioSource>().Play();
                    RotateGun(grapplePoint, false);
                }
                else
                {
                    Vector2 mousePos = m_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gunHolder.position.z - m_camera.transform.position.z));
                    RotateGun(mousePos, true);
                }
                if (launchToPoint && rope.isGrappling)
                {
                    grapplingMovement();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                check = true;
                checkIsAir = true;
                rope.enabled = false;
                controller.gravityScale = m_gravity;
                canTouchFall = false;
                checkIsGrappling = true;
                checkMovementDir = true;
                shakingLight.SetActive(false);
                if (laser)
                    laser.enabled = false;
                for (int i = 0; i < 10; i++)
                {
                    if (controller.velocity.x < 0.5f && controller.velocity.y < 0.5f)
                    {
                        controller.velocity = Vector2.zero;
                        return;
                    }
                    controller.velocity = new Vector2(controller.velocity.x / 10, controller.velocity.y / 10);
                }
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gunHolder.position.z - m_camera.transform.position.z));
                RotateGun(mousePos, true);
            }
        }
    }


    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }


    void SetGrapplePoint()
    {
        //gunHolder => Character transform positon
        //Perspective camera: converts screen 2D coordinates to world 3D coordinates,
        //Focus on the z-axis
        cameraPositionNow = m_camera.ScreenToWorldPoint
            (new Vector3(Input.mousePosition.x,
                         Input.mousePosition.y,
                         gunHolder.position.z - m_camera.transform.position.z));

        distanceVector = cameraPositionNow - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            //Detects whether the collision bodies in this ray direction satisfy the conditions
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            RaycastHit2D[] _hits = Physics2D.RaycastAll(firePoint.position, distanceVector.normalized, maxDistnace, groundLayer);
            if (laser)
            {
                if (_hit.transform.gameObject.tag == "laser")
                {
                    laser.enabled = true;
                }
                else
                {
                    laser.enabled = false;
                }
            }
            if (_hit.transform.gameObject.layer == 4)
            {
                rope.enabled = false;
                canTouchFall = false;
                return;
            }
            if (ON_TouchCeiling)
                foreach (var hit in _hits)
                {
                    if (hit.transform.gameObject.tag == "ceiling")
                    {
                        touchGuandao = true;
                        //Debug.Log("开始转动");
                        hit.transform.gameObject.tag = "Untagged";
                        Invoke("OpenGuanDao", 1f);
                    }
                }
            if (_hit.transform.gameObject.tag == "jiguan")
            {
                touchJiguan = true;
                Debug.Log("jiguan");
            }
            for (int i = 0; i < _hits.Length; i++)
            {
                if (Vector2.Distance(_hits[i].point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    //Set the first one that meets all the conditions as the target point
                    grapplePoint = _hits[i].point;
                    rope.enabled = true;
                    canTouchFall = true;
                    if (_hits[i].transform.gameObject.tag == "movingPlatform")
                    {
                        Invoke("RemoveFromMovingPlatform", 0.5f);
                    }
                    GetComponent<AudioSource>().Play();
                    return;
                }
            }
        }

    }

    void RemoveFromMovingPlatform()
    {
        rope.enabled = false;
    }


    void checkCanGrappling(bool touchWall)
    {
        if (!touchWall)
        {
            rope.enabled = false;
            //rope1.enabled = false;
            controller.gravityScale = m_gravity;
            controller.velocity = Vector2.zero;
            checkIsGrappling = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }

    void grapplingMovement()
    {
        Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
        Vector2 targetDistance = grapplePoint - (Vector2)gunHolder.position;

        checkIsAir = false;
        //Enter Double mode and prepare to aim for the second grapllingGun.
        if (Input.GetKey(KeyCode.LeftControl) && GrapplingGun1.canDoubleGrappling)
        {
            controller.gravityScale = m_gravity;
            //Double mode launch!
            if (check && Input.GetKey(KeyCode.Mouse1))
            {
                shakingLight.SetActive(true);
                check = false;
                controller.gravityScale = 0;
                Vector2 fnlvector0 = grapplePoint - (Vector2)gunHolder.position;
                Vector2 fnlvector1 = grapplingGun1.grapplePoint - (Vector2)gunHolder.position;
                //Recording the sum speed in Double mode
                finalV = (fnlvector0 + fnlvector1);
                //Record the longest distance of grappling displacement
                float m_distance = finalV.magnitude;
                if (m_distance > 10)
                    m_distance = 10f;
                if (checkIsGrappling)
                {
                    if (finalV.magnitude > 1.5 * launchSpeed)
                        controller.velocity = new Vector2(peak * finalV.x, peak * finalV.y);
                    else
                    {
                        controller.velocity = new Vector2(refresh * finalV.x, refresh * finalV.y);
                    }
                    //Record the longest duration of grappling
                    grapplingTime = 1.3f * m_distance / controller.velocity.magnitude;
                    grapplingTime = Time.time + grapplingTime;
                    checkIsGrappling = false;
                    airTime = Time.time + diruaction;

                }

                //firePoint1 = grapplingGun1.firePoint;
            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (finalV.magnitude > 1.5 * launchSpeed)
                    controller.velocity = new Vector2(peak * finalV.x, peak * finalV.y);
                else
                    controller.velocity = new Vector2(refresh * finalV.x, refresh * finalV.y);
            }
        }
        else if (!Input.GetKey(KeyCode.Mouse1))
        {
            shakingLight.SetActive(true);
            if (checkMovementDir)
            {
                checkMovementDir = false;
            }

            finalV = grapplePoint - new Vector2(gunHolder.position.x, gunHolder.position.y);

            controller.velocity = new Vector2(finalV.x * launchSpeed1, finalV.y * launchSpeed1);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            PlayerEnegy.temp_energy -= 0.007f;
            if (PlayerEnegy.temp_energy <= 1)
            {
                shakingLight.SetActive(false);
                rope.enabled = false;
                rope1.enabled = false;
                controller.gravityScale = m_gravity;
            }
        }
        Debug.DrawRay(gunHolder.position, controller.velocity, Color.yellow);
        if (!checkIsGrappling)
        {
            if (grapplingTime < Time.time)
            {
                controller.velocity = grapplingInAir();
                shakingLight.SetActive(false);
                rope.enabled = false;
                rope1.enabled = false;
                controller.gravityScale = m_gravity;
            }
        }
    }

    Vector2 grapplingInAir()
    {
        if (airTime < Time.time)
            return Vector2.zero;
        else
            return new Vector2(controller.velocity.x / 5, controller.velocity.y / 5);
    }

    void OpenGuanDao()
    {
        if (rope.openGuandao)
        {
            //Debug.Log("开始转动");
            ceiling.OpenTheCeiling();
            touchGuandao = false;
            rope.openGuandao = false;
            rope.enabled = false;
        }
    }
}
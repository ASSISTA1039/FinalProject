using UnityEngine;
using Yuliang.UI.Dark;

public class GrapplingGun1 : MonoBehaviour
{
    [Header("ComponentsScripts")]
    public GrapplingRope1 rope1;
    public GrapplingRope rope;
    public GrapplingGun grapplingGun;
    public GrapplingGun1 grapplingGun1;

    [Header("Controller")]
    public Rigidbody2D controller;
    public float force;
    public float m_gravity;

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


    [Header("RopeRotation")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("LaunchDistance")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    [Header("CheckSpecificSituation")]
    private bool check = true;
    private bool checkIsAir = true;
    public bool canTouchFall = false;
    [SerializeField] private Vector3 cameraPositionNow;


    [Header("LaunchingDetails")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private float launchSpeed = 1;

    [Header("FindTargetPoint")]
    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    [HideInInspector] public Vector2 distanceVector = new Vector2(0, 0);
    [HideInInspector] public Vector2 finalV;

    private LineRenderer lineRenderer;

    [Header("Equip_Skill_Study")]
    public Dictionary_Inventory items;
    public static bool canDoubleGrappling = false;

    private void Start()
    {
        items = transform.parent.parent.gameObject.GetComponent<Dictionary_Inventory>();
        rope1.enabled = false;
        lineRenderer = GetComponent<LineRenderer>();
        //attackWeapon = GameObject.FindGameObjectWithTag("attack");
        //attackWeapon.SetActive(false);
        //更换装备中
        for (int i = 0; i < items.values.Count; i++)
        {
            if (items.values[i].doubleGrappling)
            {
                canDoubleGrappling = items.values[i].doubleGrappling;
            }
        }

    }

    private void Update()
    {
        if (canDoubleGrappling)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                SetGrapplePoint();
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {

                    if (!canTouchFall)
                    {
                        //Debug.Log(canTouchFall);
                        rope.enabled = false;
                        rope1.enabled = false;
                        controller.gravityScale = 3;
                        controller.velocity = Vector2.zero;
                        return;
                    }
                }
                else
                {
                    Vector2 mousePos = m_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gunHolder.position.z-m_camera.transform.position.z));
                    RotateGun(mousePos, true);
                }
            }
            else
                rope1.enabled = false;
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
        cameraPositionNow = m_camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gunHolder.position.z - m_camera.transform.position.z));
        distanceVector = cameraPositionNow - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            
            //RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            RaycastHit2D[] _hits = Physics2D.RaycastAll(firePoint.position, distanceVector.normalized, maxDistnace, groundLayer);
            for (int i = 0; i < _hits.Length; i++)
            {
                if (Vector2.Distance(_hits[i].point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    grapplePoint = _hits[i].point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    if (Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.LeftControl) && rope.enabled == true)
                    {
                        rope1.enabled = true;
                    }
                    if (!(Input.GetKey(KeyCode.Mouse0) && Input.GetKey(KeyCode.LeftControl)) || rope.enabled == false)
                    {
                        rope1.enabled = false;
                    }
                    canTouchFall = true;


                    if (_hits[i].transform.gameObject.tag == "movingPlatform")
                    {
                        Invoke("RemoveFromMovingPlatform", 0.5f);
                    }
                    return;
                }
            }
        }
    }

    void RemoveFromMovingPlatform()
    {

        rope1.enabled = false;
        //m_distanceJoint2D.enabled = false;
        if (rope.enabled == false)
        {
            controller.gravityScale = m_gravity;
            //controller.velocity = Vector2.zero;
            check = true;
            checkIsAir = true;
            canTouchFall = false;
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

}
using UnityEngine;

public class GrapplingRope1 : MonoBehaviour
{
    [Header("GrapplingInfo")]
    public GrapplingGun1 grapplingGun;
    public LineRenderer m_lineRenderer;

    [Header("GrapplingAttributes")]
    [SerializeField] private int percision = 50;
    [Range(0, 20)] [SerializeField] private float straightenLineSpeed = 5.5f;

    [Header("GrapplingRope Released")]
    public AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)] [SerializeField] private float StartWaveSize = 3;
    float waveSize = 0;

    [Header("GrapplingRope Recycling")]
    public AnimationCurve ropeProgressionCurve;
    [SerializeField] [Range(1, 50)] private float ropeProgressionSpeed = 1.25f;

    float moveTime = 0;

    [SerializeField] public bool isGrappling = false;

    public bool strightLine = true;
    [SerializeField] public float delta;
    [SerializeField] public Vector2 offset;
    [SerializeField] public Vector2 targetPosition = new Vector2(0,0);
    [SerializeField] public Vector2 currentPosition = new Vector2(0, 0);

    private void OnEnable()       
    {
        moveTime = 0;   //Counting time from this moment
        m_lineRenderer.positionCount = percision;   //Determine the distance between the player's starting point and the target point... How many points (per point position)
        waveSize = StartWaveSize;   //Manually modify the ripple size
        strightLine = false;    //Initially, the rope is bent

        LinePointsToFirePoint();    //Assign the precision determined point to firepoint

        m_lineRenderer.enabled = true;
    }

    private void OnDisable()        
    {
        m_lineRenderer.enabled = false;
        isGrappling = false;
    }

    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < percision; i++)
        {
            m_lineRenderer.SetPosition(i, grapplingGun.firePoint.position);
        }
    }

    private void Update()
    {
        moveTime += Time.deltaTime;
        DrawRope();
    }

    void DrawRope()     //draw a ropeline
    {
        if (!strightLine)       //Detect if the line needs to be straightened
        {
            //true if all the precision positions set in OnEnable are reached by the player graphics
            if (m_lineRenderer.GetPosition(percision - 1).x == grapplingGun.grapplePoint.x)     
            {
                strightLine = true;
            }
            else
            {   //Otherwise the ripples will be drawn
                DrawRopeWaves();
            }
        }
        else
        {
            if (!isGrappling)
            {
                //grapplingGun.Grapple();
                isGrappling = true;
            }
            if (waveSize > 0)   //Start to gradually reduce the ripple size until it becomes straight
            {
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                waveSize = 0;

                if (m_lineRenderer.positionCount != 2) { m_lineRenderer.positionCount = 2; }

                //After setting. The first point is firepoint, the second point is grapplepoint
                m_lineRenderer.SetPosition(0, grapplingGun.firePoint.position);
                m_lineRenderer.SetPosition(1, grapplingGun.grapplePoint);
            }
        }
    }

    void DrawRopeWaves()    //Adjustment of ripple deviation
    {
        for (int i = 0; i < percision; i++)
        {
            delta = (float)i / ((float)percision - 1f);
            offset = Vector2.Perpendicular(grapplingGun.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            targetPosition = Vector2.Lerp(grapplingGun.firePoint.position, grapplingGun.grapplePoint, delta) + offset;
            //Displacement evaluation by movetime
            currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            m_lineRenderer.SetPosition(i, currentPosition);
        }
    }
}

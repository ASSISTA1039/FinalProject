using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    public AnimationScript anim;

    static protected Movement s_PlayerInstance;
    static public Movement PlayerInstance { get { return s_PlayerInstance; } }

    //public InventoryController inventoryController
    //{
    //    get { return m_InventoryController; }
    //}

    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    [Header("PlayerBehaviourBooleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;
    public bool isAttack;

    private bool groundTouch;
    private bool hasDashed;

    public int side = 1;


    [Header("CollisionScript")]
    private Collision coll;

    [Header("PlayerParticle")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;

    [Header("GrapplingRope")]
    public GrapplingRope rope;
    public GrapplingRope1 rope1;
    // Start is called before the first frame update

    [Header("Health")]
    public PlayerHealth health;


    [Header("Enegy")]
    public PlayerEnegy m_enegy;


    [Header("Dash")]
        public BoxCollider2D boxcollider;

    [Header("Inventory")]
    //public GameObject m_Bag;
    private bool isOpen = false;

    private void Awake()
    {
        s_PlayerInstance = this;

    }
    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
        boxcollider = GetComponent<BoxCollider2D>();
        //m_Bag = GameObject.Find("BagCanvas");
        //m_Bag.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + new Vector2(100, 0), Time.deltaTime);

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        if (canMove)
        {
            Walk(dir);
            anim.SetHorizontalMovement(x, y, rb.velocity.y);
        }

/*        if (coll.onWall && Input.GetButton("Fire3") && canMove)
        {
            if(side != coll.wallSide)
                Flip(side*-1);
            wallGrab = true;
            wallSlide = false;
        }

        if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }*/

        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }
        
        if (wallGrab && !isDashing)
        {
            rb.gravityScale = 0;
            if(x > .2f || x < -.2f)
            rb.velocity = new Vector2(rb.velocity.x, 0);

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        }
        else if(rope.enabled == false && rope1.enabled == false)
        {
            rb.gravityScale = 3;
        }

        if(coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab && PlayerEnegy.temp_energy >= EnegyBar.EnegyMax / 5)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        if (Input.GetButtonDown("Jump") && canMove)
        {
            anim.SetTrigger("jump");

            if (coll.onGround)
                Jump(Vector2.up, false);
            if (coll.onWall && !coll.onGround && PlayerEnegy.temp_energy >= EnegyBar.EnegyMax / 5)
                WallJump();
        }


        if (Input.GetButtonDown("Fire1") && !hasDashed && !isAttack && PlayerEnegy.temp_energy >= EnegyBar.EnegyMax / 5)
        {
            if(xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if(!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        WallParticle(y);

        if (wallGrab || wallSlide || !canMove)
            return;

        if(x > 0)
        {
            side = 1;
            Flip(side);
        }
        if (x < 0)
        {
            side = -1;
            Flip(side);
        }
    }

    void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;

        side = anim.sr.flipX ? -1 : 1;

        jumpParticle.Play();
    }

    private void Dash(float x, float y)
    {
        hasDashed = true;
        //invincible
        boxcollider.enabled = false;

        anim.SetTrigger("dash");

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;

        EnegyBar.EnegyCurrent -= 0.5f;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        dashParticle.Play();
        rb.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        dashParticle.Stop();
        rb.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
        boxcollider.enabled = true;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
        PlayerEnegy.temp_energy -= 1f;
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;

        PlayerEnegy.temp_energy -= 0.01f;
    }

    private void WallSlide()
    {
        PlayerEnegy.temp_energy -=  0.005f;
        if (coll.wallSide != side)
         Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (isAttack && !Input.GetButtonDown("Fire1"))
        {
            rb.velocity = Vector2.zero;
        }
        else if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else if (wallJumped)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed * 2f, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
        {
            rb.velocity = new Vector2(dir.x * speed + rb.velocity.x, rb.velocity.y);
        }
    }

    private void Jump(Vector2 dir, bool wall)
    {
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        particle.Play();
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (wallSlide || (wallGrab && vertical < 0))
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }

    void Flip(int side)
    {
        bool plyerHasXAxisSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        if (plyerHasXAxisSpeed)
        {
            if (side == 1)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if(side == -1)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
/*        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Enemy_Controller") || collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Enemy3"))
        {
            boxcollider.enabled = false;
            startTime = Time.time;
        }*/
        if (collision.gameObject.CompareTag("Key") && collision.GetType().ToString() == "UnityEngine.BoxCollider2D")
        {
            Key.tracePlayer = true;
        }
    }

    public void SetMoveVector(Vector2 newMoveVector)
    {
        rb.velocity = newMoveVector;
    }

    public void UpdateFacing(bool faceLeft)
    {
        if (!faceLeft)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}

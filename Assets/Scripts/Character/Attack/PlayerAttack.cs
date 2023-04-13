using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Item_Buff")]
    public Dictionary_Equip items;

    [Header("AttackDamageDetails")]
    public float damage;
    public static float temp_damage;
    public float doubleDamage;
    [SerializeField]
    private float attack1Radius, attack1Damage;
    [SerializeField]
    private Transform attack1HitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageable;
    protected AttackDetails attackDetails;


    [Header("PlayerInfo")]
    private Rigidbody2D player;
    private AnimationScript anim;
    public PolygonCollider2D _collider2D;
    public GrapplingGun grapplingGun;
    public GrapplingRope rope;
    public GrapplingGun1 grapplingGun1;
    public GrapplingRope1 rope1;
    private PlayerData playerdata;


    [Header("AnimationDetails")]
    public float attackTime;
    private float sumTime;
    private bool canAttack = true;


    // Start is called before the first frame update
    void Start()
    {
        items = GetComponentInParent<Dictionary_Equip>();
        player = GetComponentInParent<Rigidbody2D>();
        anim = GetComponent<AnimationScript>();
        _collider2D = GetComponent<PolygonCollider2D>();
        playerdata = GetComponentInParent<PlayerData>();

        if (TransitionPoint.changedScene)
        {
            playerdata.Load_PLayerState();
            temp_damage = playerdata.Attack;
        }
        else
        {
            temp_damage = damage;
            for (int i = 0; i < items.values.Count; i++)
            {
                temp_damage = items.values[i].buff_Attack;
            }
        }
        doubleDamage = 2 * temp_damage;

    }

    // Update is called once per frame
    void Update()
    {
        doubleDamage = 2 * temp_damage;


        if (Input.GetButtonDown("Attack")&& canAttack)
        {
            canAttack = false;
            anim.SetTrigger("attack");
            player.gameObject.GetComponent<Movement>().isAttack = true;
            grapplingGun.enabled = false;
            grapplingGun1.enabled = false;
            rope.enabled = false;
            rope1.enabled = false;
            sumTime = Time.time + attackTime;
        }

        if(sumTime<Time.time)
        {
            canAttack = true;
            player.gameObject.GetComponent<Movement>().isAttack = false;
            grapplingGun.enabled = true;
            grapplingGun1.enabled = true;
        }
    }

    void AttackStart()
    {
        _collider2D.enabled = true;
    }
    void AttackExit()
    {
        _collider2D.enabled = false;
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatIsDamageable);

        attackDetails.damageAmount = attack1Damage;
        attackDetails.position = transform.position;
        attackDetails.stunDamageAmount = 1f;

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
            Debug.Log(collider.transform.parent);
            //Instantiate hit particle
            if (collider.gameObject.CompareTag("Enemy"))
            {
                if (collider.GetComponentsInChildren<SpriteRenderer>()[1].enabled == true)
                {
                    attackDetails.damageAmount = attack1Damage*2;
                    collider.transform.parent.SendMessage("Damage", attackDetails);
                }
                else
                {
                    Debug.Log(collider.transform.parent);
                    collider.transform.parent.SendMessage("Damage", attackDetails);
                }
                PlayerEnegy.temp_energy += 1.5f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer==12)
        {
            if (collider.GetComponentsInChildren<SpriteRenderer>()[1].enabled == true)
            {
                attackDetails.damageAmount = attack1Damage * 2;
                collider.GetComponent<Enemy>().Damage(attackDetails);
            }
            else
            {
                collider.GetComponent<Enemy>().Damage(attackDetails);
            }

            PlayerEnegy.temp_energy += 1.5f;

        }
    }
}

using UnityEngine;
using DG.Tweening;

public class GhostTrail : MonoBehaviour
{
    private Movement move;
    private AnimationScript anim;
    private SpriteRenderer sr;
    public Transform ghostsParent;
    private GameObject player;
    public Color trailColor;
    public Color fadeColor;
    public float ghostInterval;
    public float fadeTime;

    private void Start()
    {
        player = GameObject.Find("Player");
        anim = GameObject.Find("Player").GetComponentInChildren<AnimationScript>();
        move = GameObject.Find("Player").GetComponent<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        //Debug.Log(player.GetComponent<Transform>().rotation.w);
    }
    public void ShowGhost()
    {
        Sequence s = DOTween.Sequence();

        for (int i = 0; i < ghostsParent.childCount; i++)
        {
            Transform currentGhost = ghostsParent.GetChild(i);
            s.AppendCallback(()=> currentGhost.position = move.transform.position);
            s.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().flipX = player.GetComponent<Transform>().rotation.y == 1 ? true : false);
            s.AppendCallback(()=>currentGhost.GetComponent<SpriteRenderer>().sprite = anim.sr.sprite);
            s.Append(currentGhost.GetComponent<SpriteRenderer>().material.DOColor(trailColor, 0));
            s.AppendCallback(() => FadeSprite(currentGhost));
            s.AppendInterval(ghostInterval); 
        }
    }

    public void FadeSprite(Transform current)
    {
        current.GetComponent<SpriteRenderer>().material.DOKill();
        current.GetComponent<SpriteRenderer>().material.DOColor(fadeColor, fadeTime);
    }


    void Flip(int side)
    {
        bool plyerHasXAxisSpeed = Mathf.Abs(move.gameObject.GetComponent<Rigidbody2D>().velocity.x) > Mathf.Epsilon;
        if (plyerHasXAxisSpeed)
        {
            if (side == 1)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (side == -1)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}

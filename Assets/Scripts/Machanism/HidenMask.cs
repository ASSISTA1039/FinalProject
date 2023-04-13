using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidenMask : MonoBehaviour
{
    private SpriteRenderer img;
    private Color _color;
    private float time;
    private float fadeTime;
    private void Start()
    {
        img = GetComponent<SpriteRenderer>();
        _color = img.color;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && (collision.GetType().ToString() == "UnityEngine.BoxCollider2D" || collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D"))
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, Mathf.Lerp(img.color.a, 0f, 0.1f));
            if (img.color.a == 0)
            {
                Destroy(gameObject);
            }
            //StartCoroutine(DisappearMask());
        }
    }

    IEnumerator DisappearMask()
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = img.color;
            c.a = f;
            img.color = c;
            yield return null;//Next frame continues the for loop
            //yield return WaitForSeconds(.1f);
        }
    }
    /*    private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && (collision.GetType().ToString() == "UnityEngine.BoxCollider2D" || collision.GetType().ToString() == "UnityEngine.CapsuleCollider2D"))
                img.color = _color;
        }*/
}

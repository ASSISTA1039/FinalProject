using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    public Transform _transform;
    public Transform zhidian;
    // Start is called before the first frame update

    public void OpenTheCeiling()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(StartRotating());
    }

    public IEnumerator StartRotating()
    {
        float i = 0;
        while (i < 90f)
        {
            i+=0.5f;
            //Vector3 dest = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, 90), 30);
            transform.RotateAround(zhidian.position, new Vector3(0, 0, 90), -0.5f);

            yield return new WaitForEndOfFrame();
        }

    }
}

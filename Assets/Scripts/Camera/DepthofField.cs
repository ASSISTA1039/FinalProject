/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthofField : MonoBehaviour
{
    public Camera camera;

    public Transform player;

    Vector2 startPosition;

    float startZ;

    Vector2 travel => (Vector2)camera.transform.position - startPosition;

    float distanceFromPlayer => transform.position.z - player.position.z;

    float clippingPlane => (camera.transform.position.z + (distanceFromPlayer > 0 ? camera.farClipPlane : camera.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distanceFromPlayer) / clippingPlane;
    


    void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;
    }


    void Update()
    {
        Vector2 newPos = transform.position = startPosition + travel * parallaxFactor;
        transform.position = new Vector3(newPos.x,newPos.y,startZ);
    }
}
*/
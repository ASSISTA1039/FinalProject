using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ONOFFButton_Wind : MonoBehaviour
{
    public BoxCollider2D _collider;
    public ParticleSystem particle;
    private void Start()
    {
        particle.Stop();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Mogu"))
        {
            _collider.enabled = true;
            particle.Play();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            _collider.enabled = true;
            particle.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mogu") || collision.gameObject.CompareTag("Player"))
        {
            _collider.enabled = false;
            particle.Stop();
        }
    }
}

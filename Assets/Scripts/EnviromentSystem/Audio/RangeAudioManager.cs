using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAudioManager : MonoBehaviour
{
    private AudioSource _audio;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            _audio.mute = false;
            _audio.Play();
            _audio.volume = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.CircleCollider2D")
        {
            _audio.mute =true;
            _audio.Pause();
            _audio.volume = 0f;
        }
    }
}

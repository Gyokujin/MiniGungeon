using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private AudioSource audio;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        audio.Play();
    }
}
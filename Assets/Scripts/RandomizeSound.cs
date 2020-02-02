using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomizeSound : MonoBehaviour
{
    AudioSource source;
    [SerializeField] AudioClip[] clips;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void Play()
    {
        int rand = Random.Range(0, clips.Length - 1);
        source.clip = clips[rand];
        source.Play();
    }
}

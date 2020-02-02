using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] sources;

    public void Mute()
    {
        int length = sources.Length;
        for (int i = 0; i < length; ++i)
        {
            sources[i].mute = true;
        }
    }

    public void Play()
    {
        int length = sources.Length;
        for (int i = 0; i < length; ++i)
        {
            sources[i].mute = false;
        }
    }
}

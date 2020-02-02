using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] sources;
    [SerializeField] private BoolValue isMute;
    [SerializeField] private GameObject isOn;
    [SerializeField] private GameObject isOff;

    private void Start()
    {
        isOn.SetActive(!isMute.value);
        isOff.SetActive(isMute.value);
        if (isMute.value)
        {
            Mute();
        }
    }

    public void Mute()
    {
        isMute.value = true;
        int length = sources.Length;
        for (int i = 0; i < length; ++i)
        {
            sources[i].mute = true;
        }
    }

    public void Play()
    {
        isMute.value = false;
        int length = sources.Length;
        for (int i = 0; i < length; ++i)
        {
            sources[i].mute = false;
        }
    }
}

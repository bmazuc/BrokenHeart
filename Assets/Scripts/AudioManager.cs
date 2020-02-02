using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioManager instance;
    public AudioManager Instance
    {
        get { return instance; }
    }

    public void Init()
    {
        instance = this;
    }
}

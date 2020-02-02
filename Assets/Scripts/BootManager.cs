using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootManager : MonoBehaviour
{
    [SerializeField] private GameObject audioManager;

    // Start is called before the first frame update
    void Start()
    {
        if (audioManager)
        {
            Instantiate(audioManager);
            DontDestroyOnLoad(audioManager);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

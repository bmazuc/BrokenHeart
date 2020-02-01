using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour
{
    [SerializeField] private Gauge selfEsteem;
    [SerializeField] private Gauge attention;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            selfEsteem.Value += 10;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            selfEsteem.Value -= 10;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            attention.Value += 10;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            attention.Value -= 10;
        }
    }
}

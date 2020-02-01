using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HeartAnimController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Gauge gauge;
    [SerializeField] string parameter;
    private int paramaterHashCode;

    void Start()
    {
        animator = GetComponent<Animator>();
        gauge.OnValueChange += UpdateParameter;
        paramaterHashCode = Animator.StringToHash(parameter);
        UpdateParameter();
    }

    void UpdateParameter()
    {
        Debug.Log(gauge.Value);
        animator.SetInteger(paramaterHashCode, gauge.Value);
    }
}

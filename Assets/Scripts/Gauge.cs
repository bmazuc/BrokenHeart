using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGauge", menuName = "BrokenHeart/Gauge")]
public class Gauge : ScriptableObject
{
    [SerializeField]
    private int savedValue;
    private int currentValue;
    public int Value
    {
        set
        {
            currentValue = value;
            if (currentValue > maxValue) currentValue = maxValue;
            if (currentValue < minValue) currentValue = minValue;
            OnValueChange?.Invoke();
        }
        get
        {
            return currentValue;
        }
    }
    [SerializeField]
    private int maxValue;
    public int Max { get { return maxValue; } }
    [SerializeField]
    private int minValue;
    [SerializeField]
    private int lowValue;
    [SerializeField]
    private int highValue;
    public delegate void ValueChange();
    public ValueChange OnValueChange;

    public void Init()
    {
        currentValue = savedValue;
    }

    public int GetDefaultEventId()
    {
        return Convert.ToInt32(currentValue > lowValue) + Convert.ToInt32(currentValue >= highValue);
    }
}

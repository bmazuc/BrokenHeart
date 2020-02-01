using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGauge", menuName = "BrokenHeart/Gauge")]
public class Gauge : ScriptableObject
{
    [SerializeField]
    private int currentValue;
    public int Value
    {
        set
        {
            currentValue = value;
            OnValueChange?.Invoke();
        }
        get
        {
            return currentValue;
        }
    }
    [SerializeField]
    private int maxValue;
    [SerializeField]
    private int minValue;
    public delegate void ValueChange();
    public ValueChange OnValueChange;
}

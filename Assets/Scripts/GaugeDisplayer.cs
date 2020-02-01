using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeDisplayer : MonoBehaviour
{
    [SerializeField] private Gauge gauge;
    private Text text;
    private string name;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        name = text.text;
        gauge.OnValueChange += DisplayValue;
    }

    void DisplayValue()
    {
        text.text = name + gauge.Value;
    }
}

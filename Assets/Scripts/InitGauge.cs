using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGauge : MonoBehaviour
{
    [SerializeField] private Gauge selfEsteemGauge;
    [SerializeField] private Gauge attentionGauge;

    private void Start()
    {
        selfEsteemGauge.Init();
        attentionGauge.Init();
    }
}

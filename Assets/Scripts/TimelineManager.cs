using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    public Timeline place;
    public Gauge selfEsteem;
    public Gauge attention;

    private void Start()
    {
        TextAsset json = Resources.Load<TextAsset>("timelines");
        place = JsonUtility.FromJson<Timeline>(json.text);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            selfEsteem.Value += place.months[0].days[0].hour[0].ami.selfEsteem;
            attention.Value += place.months[0].days[0].hour[0].ami.attention;
        }
    }
}

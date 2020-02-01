using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    public Timeline timeline;
    public Gauge selfEsteem;
    public Gauge attention;

    private void Start()
    {
        TextAsset json = Resources.Load<TextAsset>("timelines");
        timeline = JsonUtility.FromJson<Timeline>(json.text);
    }

    private Event GetPJDefaultEvent()
    {
        return timeline.defaultEvent.pj[selfEsteem.GetDefaultEventId() + (attention.GetDefaultEventId() * 3)];
    }

    private Event GetExDefaultEvent()
    {
        return timeline.defaultEvent.ex[selfEsteem.GetDefaultEventId() + (attention.GetDefaultEventId() * 3)];
    }

    private Event GetFriendDefaultEvent()
    {
        return timeline.defaultEvent.ami[selfEsteem.GetDefaultEventId() + (attention.GetDefaultEventId() * 3)];
    }

    private Event GetMomDefaultEvent()
    {
        return timeline.defaultEvent.maman[selfEsteem.GetDefaultEventId() + (attention.GetDefaultEventId() * 3)];
    }

    private Event GetCenterTownDefaultEvent()
    {
        return timeline.defaultEvent.centreville[selfEsteem.GetDefaultEventId() + (attention.GetDefaultEventId() * 3)];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            selfEsteem.Value += timeline.months[0].days[0].hour[0].ami.selfEsteem;
            attention.Value += timeline.months[0].days[0].hour[0].ami.attention;
        }
    }
}

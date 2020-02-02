using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineManager : MonoBehaviour
{
    public LoadedJSON loadedTimeline;
    public Gauge selfEsteem;
    public Gauge attention;

    public Month[] months;
    public Dictionary<string, Event[]> defaultEvent;

    private void Start()
    {
        TextAsset json = Resources.Load<TextAsset>("timelines");
        loadedTimeline = JsonUtility.FromJson<LoadedJSON>(json.text);

        defaultEvent = new Dictionary<string, Event[]>();
        defaultEvent.Add("pj", loadedTimeline.defaultEvent.pj);
        defaultEvent.Add("ex", loadedTimeline.defaultEvent.ex);
        defaultEvent.Add("ami", loadedTimeline.defaultEvent.ami);
        defaultEvent.Add("maman", loadedTimeline.defaultEvent.maman);
        defaultEvent.Add("centreVille", loadedTimeline.defaultEvent.centreville);

        int length = loadedTimeline.months.Length;
        months = new Month[length];
        for (int i = 0; i < length; ++i)
        {
            LoadedMonth month = loadedTimeline.months[i];
            int dayLength = month.days.Length;
            months[i] = new Month();
            months[i].days = new Day[dayLength];
            for (int j = 0; j < dayLength; ++j)
            {
                LoadedDay day = month.days[j];
                int hourLength = day.hour.Length;
                months[i].days[j] = new Day();
                months[i].days[j].hour = new Hour[hourLength];
                for (int k = 0; k < hourLength; ++k)
                {
                    LoadedHour loadedHour = day.hour[k];
                    months[i].days[j].hour[k] = new Hour();
                    Hour hour = months[i].days[j].hour[k];
                    hour.time = loadedHour.time;
                    hour.events = new Dictionary<string, Event>();
                    hour.events.Add("pj", loadedHour.pj);
                    hour.events.Add("ex", loadedHour.ex);
                    hour.events.Add("ami", loadedHour.ami);
                    hour.events.Add("maman", loadedHour.maman);
                    hour.events.Add("centreVille", loadedHour.centreVille);
                }
            }
        }
    }

    private Event GetDefaultEvent(string key)
    {
        return defaultEvent[key][selfEsteem.GetDefaultEventId() + (attention.GetDefaultEventId() * 3)];
    }

}

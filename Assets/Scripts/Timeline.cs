using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoadedHour
{
    public string time;
    public Event pj;
    public Event ex;
    public Event ami;
    public Event maman;
    public Event centreVille;
}

[System.Serializable]
public class LoadedDay
{
    public LoadedHour[] hour;
}

[System.Serializable]
public class LoadedMonth
{
    public LoadedDay[] days;
}

[System.Serializable]
public class LoadedDefaultEvent
{
    public Event[] pj;
    public Event[] ex;
    public Event[] ami;
    public Event[] maman;
    public Event[] centreville;
}

[System.Serializable]
public class LoadedJSON
{
    public LoadedMonth[] months;
    public LoadedDefaultEvent defaultEvent;
}

[System.Serializable]
public class Hour
{
    public string time;
    public Dictionary<string, Event> events;
}

[System.Serializable]
public class Day
{
    public Hour[] hour;
}

[System.Serializable]
public class Month
{
    public Day[] days;
}

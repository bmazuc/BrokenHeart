using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hour
{
    public string time;
    public Event pj;
    public Event ex;
    public Event ami;
    public Event maman;
    public Event centreVille;
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

[System.Serializable]
public class DefaultEvent
{
    public Event[] pj;
    public Event[] ex;
    public Event[] ami;
    public Event[] maman;
    public Event[] centreville;
}

[System.Serializable]
public class Timeline
{
    public Month[] months;
    public DefaultEvent defaultEvent;
}

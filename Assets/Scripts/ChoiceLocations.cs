//    private Event GetDefaultEvent(string key)
//    {
//        return defaultEvent[key][selfEsteem.GetDefaultEventId() + (attention.GetDefaultEventId() * 3)];
//    }
//}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceLocations : MonoBehaviour
{
    public LoadedJSON loadedJson;
    public Gauge selfEsteem;
    public Gauge attention;

    public Month[] months;
    public Dictionary<string, Event[]> defaultEvent;

    int jour;
    int mois;
    
    bool move;
    int time;
    string infoTime;
    string textDisplay;
    string reaction;
    string imHere;

    public Button buttonMaison;
    public Button buttonMaman;
    public Button buttonEx;
    public Button buttonAmi;
    public Button buttonVille;

    [SerializeField] private Text date;
    [SerializeField] private Text EventOne;
    [SerializeField] private Text EventTwo;
    [SerializeField] private Text EventThree;
    [SerializeField] private Text EventFour;
    [SerializeField] private Text EventFive;
    [SerializeField] private Text EventBilan;

    [SerializeField] private Text ReactionOne;
    [SerializeField] private Text ReactionTwo;
    [SerializeField] private Text ReactionThree;
    [SerializeField] private Text ReactionFour;
    [SerializeField] private Text ReactionFive;
    [SerializeField] private Text ReactionBilan;

    [SerializeField] private Text planningOne;
    [SerializeField] private Text planningTwo;
    [SerializeField] private Text planningThree;
    [SerializeField] private Text planningFour;
    [SerializeField] private Text planningFive;

    [SerializeField] private GameObject pin;

    [SerializeField] private UIManager uiMgr;

    int month;
    int day;
    int hour;

    // Start is called before the first frame update
    void Start()
    {
        LoadJson();

        month = 0;
        day = 0;
        hour = 0;

        time = 9;
        jour = 1;
        mois = 7;
        move = false;
        Button btnMaison = buttonMaison.GetComponent<Button>();
        Button btnMaman = buttonMaman.GetComponent<Button>();
        Button btnEx = buttonEx.GetComponent<Button>();
        Button btnAmi = buttonAmi.GetComponent<Button>();
        Button btnVille = buttonVille.GetComponent<Button>();

        btnMaison.onClick.AddListener(ActionMaison);
        btnMaman.onClick.AddListener(ActionMaman);
        btnEx.onClick.AddListener(ActionEx);
        btnAmi.onClick.AddListener(ActionAmi);
        btnVille.onClick.AddListener(ActionVille);

        date.text = jour + "/0" + mois;

        StartNewDay();
        ActionMaison();

    }

    void LoadJson()
    {
        TextAsset json = Resources.Load<TextAsset>("test");
        loadedJson = JsonUtility.FromJson<LoadedJSON>(json.text);

        defaultEvent = new Dictionary<string, Event[]>();
        defaultEvent.Add("pj", loadedJson.defaultEvent.pj);
        defaultEvent.Add("ex", loadedJson.defaultEvent.ex);
        defaultEvent.Add("ami", loadedJson.defaultEvent.ami);
        defaultEvent.Add("maman", loadedJson.defaultEvent.maman);
        defaultEvent.Add("centreVille", loadedJson.defaultEvent.centreville);

        int length = loadedJson.months.Length;
        months = new Month[length];
        for (int i = 0; i < length; ++i)
        {
            LoadedMonth month = loadedJson.months[i];
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
                    hour.planning = loadedHour.planning;
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

    // Update is called once per frame
    void Update()
    {
        //Touch touch = Input.GetTouch(0);
        //pin.transform.position = touch.position;

        if (move)
        {
            textDisplay = infoTime;


            if (time==9)
            {
                EventOne.text = textDisplay;
                ReactionOne.text = reaction;
            }
            else if (time == 12)
            {
                EventTwo.text = textDisplay;
                ReactionTwo.text = reaction;
            }
            else if (time == 15)
            {
                EventThree.text = textDisplay;
                ReactionThree.text = reaction;
            }
            else if (time == 18)
            {
                EventFour.text = textDisplay;
                ReactionFour.text = reaction;

            }
            else if (time == 21)
            {
                EventFive.text = textDisplay;
                ReactionFive.text = reaction;
                EventBilan.text = "23 h : Bilan de la journée";
            }        
            
            if (mois < 10)
            {
                date.text = jour + "/0" + mois;
            }
            else
            {
                date.text = jour + "/" + mois;
            }

            Debug.Log(textDisplay);

            time = time + 3;
            hour++;

            if (time > 22)
            {
                day++;
                endDay();
            }
            if (jour > 31)
            {
                jour = 1;
                mois++;
                month++;
            }

            if (month > 2)
            {
                EndGame();
            }
            move = false;
        }

        if (month > 1)
        {
            EndGame();
        }
    }

    void ActionMaison()
    {
        Event ev = months[month].days[day].hour[hour].events["pj"];

        if (ev.text == "" || ev.text == "rien")
            ev = GetDefaultEvent("pj");

        if (imHere == "chez moi")
        {
            infoTime = "je reste en un peu chez moi, " + ev.text ; 
        }
        else
        {
            infoTime = "je suis chez moi, " + ev.text;
        }
        reaction = ev.text2;
        move = true;
        imHere = "chez moi";

        selfEsteem.Value += ev.selfEsteem;
        attention.Value += ev.attention;
    }

    void ActionMaman()
    {
        Event ev = months[month].days[day].hour[hour].events["maman"];

        if (ev.text == "" || ev.text == "rien")
            ev = GetDefaultEvent("maman");

        if (imHere == "je suis chez maman")
        {
            infoTime = "je reste encore un peu chez maman, " + ev.text;
        }
        else
        {
            infoTime = "je suis allée chez maman, " + ev.text;
        }
        imHere = "je suis chez maman";
        reaction = ev.text2;
        move = true;
        selfEsteem.Value += ev.selfEsteem;
        attention.Value += ev.attention;
    }

    void ActionEx()
    {
        Event ev = months[month].days[day].hour[hour].events["ex"];

        if (ev.text == "" || ev.text == "rien")
            ev = GetDefaultEvent("ex");

        if (imHere == "je suis chez mon ex")
        {
            infoTime = "je suis toujours chez mon ex, " + ev.text;
        }
        else
        {
            infoTime = "je suis allée chez mon ex, " + ev.text;
        }
        imHere = "je suis chez mon ex";
        reaction = ev.text2;
        move = true;

        selfEsteem.Value += ev.selfEsteem;
        attention.Value += ev.attention;
    }

    void ActionAmi()
    {
        Event ev = months[month].days[day].hour[hour].events["ami"];

        if (ev.text == "" || ev.text == "rien")
            ev = GetDefaultEvent("ami");

        if (imHere == "je suis chez mon ami")
        {
            infoTime = "je suis toujours chez mon ami, " + ev.text;
        }
        else
        {
            infoTime = "je suis chez mon ami, " + ev.text;
        }
        imHere = "je suis chez mon ami";
        reaction = ev.text2;
        move = true;

        selfEsteem.Value += ev.selfEsteem;
        attention.Value += ev.attention;

    }

    void ActionVille()
    {
        Event ev = months[month].days[day].hour[hour].events["centreVille"];

        if (ev.text == "" || ev.text == "rien")
            ev = GetDefaultEvent("centreVille");

        if (imHere == "je suis en ville")
        {
            infoTime = "je suis toujours en ville, " + ev.text;
        }
        else
        {
            infoTime = "je suis partie en ville, " + ev.text;
        }
        imHere = "je suis en ville";
        reaction = ev.text2;
        move = true;

        selfEsteem.Value += ev.selfEsteem;
        attention.Value += ev.attention;
    }

    void endDay()
    {

        if (mois < 10)
        {
            Debug.Log("23 h : Bilan de la journée");

        }
        else
        {
            Debug.Log("23 h : Bilan de la journée");

        }
        time = 9;
        jour++;
        
        EventOne.text = "";
        EventTwo.text = "";
        EventThree.text = "";
        EventFour.text = "";
        EventFive.text = ""; 
        EventBilan.text = "";
        ReactionOne.text = "";
        ReactionTwo.text = "";
        ReactionThree.text = "";
        ReactionFour.text = "";
        ReactionFive.text = "";
        ReactionBilan.text = "";

        StartNewDay();
    }

    void StartNewDay()
    {
        Day currentDay = months[month].days[day];
        planningOne.text = currentDay.hour[0].planning;
        planningTwo.text = currentDay.hour[1].planning;
        planningThree.text = currentDay.hour[2].planning;
        planningFour.text = currentDay.hour[3].planning;
        planningFive.text = currentDay.hour[4].planning;

}

    void EndGame()
    {
        uiMgr.End();
    }

    private Event GetDefaultEvent(string key)
    {
        return defaultEvent[key][selfEsteem.GetDefaultEventId() + (attention.GetDefaultEventId() * 3)];
    }
}
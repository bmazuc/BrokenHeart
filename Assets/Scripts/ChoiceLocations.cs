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
        TextAsset json = Resources.Load<TextAsset>("timelines_definitif");
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
        //Vector3 pinPosition = Camera.main.ScreenToWorldPoint(touch.position);
        //pinPosition.z = 0;
        //pin.transform.position = pinPosition;

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

                StartCoroutine("EndOfADay");
            }
            if (time > 22)
            {
                EventBilan.text = "23 h : Bilan de la journée 22";
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

    IEnumerator EndOfADay()
    {
        yield return new WaitForSeconds(0.2f);
        EventBilan.text = "23 h : Bilan de la journée IEnum";
        yield return new WaitForSeconds(3.5f);
    }

    void ActionMaison()
    {
        pin.transform.position = buttonMaison.transform.position;
        Debug.Log(month);
        Debug.Log(day);
        Debug.Log(hour);
        Event ev = months[month].days[day].hour[hour].events["pj"];

        if (ev.text == "" || ev.text == "rien")
            ev = GetDefaultEvent("pj");

        if (imHere == "Chez moi")
        {
            infoTime = "Je reste en un peu à la piaule, " + ev.text ; 
        }
        else
        {
            infoTime = "Je suis resté à la maison, " + ev.text;
        }
        reaction = ev.text2;
        move = true;
        imHere = "Chez moi";

        selfEsteem.Value += ev.selfEsteem;
        attention.Value += ev.attention;
    }

    void ActionMaman()
    {
        pin.transform.position = buttonMaman.transform.position;
        Event ev = months[month].days[day].hour[hour].events["maman"];

        if (ev.text == "" || ev.text == "rien")
            ev = GetDefaultEvent("maman");

        if (imHere == "Je suis chez Maman, ")
        {
            infoTime = "Je reste encore un peu chez Maman, " + ev.text;
        }
        else
        {
            infoTime = "Je suis allée chez Maman, " + ev.text;
        }
        imHere = "Je suis chez Maman, ";
        reaction = ev.text2;
        move = true;
        selfEsteem.Value += ev.selfEsteem;
        attention.Value += ev.attention;
    }

    void ActionEx()
    {
        pin.transform.position = buttonEx.transform.position;
        Event ev = months[month].days[day].hour[hour].events["ex"];

        if (ev.text == "" || ev.text == "rien")
            ev = GetDefaultEvent("ex");

        if (imHere == "Je suis chez Anna, ")
        {
            infoTime = "Je suis toujours chez Anna, " + ev.text;
        }
        else
        {
            infoTime = "Je suis allée chez Anna, " + ev.text;
        }
        imHere = "Je suis chez Anna, ";
        reaction = ev.text2;
        move = true;

        selfEsteem.Value += ev.selfEsteem;
        attention.Value += ev.attention;
    }

    void ActionAmi()
    {
        pin.transform.position = buttonAmi.transform.position;
        Event ev = months[month].days[day].hour[hour].events["ami"];

        if (ev.text == "" || ev.text == "rien")
            ev = GetDefaultEvent("ami");

        if (imHere == "Je suis chez Seb, ")
        {
            infoTime = "je suis toujours chez Seb, " + ev.text;
        }
        else
        {
            infoTime = "Je suis chez Seb, " + ev.text;
        }
        imHere = "Je suis chez Seb, ";
        reaction = ev.text2;
        move = true;

        selfEsteem.Value += ev.selfEsteem;
        attention.Value += ev.attention;

    }

    void ActionVille()
    {
        pin.transform.position = buttonVille.transform.position;
        Event ev = months[month].days[day].hour[hour].events["centreVille"];

        if (ev.text == "" || ev.text == "rien")
            ev = GetDefaultEvent("centreVille");

        if (imHere == "Je suis en ville ")
        {
            infoTime = "Je suis toujours en ville, " + ev.text;
        }
        else
        {
            infoTime = "Je suis partie en ville, " + ev.text;
        }
        imHere = "Je suis en ville ";
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
        hour = 0;
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
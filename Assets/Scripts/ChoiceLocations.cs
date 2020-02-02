using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceLocations : MonoBehaviour
{
    public LoadedJSON loadedTimeline;
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
    string imHere;

    public Button buttonMaison;
    public Button buttonMaman;
    public Button buttonEx;
    public Button buttonAmi;
    public Button buttonVille;

    public Text date;
    public Text EventOne;
    public Text EventTwo;
    public Text EventThree;
    public Text EventFour;
    public Text EventFive;
    public Text EventBilan;

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

        ActionMaison();

    }

    void LoadJson()
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

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            textDisplay = infoTime;


            if (time == 9)
            {
                EventOne.text = textDisplay;
            }
            else if (time == 12)
            {
                EventTwo.text = textDisplay;
            }
            else if (time == 15)
            {
                EventThree.text = textDisplay;
            }
            else if (time == 18)
            {
                EventFour.text = textDisplay;

            }
            else if (time == 21)
            {
                EventFive.text = textDisplay;
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

            move = false;
        }
    }

    void ActionMaison()
    {

        Event ev = months[month].days[day].hour[hour].events["pj"];

        if (ev.text == "" || ev.text == "rien")
            ev = GetDefaultEvent("pj");

        if (imHere == "chez moi")
        {
            infoTime = "je reste en un peu chez moi, " + ev.text;

        }
        else
        {
            infoTime = "je suis chez moi, " + ev.text;
        }
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
            infoTime = ev.text;
        }
        imHere = "je suis en ville";
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

    }

    private Event GetDefaultEvent(string key)
    {
        return defaultEvent[key][selfEsteem.GetDefaultEventId() + (attention.GetDefaultEventId() * 3)];
    }
}
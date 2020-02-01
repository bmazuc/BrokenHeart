using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceLocations : MonoBehaviour
{
    public Timeline place;
    public Gauge selfEsteem;
    public Gauge attention;

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
        
        TextAsset json = Resources.Load<TextAsset>("timelines");
        Debug.Log(json);
        place = JsonUtility.FromJson<Timeline>(json.text);

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

    // Update is called once per frame
    void Update()
{
    if (move)
    {
            textDisplay = infoTime;


            if (time==9)
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



        if (imHere == "chez moi")
    {
            infoTime = "je reste en un peu chez moi, " + place.months[month].days[day].hour[hour].pj.text ; 

    }
    else
    {
        infoTime = "je suis chez moi, " + place.months[month].days[day].hour[hour].pj.text;
        }
    move = true;
    imHere = "chez moi";

        selfEsteem.Value += place.months[month].days[day].hour[hour].pj.selfEsteem;
        attention.Value += place.months[month].days[day].hour[hour].pj.attention;
    }

void ActionMaman()
{
    if (imHere == "je suis chez maman")
    {
        infoTime = "je reste encore un peu chez maman, " + place.months[month].days[day].hour[hour].maman.text;
        }
    else
    {
        infoTime = "je suis allée chez maman, " + place.months[month].days[day].hour[hour].maman.text;
        }
    imHere = "je suis chez maman";
    move = true;
        selfEsteem.Value += place.months[month].days[day].hour[hour].maman.selfEsteem;
        attention.Value += place.months[month].days[day].hour[hour].maman.attention;
    }

void ActionEx()
{

    if (imHere == "je suis chez mon ex")
    {
        infoTime = "je suis toujours chez mon ex, " + place.months[month].days[day].hour[hour].ex.text;
        }
    else
    {
        infoTime = "je suis allée chez mon ex, " + place.months[month].days[day].hour[hour].ex.text;
        }
    imHere = "je suis chez mon ex";
    move = true;

        selfEsteem.Value += place.months[month].days[day].hour[hour].ex.selfEsteem;
        attention.Value += place.months[month].days[day].hour[hour].ex.attention;
    }
void ActionAmi()
{

    if (imHere == "je suis chez mon ami")
    {
        infoTime = "je suis toujours chez mon ami, " + place.months[month].days[day].hour[hour].ami.text;

        }
    else
    {
        infoTime = "je suis chez mon ami, " + place.months[month].days[day].hour[hour].ami.text;
        }
    imHere = "je suis chez mon ami";
    move = true;

        selfEsteem.Value += place.months[month].days[day].hour[hour].ami.selfEsteem;
        attention.Value += place.months[month].days[day].hour[hour].ami.attention;

    }
void ActionVille()
{

    if (imHere == "je suis en ville")
    {
        infoTime = "je suis toujours en ville, " + place.months[month].days[day].hour[hour].centreVille.text;

        }
    else
    {
        infoTime = "je suis partie en ville, " + place.months[month].days[day].hour[hour].centreVille.text;
        }
    imHere = "je suis en ville";
    move = true;

        selfEsteem.Value += place.months[month].days[day].hour[hour].centreVille.selfEsteem;
        attention.Value += place.months[month].days[day].hour[hour].centreVille.attention;
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

}

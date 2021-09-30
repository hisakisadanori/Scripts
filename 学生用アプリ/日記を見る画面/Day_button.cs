using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day_button : MonoBehaviour
{
    GameObject diary;
    GameObject MyManager;
    DaySelect daySelect_script;

    public int Index;
    int Year;
    int Month;
    int Day;
    float Water;
    string glowLevel;
    string Diary;
    string Weather;

    void Start()
    {

        
        MyManager = GameObject.Find("SelectButtonManager");

        daySelect_script = MyManager.GetComponent<DaySelect>();
        Year = System.Convert.ToInt32(daySelect_script.Year[Index]);
        Month = System.Convert.ToInt32(daySelect_script.Month[Index]);
        Day = System.Convert.ToInt32(daySelect_script.Day[Index]);
        Water = System.Convert.ToSingle(daySelect_script.Water[Index]);
        glowLevel = System.Convert.ToString(daySelect_script.Growlevel[Index]);
        Diary = System.Convert.ToString(daySelect_script.Diary[Index]);
        Weather = System.Convert.ToString(daySelect_script.Weather[Index]);
        
    }

    public void OnClick()
    {
        GameObject parent = GameObject.Find("Canvas");
        diary = parent.transform.Find("diary").gameObject;
        diary.SetActive(true);

        Image plant = diary.transform.GetChild(0).gameObject.GetComponent<Image>();
        Text day = diary.transform.GetChild(1).gameObject.GetComponent<Text>();
        Text weather = diary.transform.GetChild(2).gameObject.GetComponent<Text>();
        Text water = diary.transform.GetChild(3).gameObject.GetComponent<Text>();
        Text nikki = diary.transform.GetChild(4).gameObject.GetComponent<Text>();

        plant.sprite = Resources.Load<Sprite>("plants/" + glowLevel);
        day.text = Year + "/" + Month + "/" + Day;
        water.text = "水を与えた量:" + Water;
        weather.text = "天気:" + Weather;
        nikki.text = Diary;
    }
}

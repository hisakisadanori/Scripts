using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System;

public class DaySelect : MonoBehaviour
{
    public int mode;

    [SerializeField] GameObject Canvas;
    [SerializeField] Button next_button;
    [SerializeField] Button pre_button;
    [SerializeField] int buttons;
    [SerializeField] int firstY;

    public ArrayList Year = new ArrayList();
    public ArrayList Month = new ArrayList();
    public ArrayList Day = new ArrayList();
    public ArrayList Water = new ArrayList();
    public ArrayList Weather = new ArrayList();
    public ArrayList Growlevel = new ArrayList();
    public ArrayList Diary = new ArrayList();

    Day_button button_script;

    int Count = 0;

    void Start()
    {
        pre_button.interactable = false;
        next_button.interactable = true;

        NCMBUser currentUser;
        currentUser = NCMBUser.CurrentUser;
        string Name = currentUser.UserName;


        string classname;
        if(mode == 1) {
            classname = "Diary";
        }
        else
        {
            classname = "Pra_Diary";
        }

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(classname);

        query.WhereEqualTo("name", Name);

        query.FindAsync((List<NCMBObject> DiaryList, NCMBException e) =>
        {
            //検索成功したら
            if (e == null)
            {
                Year = getListFromNCMBObject(DiaryList[0], "year");
                Month = getListFromNCMBObject(DiaryList[0], "month");
                Day = getListFromNCMBObject(DiaryList[0], "day");
                Water = getListFromNCMBObject(DiaryList[0], "water_value");
                Weather = getListFromNCMBObject(DiaryList[0], "weather");
                Growlevel = getListFromNCMBObject(DiaryList[0], "growlevel");
                Diary = getListFromNCMBObject(DiaryList[0], "diary");

                InitializeButton(0);
            }
            else
            {
            }

        });
    }

    void InitializeButton(int startIndex)
    {
        GameObject Button = (GameObject)Resources.Load("Day_Button");

        GameObject[] Clones = GameObject.FindGameObjectsWithTag("day_button");

        foreach (var Clone in Clones)
        {
            Destroy(Clone);
        }

        int LastIndex;

        if (startIndex + buttons > Year.Count) {
            LastIndex = Year.Count;
            next_button.interactable = false;
        }
        else
        {
            LastIndex = startIndex + buttons;
        }

        for (int i = startIndex; i < LastIndex; i++)
        {

            GameObject obj = Instantiate(Button, new Vector3(300, ((i - startIndex) * -50) - firstY, 0), Quaternion.identity) as GameObject;
            obj.name += i;
            obj.transform.SetParent(Canvas.transform, false);

            GameObject button = obj.transform.GetChild(1).gameObject;
            GameObject Textobj = obj.transform.GetChild(2).gameObject;

            button_script = button.GetComponent<Day_button>();
            button_script.Index = i;

            
            Text text = Textobj.GetComponent<Text>();
            text.text = System.Convert.ToString(Year[i]) + "/" + System.Convert.ToString(Month[i]) + "/" + System.Convert.ToString(Day[i]);
        }
    }

    ArrayList getListFromNCMBObject(NCMBObject n, string key)
    {
        if (n.ContainsKey(key))
        {
            return (ArrayList)n[key];
        }
        else
        {
            return new ArrayList();
        }
    }

    public void next()
    {
        Count++;
        pre_button.interactable = true;
        InitializeButton(Count*7);
    }

    public void pre()
    {
        Count--;
        next_button.interactable = true;
        if (Count <= 0)
        {
            pre_button.interactable = false;
        }

        InitializeButton(Count * 7);
    }
}

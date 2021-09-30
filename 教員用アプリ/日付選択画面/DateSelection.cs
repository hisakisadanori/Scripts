using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class DateSelection : MonoBehaviour
{
    public string name = "sada";
    public string mode = "Pra_Diary";
    public ArrayList Year = new ArrayList();
    public ArrayList Month = new ArrayList();
    public ArrayList Day = new ArrayList();
    public ArrayList Weather = new ArrayList();
    public ArrayList Water = new ArrayList();
    public ArrayList Growlevel = new ArrayList();
    public ArrayList Diary = new ArrayList();
    public int Page;

    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] private GameObject ButtonPrevPrefab;
    [SerializeField] private GameObject ButtonNextPrefab;
    private List<GameObject> Buttons = new List<GameObject>();
    private int ButtonNumberAll = 16;
    private int ButtonNumberPerPage = 6;
    private int PageNumber = 2;

    // Start is called before the first frame update
    ArrayList getListFromNCMBObject(NCMBObject n, string key)
    {
        try {
            if (n.ContainsKey(key)) {
                return (ArrayList)n[key];
            }
            else {
                return new ArrayList();
            }
        }
        catch (ArgumentOutOfRangeException) {
            Debug.Log("error 1");
            return new ArrayList();
        }
    }

    void Start()
    {
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(mode);
        query.WhereEqualTo("name", name);
        query.FindAsync((List<NCMBObject> DiaryList, NCMBException e) =>
        {
            //検索成功したら
            if (e == null) {
                Year = getListFromNCMBObject(DiaryList[0], "year");
                Month = getListFromNCMBObject(DiaryList[0], "month");
                Day = getListFromNCMBObject(DiaryList[0], "day");
                Weather = getListFromNCMBObject(DiaryList[0], "weather");
                Water = getListFromNCMBObject(DiaryList[0], "water_value");
                Growlevel = getListFromNCMBObject(DiaryList[0], "growlevel");
                Diary = getListFromNCMBObject(DiaryList[0], "diary");

                ButtonNumberAll = Year.Count;
                Debug.Log("ButtonNumberAll:" + ButtonNumberAll);
                //ButtonNumberPerPage = (Screen.height - 60) / 200;
                Debug.Log("ButtonNumberPerPage:" + ButtonNumberPerPage);
                PageNumber = (int)Math.Ceiling((double)ButtonNumberAll / ButtonNumberPerPage);
                //PageNumber = 2;
                Debug.Log("PageNumber:" + PageNumber);
                changePageTo(1);
            }
            else {
                Debug.Log("error");
                Debug.Log(e);
            }
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changePageTo(int pageIndex)
    {
        Page = pageIndex;
        Debug.Log("ChangePageTo:" + pageIndex);
        // ボタン削除
        foreach(GameObject button in Buttons) {
            Destroy(button);
            Debug.Log("Destroyed");
        }
        Buttons.Clear();

        if (Page > 1) {
            var buttonPrev = Instantiate(ButtonPrevPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Buttons.Add(buttonPrev);
            buttonPrev.transform.parent = transform;
            buttonPrev.GetComponent<RectTransform>().anchoredPosition = new Vector2(200, 20);
        }

        if (Page < PageNumber) {
            var buttonNext = Instantiate(ButtonNextPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Buttons.Add(buttonNext);
            buttonNext.transform.parent = transform;
            buttonNext.GetComponent<RectTransform>().anchoredPosition = new Vector2(-200, 20);
        }

        if (pageIndex < 1 || pageIndex > PageNumber) {
            return;
        }

        // 描画するボタンの数
        int buttonNumber = ButtonNumberAll - (pageIndex - 1) * ButtonNumberPerPage;
        if(buttonNumber > ButtonNumberPerPage) {
            buttonNumber = ButtonNumberPerPage;
        }

        Debug.Log(buttonNumber);

        // ボタンの描画
        for (int i = 0; i < buttonNumber; i++) {
            var button = Instantiate(ButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Buttons.Add(button);
            button.transform.SetParent(transform);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -200 * i - 200);
            int index = (pageIndex - 1) * ButtonNumberPerPage + i;
            button.GetComponent<DateSelectionButton>().index = index;
            button.transform.Find("Text").GetComponent<Text>().text = Year[index] + "." + Month[index] + "." + Day[index];
        }
        /*
        for (int i = 0; i < 7; i++) {
            var button = Instantiate(ButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            Buttons.Add(button);
            button.transform.parent = transform;
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -45 * i - 20);
            int index = (pageIndex - 1) * ButtonNumberPerPage + i;
            button.GetComponent<DateSelectionButton>().index = index;
            button.transform.Find("Text").GetComponent<Text>().text = Year[0] + "." + Month[0] + "." + Day[0];
        }
        */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NCMB;

public class Data_Confirmation : MonoBehaviour
{
    public string Year;
    public string Month;
    public string Day;
    public string Weather;
    public string Water;
    public string Growlevel;
    public string Diary;

    public string name = "";

    // Start is called before the first frame update
    void Start()
    {
        /*
        this.dateYear = "2021";
        this.dateMonth = "8";
        this.dateDay = "25";
        this.water = "750";
        this.growthStage = "2";
        this.daily = "tset";
        */
        Debug.Log("Data_Confirmation: name=" + name);
        /*
        // Apiから読み取り
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Diary");
        query.WhereEqualTo("name", "sada");
        query.FindAsync((List<NCMBObject> DiaryList, NCMBException e) =>
        {
            //検索成功したら
            if (e == null) {
                //Debug.Log(DiaryList[0]["year"]);
                this.Year = DiaryList[0]["year"].ToString();
                this.Month = DiaryList[0]["month"].ToString();
                this.Day = DiaryList[0]["day"].ToString();
                this.Water = DiaryList[0]["water_value"].ToString();
                this.Growlevel = DiaryList[0]["growlevel"].ToString();

                transform.Find("Text_Date").GetComponent<Text>().text = this.Year + "年 " + this.Month + "月 " + this.Day + "日";
                transform.Find("Text_Water").GetComponent<Text>().text = "あげた水の量: " + this.Water + " ml";
                transform.Find("Text_GrowthStage").GetComponent<Text>().text = "成長段階: " + this.Growlevel;
                //transform.GetComponent<Text>()
            }
            else {
                Debug.Log(e);
            }
        });
        */
        transform.Find("Image_Plant").GetComponent<Image>().sprite = Resources.Load<Sprite>("plants/"+ /*"tomato_big_D"*/this.Growlevel);
        transform.Find("Text_Date").GetComponent<Text>().text = this.Year + "年 " + this.Month + "月 " + this.Day + "日";
        transform.Find("Text_Weather").GetComponent<Text>().text = "天気: " + this.Weather;
        transform.Find("Text_Water").GetComponent<Text>().text = "あげた水の量: " + this.Water + " ml";
        //transform.Find("Text_GrowthStage").GetComponent<Text>().text = "成長段階: " + this.Growlevel;
        transform.Find("Text_Dialy").GetComponent<TextMeshProUGUI>().text = this.Diary;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

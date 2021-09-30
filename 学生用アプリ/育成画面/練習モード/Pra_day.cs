using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Pra_day : MonoBehaviour
{
    [SerializeField] Text DateTimeText;
    [SerializeField] GameObject plant_Con;

    GlowSave glowSave;
    GlowData glowData;


    //DateTimeを使うため変数を設定
    DateTime TodayNow;

    public int now_year, now_month, now_day;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnEnable()
    {
        glowSave = plant_Con.GetComponent<GlowSave>();
        glowData = glowSave.Load("Pra_savedata.json");

        var Index_last = glowData.day.Count - 1;

        //時間を取得
        TodayNow = DateTime.Parse(glowData.day[Index_last]);

        now_year = TodayNow.Year;
        now_month = TodayNow.Month;
        now_day = TodayNow.Day;

        //テキストUIに年・月・日・秒を表示させる
        DateTimeText.text = TodayNow.Year.ToString() + "年 " + TodayNow.Month.ToString() + "月" + TodayNow.Day.ToString() + "日";
    }
}

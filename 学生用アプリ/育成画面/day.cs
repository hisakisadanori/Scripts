using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class day : MonoBehaviour
{
    [SerializeField] Text DateTimeText;

    //DateTimeを使うため変数を設定
    DateTime TodayNow;

    public int now_year,now_month,now_day;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnEnable()
    {
        //時間を取得
        TodayNow = DateTime.Now;

        now_year = TodayNow.Year;
        now_month = TodayNow.Month;
        now_day = TodayNow.Day;

        //テキストUIに年・月・日・秒を表示させる
        DateTimeText.text = TodayNow.Year.ToString() + "年 " + TodayNow.Month.ToString() + "月" + TodayNow.Day.ToString() + "日";
    }
}

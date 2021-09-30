using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class Get_PraData : MonoBehaviour
{

    public List<float> GetPreData(int IntervalDays, string LastDay, string Pre, string Muni)
    {
        List<float> Precipitation_amount = new List<float>();

        DateTime lastday = DateTime.Parse(LastDay);
        lastday = lastday.AddYears(-1);

        Muni = Muni.TrimEnd('市', '町', '村', '区');
        Debug.Log(Muni);
        Debug.Log(Pre);

        //JSONデータの読み込み
        string inputString = Resources.Load<TextAsset>("PraDatas").ToString();
        Country inputJson = JsonUtility.FromJson<Country>(inputString);

        for (int j = 0; j < inputJson.Prefecture.Length; j++)
        {
            if (inputJson.Prefecture[j].name.Contains(Pre))
            {
                for (int i = 0; i < inputJson.Prefecture[j].city.Length; i++) {

                    if (inputJson.Prefecture[j].city[i].name.Contains(Muni))
                    {
                        for (int k = 0; k < inputJson.Prefecture[j].city[i].weather.day.Length; k++) {

                            if (DateTime.Parse(inputJson.Prefecture[j].city[i].weather.day[k]) == lastday)
                            {
                                for (int l = 0; l < IntervalDays; l++)
                                {
                                    Precipitation_amount.Add(inputJson.Prefecture[j].city[i].weather.pre[k+l]);
                                }
                            }
                        }
                    }
                }
            }
        }

        foreach (var a in Precipitation_amount)
        {
            Debug.Log(a);
        }
        return Precipitation_amount;
    }

    public List<float> GetTempData(int IntervalDays, string LastDay, string Pre, string Muni)
    {
        List<float> Temp_amount = new List<float>();

        Debug.Log(IntervalDays);
        Debug.Log(LastDay);

        DateTime lastday = DateTime.Parse(LastDay);
        lastday = lastday.AddYears(-1);
        

        Muni = Muni.TrimEnd('市', '町', '村', '区');
        Debug.Log(Muni);
        Debug.Log(Pre);

        string inputString = Resources.Load<TextAsset>("PraDatas").ToString();
        Debug.Log(inputString.Length);

        Country inputJson = JsonUtility.FromJson<Country>(inputString);

        for (int j = 0; j < inputJson.Prefecture.Length; j++)
        {
            if (inputJson.Prefecture[j].name.Contains(Pre))
            {
                Debug.Log(inputJson.Prefecture[j].name);

                for (int i = 0; i < inputJson.Prefecture[j].city.Length; i++)
                {
                    if (inputJson.Prefecture[j].city[i].name.Contains(Muni))
                    {
                        Debug.Log(inputJson.Prefecture[j].city[i].name);

                        for (int k = 0; k < inputJson.Prefecture[j].city[i].weather.day.Length; k++)
                        {

                            if (DateTime.Parse(inputJson.Prefecture[j].city[i].weather.day[k]) == lastday)
                            {
                                Debug.Log(inputJson.Prefecture[j].city[i].weather.day[k]);

                                for (int l = 0; l < IntervalDays; l++)
                                {
                                    Temp_amount.Add(inputJson.Prefecture[j].city[i].weather.temp[k + l]);
                                }
                            }
                        }
                    }
                }
            }
        }

        foreach (var a in Temp_amount)
        {
            Debug.Log(a);
        }

        return Temp_amount;
    }
}

[Serializable]
public class InputJson
{
    public string aaa;
    public int bbb;
}

[Serializable]
public class Country
{
    public Prefecture[] Prefecture;
}

[Serializable]
public class Prefecture
{
    public string name;
    public city[] city;
}

[Serializable]
public class city
{
    public string name;
    public weather weather;
}

[Serializable]
public class weather
{
    public string[] day;
    public float[] pre;
    public float[] temp;
}
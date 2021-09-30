using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using NCMB;
using System;
using System.IO;
using System.Text;

public class weatherloader : MonoBehaviour
{
    public string BaseUrl = "http://api.openweathermap.org/data/2.5/weather";
    public string ApiKey = "84f790fc6e53fb005d0c4c604720ce36"; // APIキーを指定する
    public int TimeOutSeconds = 10;

    [SerializeField] GameObject sunny;
    [SerializeField] GameObject cloud;
    [SerializeField] GameObject rain;

    public void Render(WeatherEntity weatherEntity)
    {
        Debug.Log("weather:" + weatherEntity.weather[0].main);
        Debug.Log("wind:" + weatherEntity.wind.speed);

        if (weatherEntity.weather[0].main == "Clouds")
        {

            cloud.SetActive(true);
            sunny.SetActive(false);
            rain.SetActive(false);

        }
        else if (weatherEntity.weather[0].main == "Rain")
        {

            cloud.SetActive(false);
            sunny.SetActive(false);
            rain.SetActive(true);

        }
        else
        {
            cloud.SetActive(false);
            sunny.SetActive(true);
            rain.SetActive(false);
        }
    }

    public float Render_YDay(WeatherEntity weatherEntity)
    {
        return weatherEntity.current.temp;
    }

    long GetUnixTime_YDay(int day)
    {
        DateTime UnixEpoch = new DateTime(1970, 1, day + 1, 0, 0, 0, 0);
        return (long)(DateTime.Now - UnixEpoch).TotalSeconds;
    }

    public IEnumerator TimeMachine_Load(int days, double latitude, double longitude, Action<List<float>> callback)
    {
        string baseUrl = "http://api.openweathermap.org/data/2.5/onecall/timemachine";
        List<float> temp = new List<float>();

        for (int i = 1; i <= days; i++)
        {

            var url = string.Format("{0}?lat={1}&lon={2}&dt={3}&appid={4}&lang=ja&units=metric", baseUrl, latitude.ToString(), longitude.ToString(), GetUnixTime_YDay(i).ToString(), ApiKey);
            Debug.Log(url);
            var request = UnityWebRequest.Get(url);
            var progress = request.Send();

            int waitSeconds = 0;
            while (!progress.isDone)
            {
                yield return new WaitForSeconds(1.0f);
                waitSeconds++;
                if (waitSeconds >= TimeOutSeconds)
                {
                    Debug.Log("timeout:" + url);
                    yield break;
                }
            }

            if (request.isNetworkError)
            {
                Debug.Log("error:" + url);
            }
            else
            {
                string jsonText = request.downloadHandler.text;
                var weatherEntity = JsonUtility.FromJson<WeatherEntity>(jsonText);
                temp.Add(weatherEntity.current.temp);

                
            }

        }

        callback(temp);
        yield break;
    }

    public IEnumerator Load(double latitude, double longitude, UnityAction<WeatherEntity> callback)
    {
        var url = string.Format("{0}?lat={1}&lon={2}&appid={3}", BaseUrl, latitude.ToString(), longitude.ToString(), ApiKey);
        Debug.Log(url);
        var request = UnityWebRequest.Get(url);
        var progress = request.Send();

        int waitSeconds = 0;
        while (!progress.isDone)
        {
            yield return new WaitForSeconds(1.0f);
            waitSeconds++;
            if (waitSeconds >= TimeOutSeconds)
            {
                Debug.Log("timeout:" + url);
                yield break;
            }
        }

        if (request.isNetworkError)
        {
            Debug.Log("error:" + url);
        }
        else
        {
            string jsonText = request.downloadHandler.text;
            callback(JsonUtility.FromJson<WeatherEntity>(jsonText));
            yield break;
        }
    }


}

using UnityEngine;

[System.Serializable]
public class WeatherEntity
{
    public Weather[] weather;
    public Wind wind;
    public Current current;
}

[System.Serializable]
public class Weather
{
    public string main; // Rain, Snow, Clouds ... etc
}

[System.Serializable]
public class Wind
{
    public float deg;
    public float speed;
}

[System.Serializable]
public class Current
{
    public float temp;
}

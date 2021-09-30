using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weathertext : MonoBehaviour
{
    [SerializeField] Text weather;

    [SerializeField] GameObject sunny;
    [SerializeField] GameObject cloud;
    [SerializeField] GameObject rain;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (sunny.activeSelf)
        {
            weather.text = "天気:晴れ"; 
        }
        else if (cloud.activeSelf)
        {
            weather.text = "天気:くもり";
        }
        else if (rain.activeSelf)
        {
            weather.text = "天気:雨";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

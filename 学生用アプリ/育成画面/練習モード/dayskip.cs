using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class dayskip : MonoBehaviour
{
    [SerializeField] GameObject plant_Controller;
    GlowSave glowSave;
    GlowData glowData;

    string filename = "Pra_savedata.json";

    // Start is called before the first frame update
    void Start()
    {
        glowSave = plant_Controller.GetComponent<GlowSave>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        glowData = glowSave.Load(filename);

        Debug.Log("OK");
        var Index_last = glowData.day.Count - 1;
        DateTime LastDay = DateTime.Parse(glowData.day[Index_last]);

        var TempDay = LastDay.AddDays(1);
        glowData.day.Add(TempDay.ToShortDateString());
        glowData.Once = true;

        glowSave.Save(glowData,filename);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

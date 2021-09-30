using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class view_water : MonoBehaviour
{
    [SerializeField] GameObject plantController;
    [SerializeField] string filename;
    [SerializeField] string unit;
    [SerializeField] GameObject waterpot;
    waterpot waterpot_script;
    GlowSave glowSave;
    GlowData glowData;

    Text myText;
    // Start is called before the first frame updat
    private void OnEnable()
    {
        glowSave = plantController.GetComponent<GlowSave>();
        waterpot_script = waterpot.GetComponent<waterpot>();
        myText = GetComponent<Text>();
        glowData = glowSave.Load(filename);
    }

    void Update()
    {
        if (waterpot.name == "waterpot")
        {
            float value = glowData.water[glowData.water.Count - 1] + waterpot_script.water_value;
            myText.text = value + unit;
        }
        else
        {
            float value = glowData.fert[glowData.fert.Count - 1] + waterpot_script.water_value;
            myText.text = value + unit;
        }
    }
}

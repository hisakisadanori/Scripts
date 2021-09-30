using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dirary_watertext : MonoBehaviour
{
    [SerializeField] Text text;

    [SerializeField] GameObject waterpot;
    waterpot waterpot_script;

    [SerializeField] GameObject plantController;
    GlowSave glowSave;
    GlowData glowData;

    [SerializeField] string filename;
    // Start is called before the first frame update
    void OnEnable()
    {
        glowSave = plantController.GetComponent<GlowSave>();
        glowData = glowSave.Load(filename);

        

        text.text = "水をあげた量:" + glowData.water[glowData.water.Count - 1] + "ml";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

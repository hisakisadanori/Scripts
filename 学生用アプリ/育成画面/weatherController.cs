using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class weatherController : MonoBehaviour
{
    [SerializeField] GameObject plant_Con;
    Get_PraData get_PraData;
    GlowSave glowSave;
    GlowData glowData;

    NCMBUser currentUser;

    [SerializeField] GameObject sunny;
    [SerializeField] GameObject cloud;
    [SerializeField] GameObject rain;

    // Start is called before the first frame update
    public void Set_Weather()
    {
        currentUser = NCMBUser.CurrentUser;

        get_PraData = plant_Con.GetComponent<Get_PraData>();
        glowSave = plant_Con.GetComponent<GlowSave>();

        glowData = glowSave.Load("Pra_savedata.json");

        var Prefecture = currentUser["Pra_Prefecture"].ToString();
        var Municipalities = currentUser["Pra_Municipalities"].ToString();

        var Pre_now = get_PraData.GetPreData(1, glowData.day[glowData.day.Count - 1], Prefecture, Municipalities);

        Debug.Log(glowData.day[glowData.day.Count - 1]);
        Debug.Log(Pre_now[0]);

        if (Pre_now[0] <= 0f)
        {

            cloud.SetActive(false);
            sunny.SetActive(true);
            rain.SetActive(false);

        }
        else if (Pre_now[0] <= 5f)
        {

            cloud.SetActive(true);
            sunny.SetActive(false);
            rain.SetActive(false);

        }
        else
        {
            cloud.SetActive(false);
            sunny.SetActive(false);
            rain.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

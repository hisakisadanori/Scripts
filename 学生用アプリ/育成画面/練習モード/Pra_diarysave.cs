using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;


public class Pra_diarysave : MonoBehaviour
{

    [SerializeField] Text diary_text;

    [SerializeField] GameObject waterbutton;
    Pra_waterbutton waterbutton_script;

    [SerializeField] GameObject plant_Con;
    Pra_plantController plant_script;

    [SerializeField] GameObject day;
    Pra_day day_script;

    [SerializeField] Text weather;

    [SerializeField] Button diarybutton; 

    NCMBUser currentUser;

    [SerializeField] string filename;
    GlowSave glowSave;
    GlowData glowData;

    [SerializeField] GameObject evaluation;
    [SerializeField] Sprite evaluation_A;
    [SerializeField] Sprite evaluation_B;
    [SerializeField] Sprite evaluation_C;
    [SerializeField] Sprite evaluation_D;

    // Start is called before the first frame update
    void Start()
    {
        currentUser = NCMBUser.CurrentUser;
        glowSave = plant_Con.GetComponent<GlowSave>();
        glowData = glowSave.Load(filename);

        currentUser = NCMBUser.CurrentUser;
        waterbutton_script = waterbutton.GetComponent<Pra_waterbutton>();
        day_script = day.GetComponent<Pra_day>();
        plant_script = plant_Con.GetComponent<Pra_plantController>();
        diarybutton = diarybutton.GetComponent<Button>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Pra_Diary");

        query.WhereEqualTo("name", currentUser.UserName);

        query.FindAsync((List<NCMBObject> DiaryList, NCMBException e) =>
        {

            //検索成功したら    
            if (e == null)
            {
                DiaryList[0].AddToList("diary", diary_text.text);
                DiaryList[0].AddToList("water_value", glowData.water[glowData.water.Count - 1]);
                DiaryList[0].AddToList("year", day_script.now_year);
                DiaryList[0].AddToList("month", day_script.now_month);
                DiaryList[0].AddToList("day", day_script.now_day);

                string weather_text = weather.text.Replace("天気:", "");
                DiaryList[0].AddToList("weather", weather_text);

                GameObject planter = GameObject.FindWithTag("planter");
                DiaryList[0].AddToList("growlevel", planter.name);

                if (planter.name.Contains("fruit"))
                {
                    Image evaluation_image = evaluation.GetComponent<Image>();
                    if (planter.name.Contains("A"))
                    {
                        DiaryList[0]["evaluation"] = "A";
                        evaluation_image.sprite = evaluation_A;

                    }
                    else if (planter.name.Contains("B"))
                    {
                        DiaryList[0]["evaluation"] = "B";
                        evaluation_image.sprite = evaluation_B;
                    }
                    else
                    {
                        DiaryList[0]["evaluation"] = "C";
                        evaluation_image.sprite = evaluation_C;
                    }

                    evaluation.SetActive(true);

                }
                if (planter.name.Contains("D"))
                {
                    Image evaluation_image = evaluation.GetComponent<Image>();
                    DiaryList[0]["evaluation"] = "D";
                    evaluation_image.sprite = evaluation_D;

                    evaluation.SetActive(true);
                }
                else
                {
                    DiaryList[0]["evaluation"] = "Z";
                }

                diarybutton.interactable = false;
                glowData.Once = false;

                glowSave.Save(glowData, filename);


                DiaryList[0].Save();
            }
            else
            {
                Debug.Log("No");
            }

        });
    }
}

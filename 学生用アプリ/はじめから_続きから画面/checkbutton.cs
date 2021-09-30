using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using NCMB;

public class checkbutton : MonoBehaviour
{
    string loadscene;

    NCMBUser currentUser;

    // Start is called before the first frame update
    void Start()
    {
        currentUser = NCMBUser.CurrentUser;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        string classname;

        if ((int)currentUser["mode"] == 1)
        {
#if UNITY_EDITOR
            File.Delete(Application.dataPath + "/savedata.json");
#else
            File.Delete(Application.persistentDataPath + "/savedata.json");
#endif
            classname = "Diary";
        }
        else
        {
#if UNITY_EDITOR
            File.Delete(Application.dataPath + "/Pra_savedata.json");
#else
            File.Delete(Application.persistentDataPath + "/Pra_savedata.json");
#endif

            classname = "Pra_Diary";
        }

        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(classname);

        query.WhereEqualTo("name", currentUser.UserName);

        query.FindAsync((List<NCMBObject> DiaryList, NCMBException e) =>
        {
            //検索成功したら
            if (e == null)
            {
                int[] empty = new int[0];
                float[] water = new float[0];
                string[] texts = new string[0];

                DiaryList[0]["day"] = empty;
                DiaryList[0]["year"] = empty;
                DiaryList[0]["month"] = empty;
                DiaryList[0]["water_value"] = water;
                DiaryList[0]["growlevel"] = texts;
                DiaryList[0]["diary"] = texts;
                DiaryList[0]["weather"] = texts;
                DiaryList[0]["evaluation"] = "Z";

                DiaryList[0].Save();
                SceneManager.LoadScene("selectseed_scene");
            }
            else
            {
            }

        });

        
    }
}

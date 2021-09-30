using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using System;
using UnityEngine.SceneManagement;

public class Pra_plantController : MonoBehaviour
{
    string filename = "Pra_savedata.json";
    [SerializeField] GameObject Loading;

    [SerializeField] GameObject diarybutton;
    Button btn;


    [SerializeField] int tomato_zerodays;
    [SerializeField] int tomato_smalldays;
    [SerializeField] int tomato_mediumdays;
    [SerializeField] int tomato_bigdays;
    [SerializeField] int tomato_flowerdays;
    [SerializeField] float tomato_waterA;
    [SerializeField] float tomato_fertA;
    [SerializeField] float tomato_first_fertA;
    [SerializeField] float tomato_tempHigh;
    [SerializeField] float tomato_waterTolerance;
    [SerializeField] float tomato_fertTolerance;
    [SerializeField] float tomato_first_fertTolerance;

    [SerializeField] int toumorokoshi_zerodays;
    [SerializeField] int toumorokoshi_smalldays;
    [SerializeField] int toumorokoshi_mediumdays;
    [SerializeField] int toumorokoshi_bigdays;
    [SerializeField] int toumorokoshi_flowerdays;
    [SerializeField] float toumorokoshi_waterA;
    [SerializeField] float toumorokoshi_fertA;
    [SerializeField] float toumorokoshi_first_fertA;
    [SerializeField] float toumorokoshi_tempHigh;
    [SerializeField] float toumorokoshi_waterTolerance;
    [SerializeField] float toumorokoshi_fertTolerance;
    [SerializeField] float toumorokoshi_first_fertTolerance;

    Get_PraData get_PraData;

    [SerializeField] GameObject plant;
    SpriteRenderer plant_render;

    NCMBUser currentUser;

    GlowSave glowSave;
    public GlowData glowData;

    weatherController controller;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR
        Login("sada", "Sadanori1");
#else
        //ログイン中のユーザ情報の取得
        currentUser = NCMBUser.CurrentUser;
#endif

        //スクリプトの取得
        btn = diarybutton.GetComponent<Button>();
        get_PraData = this.GetComponent<Get_PraData>();
        glowSave = this.GetComponent<GlowSave>();
        controller = this.GetComponent<weatherController>();
        plant_render = plant.GetComponent<SpriteRenderer>();

        //初回起動時の処理
#if UNITY_EDITOR
        if (System.IO.File.Exists(Application.dataPath + "/" + filename) == false)
        { 
            glowSave.ResetData(filename);
        }
#else
        if (System.IO.File.Exists(Application.persistentDataPath + "/" + filename) == false)
        {
            glowSave.ResetData(filename);
        }
#endif



        //ロード
        glowData = glowSave.Load(filename);

        //育成に必要なデータを取得
#if UNITY_EDITOR

#else
        SaveDatas();
        controller.Set_Weather();
#endif

    }

    public void Login(string UserName, string PassWord)
    {
        Debug.Log(UserName);
        Debug.Log(PassWord);

        //NCMBUserのインスタンス作成 
        NCMBUser user = new NCMBUser();

        // ユーザー名とパスワードでログイン
        NCMBUser.LogInAsync(UserName, PassWord, (NCMBException e) => {
            if (e != null)
            {
                UnityEngine.Debug.Log("ログインに失敗: " + e.ErrorMessage);
            }
            else
            {
                UnityEngine.Debug.Log("ログインに成功！");
#if UNITY_EDITOR
                currentUser = NCMBUser.CurrentUser;

                double ido = System.Convert.ToDouble(currentUser["ido"]);
                double keido = System.Convert.ToDouble(currentUser["keido"]);
                SaveDatas();
                controller.Set_Weather();
#else

#endif
            }
        });

    }

    void SaveDatas()
    {
        var Today = DateTime.Now.ToShortDateString();

        if (glowData.day.Count == 0)
        {

            Debug.Log(Today);
            glowData.day.Add(Today);
            glowData.Once = true;
            btn.interactable = true;
            glowData.water.Add(0);
            glowData.fert.Add(0);


            glow_plant();
        }
        else
        {
            //末尾のインデックスを取得
            var Index_last = glowData.day.Count - 1;

            DateTime LastDay = DateTime.Parse(glowData.day[Index_last]);
            var Inteval = (int)(DateTime.Now.Date - LastDay).TotalDays;

            if (Inteval >= 0)
            {
                for (int i = 0; i < Inteval; i++)
                {
                    var TempDay = LastDay.AddDays(i + 1);
                    glowData.day.Add(TempDay.ToShortDateString());
                    glowData.Once = true;
                    btn.interactable = true;
                    glowData.fert.Add(0);
                    glowData.water.Add(0);
                }
            }
            else
            {
                Inteval = glowData.day.Count - glowData.water.Count;

                Debug.Log("Interval:" + Inteval);

                for (int i = 0; i < Inteval; i++)
                {
                    glowData.fert.Add(0);
                    glowData.water.Add(0);
                }
            }
            GetData(Inteval);

        }

        glowSave.Save(glowData, filename);
    }


    void GetData(int days)
    {
        var Index_last = glowData.day.Count - 1;

        var Prefecture = currentUser["Pra_Prefecture"].ToString();
        var Municipalities = currentUser["Pra_Municipalities"].ToString();

        glowData.pre.AddRange(get_PraData.GetPreData(days, glowData.day[Index_last], Prefecture, Municipalities));
        glowData.temp.AddRange(get_PraData.GetTempData(days, glowData.day[Index_last], Prefecture, Municipalities));

        glowSave.Save(glowData, filename);

        glow_plant();
    }

    void glow_plant()
    {
        int seed = System.Convert.ToInt32(currentUser["Pra_seed"]);

        if (seed == 1)
        {
            glow_tomato();
        }
        else
        {
            glow_toumorokoshi();
        }



        Loading.SetActive(false);
    }

    void glow_tomato()
    {
        string imagefile = "";

        if (glowData.day.Count <= tomato_zerodays)
        {
            imagefile = "plants/planter";
        }
        else if (glowData.day.Count <= tomato_smalldays)
        {
            if (tomato_glowCount() == glowData.day.Count - 1)
            {
                imagefile = "plants/tomato_small_A";
            }
            else
            {
                imagefile = "plants/tomato_small_D";
            }
        }
        else if (glowData.day.Count <= tomato_mediumdays)
        {
            if (tomato_glowCount() == glowData.day.Count - 1)
            {
                imagefile = "plants/tomato_medium_A";
            }
            else
            {
                imagefile = "plants/tomato_medium_D";
            }
        }
        else if (glowData.day.Count <= tomato_bigdays)
        {
            if (tomato_glowCount() == glowData.day.Count - 1)
            {
                imagefile = "plants/tomato_big_A";
            }
            else
            {
                imagefile = "plants/tomato_big_D";
            }
        }
        else if (glowData.day.Count <= tomato_flowerdays)
        {
            if (tomato_glowCount() == glowData.day.Count - 1)
            {
                imagefile = "plants/tomato_flower_A";
            }
            else
            {
                imagefile = "plants/tomato_flower_D";
            }
        }
        else
        {
            if (tomato_glowCount() == glowData.day.Count - 1)
            {
                if (tomato_fertCount() == glowData.day.Count - 1)
                {
                    imagefile = "plants/tomato_fruit_A";
                }
                else if (toumorokoshi_fertCount() == glowData.day.Count - 2)
                {
                    imagefile = "plants/tomato_fruit_B";
                }
                else
                {
                    imagefile = "plants/tomato_fruit_C";
                }
            }
            else if (tomato_glowCount() == glowData.day.Count - 2)
            {
                imagefile = "plants/tomato_fruit_B";

                if (tomato_fertCount() == glowData.day.Count - 1)
                {
                    imagefile = "plants/tomato_fruit_B";
                }
                else
                {
                    imagefile = "plants/tomato_fruit_C";
                }
            }
            else
            {
                imagefile = "plants/tomato_fruit_C";
            }
        }

        Sprite plant_Image = Resources.Load<Sprite>(imagefile);
        plant_render.sprite = plant_Image;
        plant.name = imagefile.Replace("plants/", "");

    }

    void glow_toumorokoshi()
    {
        string imagefile = "";

        if (glowData.day.Count <= toumorokoshi_zerodays)
        {
            imagefile = "plants/planter";
        }
        else if (glowData.day.Count <= toumorokoshi_smalldays)
        {
            if (toumorokoshi_glowCount() == glowData.day.Count - 1)
            {
                imagefile = "plants/toumorokoshi_small_A";
            }
            else
            {
                imagefile = "plants/toumorokoshi_small_D";
            }
        }
        else if (glowData.day.Count <= toumorokoshi_mediumdays)
        {
            if (toumorokoshi_glowCount() == glowData.day.Count - 1)
            {
                imagefile = "plants/toumorokoshi_medium_A";
            }
            else
            {
                imagefile = "plants/toumorokoshi_medium_D";
            }
        }
        else if (glowData.day.Count <= toumorokoshi_bigdays)
        {
            if (toumorokoshi_glowCount() == glowData.day.Count - 1)
            {
                imagefile = "plants/toumorokoshi_big_A";
            }
            else
            {
                imagefile = "plants/toumorokoshi_big_D";
            }
        }
        else if (glowData.day.Count <= toumorokoshi_flowerdays)
        {
            imagefile = "plants/toumorokoshi_flower_A";
        }
        else
        {
            if (toumorokoshi_glowCount() == glowData.day.Count - 1)
            {
                if (toumorokoshi_fertCount() == glowData.day.Count - 1)
                {
                    imagefile = "plants/toumorokoshi_fruit_A";
                }
                else
                {
                    imagefile = "plants/toumorokoshi_fruit_B";
                }
            }
            else
            {
                imagefile = "plants/toumorokoshi_fruit_B";
            }

        }

        Sprite plant_Image = Resources.Load<Sprite>(imagefile);
        plant_render.sprite = plant_Image;
        plant.name = imagefile.Replace("plants/", "");

    }

    int tomato_fertCount()
    {
        int fertCount = 0;

        for (int i = 0; i < glowData.day.Count - 1; i++)
        {
            if (i == 0)
            {
                if (Math.Abs(tomato_first_fertA - glowData.fert[i]) < tomato_first_fertTolerance)
                {
                    fertCount++;
                }
            }

            if (i % 14 == 0)
            {
                if (Math.Abs(tomato_fertA - glowData.fert[i]) < tomato_fertTolerance)
                {
                    fertCount++;
                }
            }
            else
            {
                if (glowData.fert[i] == 0)
                {
                    fertCount++;
                }
            }


        }

        return fertCount;
    }

    int tomato_glowCount()
    {

        int glowCount = 0;

        for (int i = 0; i < glowData.day.Count - 1; i++)
        {
            if (glowData.temp[i] >= tomato_tempHigh)
            {
                tomato_waterA += glowData.temp[i];
            }

            if (Math.Abs(tomato_waterA - (glowData.water[i] + glowData.pre[i])) < tomato_waterTolerance)
            {
                glowCount++;
            }
        }

        return glowCount;
    }

    int toumorokoshi_glowCount()
    {
        int glowCount = 0;

        for (int i = 0; i < glowData.day.Count - 1; i++)
        {
            if (glowData.temp[i] >= toumorokoshi_tempHigh)
            {
                toumorokoshi_waterA += glowData.temp[i];
            }

            if (Math.Abs(toumorokoshi_waterA - (glowData.water[i] + glowData.pre[i])) < toumorokoshi_waterTolerance)
            {
                glowCount++;
            }
        }

        return glowCount;
    }

    int toumorokoshi_fertCount()
    {

        int fertCount = 0;

        for (int i = 0; i < glowData.day.Count - 1; i++)
        {
            if (i == 0)
            {
                if (Math.Abs(toumorokoshi_first_fertA - glowData.fert[i]) < toumorokoshi_first_fertTolerance)
                {
                    fertCount++;
                }
            }

            if (i % 14 == 0)
            {
                if (Math.Abs(toumorokoshi_fertA - glowData.fert[i]) < toumorokoshi_fertTolerance)
                {
                    fertCount++;
                }
            }
            else
            {
                if (glowData.fert[i] == 0)
                {
                    fertCount++;
                }
            }
        }

        return fertCount;
    }
}

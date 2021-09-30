using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using NCMB;

public class waterbutton : MonoBehaviour
{
    string filename = "savedata.json";

    public int value = 0;

    [SerializeField] GameObject plantCon;
    GlowSave glowSave;
    GlowData glowData;

    [SerializeField] GameObject my_sprite;
    waterpot waterpot_script;

    [SerializeField] GameObject other_sprite;

    [SerializeField] Image image;
    [SerializeField] Sprite ON_sprite;
    [SerializeField] Sprite OFF_sprite;
    [SerializeField] GameObject Video;

    public LocationServiceStatus Status;
    public LocationInfo Location { get; private set; }

    NCMBUser currentUser;

    // Start is called before the first frame update
    void Start()
    {
        my_sprite.SetActive(false);
        image = this.GetComponent<Image>();

        currentUser = NCMBUser.CurrentUser;
        waterpot_script = my_sprite.GetComponent<waterpot>();

        glowSave = plantCon.GetComponent<GlowSave>();

    }



    IEnumerator Location_Start(UnityAction<float, float> callback)
    {
        this.Status = Input.location.status;
        if (Input.location.isEnabledByUser)
        {
            if (this.Status == LocationServiceStatus.Stopped)
            {
                Input.location.Start();
                Debug.Log(Status.ToString());
                yield return new WaitUntil(() => Input.location.status == LocationServiceStatus.Running);

                this.Location = Input.location.lastData;
                Debug.Log(Input.location.status.ToString());

                Debug.Log("lat:" + Location.latitude.ToString());
                Debug.Log("lng:" + Location.longitude.ToString());

                Input.location.Stop();

                callback(Location.latitude, Location.longitude);

            }



        }
        else
        {
            // FIXME 位置情報を有効にして!! 的なダイアログの表示処理を入れると良さそう
            Debug.Log("location is disabled by user");
        }

    }

    public void water_start(float C_ido, float C_keido)
    {

        float ido = System.Convert.ToSingle(currentUser["ido"]);
        float keido = System.Convert.ToSingle(currentUser["keido"]);

        if ((Math.Abs(ido - C_ido)) < 0.1 && (Math.Abs(keido - C_keido)) < 0.1)
        {
            Debug.Log("ido keido OK");
            my_sprite.SetActive(true);
            Video.SetActive(true);
            image.sprite = OFF_sprite;
        }
        else
        {
            Debug.Log("ido keido NO");
        }
    }

    public void OnClick()
    {


        Debug.Log("button push");
        if (other_sprite.activeSelf)
        {
        }
        else
        {
            if (!my_sprite.activeSelf)
            {
                StartCoroutine(Location_Start(water_start));
            }
            else
            {
                //水やりした量を計算
                value += waterpot_script.water_value;
                value_save();

                my_sprite.SetActive(false);
                Video.SetActive(false);
                image.sprite = ON_sprite;

            }
        }


    }

    void value_save()
    {
        glowData = glowSave.Load(filename);

        //末尾のインデックスを取得
        var Index_last = glowData.day.Count - 1;

        Debug.Log("water:" + glowData.water.Count);
        Debug.Log("day:" + glowData.day.Count);


        if (this.gameObject.name == "water_button")
        {
            glowData.water[Index_last] += value;
        }
        else
        {
            glowData.fert[Index_last] += value;
        }
        value = 0;

        glowSave.Save(glowData, filename);
        Debug.Log(glowData);

    }
}
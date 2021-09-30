using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using UnityEngine.SceneManagement;
using System;


public class select_locatin : MonoBehaviour
{
    private ReverseGeocoding reverseGeocoding;

    public float IntervalSeconds = 1.0f;
    public LocationServiceStatus Status;
    public LocationInfo Location { get; private set; }
    private int mode;

    public Text text;

    NCMBUser currentUser;

    private void Start()
    {
        //カレントユーザを定義
        currentUser = NCMBUser.CurrentUser;
        mode = System.Convert.ToInt32(currentUser["mode"]);

        //テキスト表示オブジェクト
        reverseGeocoding = GetComponent<ReverseGeocoding>();
    }

    public bool CanGetLonLat()
    {
        return Input.location.isEnabledByUser;
    }

    IEnumerator Location_Start()
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


                if (mode == 1)
                {
                    currentUser["ido"] = Location.latitude;
                    currentUser["keido"] = Location.longitude;
                }
                else if(mode == 2)
                {
                    currentUser["Pra_ido"] = Location.latitude;
                    currentUser["Pra_keido"] = Location.longitude;
                }
                currentUser.SaveAsync((NCMBException e) =>
                {
                    if (e != null)
                    {
                        Debug.Log("保存失敗");
                    }

                    else
                    {
                        Debug.Log("保存成功!");
                    }
                });

                Input.location.Stop();

                reverseGeocoding.GetAddr();
                
                

            }
            
            
        }
        else
        {
            // FIXME 位置情報を有効にして!! 的なダイアログの表示処理を入れると良さそう
            Debug.Log("location is disabled by user");
        }

    }



    public void OnClick()
    {
        StartCoroutine("Location_Start");

    }

}


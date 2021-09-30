using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB; //NCMBの機能を追加

public class select_mode : MonoBehaviour
{
    NCMBUser currentUser; 

    void Start() {
        currentUser = NCMBUser.CurrentUser;
    }

    public void OnClick()
    {
        int mode;
        
        if (currentUser != null)
        {
            UnityEngine.Debug.Log("ログイン中のユーザー: " + currentUser.UserName);
            
        }
        else
        {
            UnityEngine.Debug.Log("未ログインまたは取得に失敗");
        }

        if (this.gameObject.name == "real_mode")
        {
            mode = 1;
            currentUser["mode"] = mode;
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
            UnityEngine.Debug.Log("ログイン中のユーザーのモード " + currentUser["mode"]);

            Debug.Log(mode);
        }
        else if(this.gameObject.name == "practice_mode")
        {
            mode = 2;
            currentUser["mode"] = mode;
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
            UnityEngine.Debug.Log("ログイン中のユーザーのシード " + currentUser["mode"]);
            Debug.Log(mode);
        }
        else if (this.gameObject.name == "show_data")
        {
            mode = 3;
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
            currentUser["mode"] = mode;
            UnityEngine.Debug.Log("ログイン中のユーザーのシード " + currentUser["mode"]);
            Debug.Log(mode);
        }

    }

}

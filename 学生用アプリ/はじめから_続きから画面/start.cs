using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using NCMB;

public class start : MonoBehaviour
{
    NCMBUser currentUser;

    string loadscene = "selectseed_scene";
    [SerializeField] GameObject check;

    void Start()
    {
        string filename;
        currentUser = NCMBUser.CurrentUser;
        if ((int)currentUser["mode"] == 1)
        {
            filename = "savedata.json";
        }
        else
        {
            filename = "Pra_savedata.json";
        }

        if (this.gameObject.name == "tudukikara")
        {
#if UNITY_EDITOR
            if (System.IO.File.Exists(Application.dataPath + "/" + filename) == false)
            { 
                Button btn = GetComponent<Button>();
                btn.interactable = false;
            }
#else
            if (System.IO.File.Exists(Application.persistentDataPath + "/" + filename) == false)
            {
                Button btn = GetComponent<Button>();
                btn.interactable = false;
            }
#endif
        }
    }

    public void onclick()
    {
        Debug.Log("button on");

        if(this.gameObject.name == "hazimekara")
        {
            check.SetActive(true);
        }
        else
        {
            if ((int)currentUser["mode"] == 1)
            {
                loadscene = "hisaki_scene";
            }
            else
            {
                loadscene = "practice_scene";
            }

            ChangeScene();
        }

        
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(loadscene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using UnityEngine.SceneManagement;
using System;

public class Ready : MonoBehaviour
{
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
        string scene_name = "";
        int mode = System.Convert.ToInt32(currentUser["mode"]);
        if (mode == 1)
        {
            scene_name = "hisaki_scene";
        }
        else
        {
            scene_name = "practice_scene";
        }

        SceneManager.LoadScene(scene_name);
    }
}

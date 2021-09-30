using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using UnityEngine.SceneManagement;
using System;

public class select_seed : MonoBehaviour
{


    bool seed1, seed2;
    [SerializeField] GameObject seed_1;
    [SerializeField] GameObject seed_2;

    Outline outline_1;
    Outline outline_2;


    private int seed;
    private int mode;
    NCMBUser currentUser;

    // Start is called before the first frame update
    void Start()
    {
        currentUser = NCMBUser.CurrentUser;
        mode = System.Convert.ToInt32(currentUser["mode"]);
        Debug.Log(currentUser.UserName);
        outline_1 = seed_1.GetComponent<Outline>();
        outline_2 = seed_2.GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void select_seed1()
    {
        seed1 = true;
        seed2 = false;

        outline_1.enabled = true;
        outline_2.enabled = false;

        Debug.Log(seed1);
        Debug.Log(seed2);
    }

    public void select_seed2()
    {
        seed1 = false;
        seed2 = true;

        outline_1.enabled = false;
        outline_2.enabled = true;

        Debug.Log(seed1);
        Debug.Log(seed2);
    }

    void save_seed(int seed)
    {


        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Diary");

        query.WhereEqualTo("name",currentUser.UserName);

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索成功したら    
            if (e == null)
            {
                if (mode == 1)
                {
                    objList[0]["seed"] = seed;
                    currentUser["seed"] = seed;
                    
                }
                else if(mode == 2)
                {
                    objList[0]["Pra_seed"] = seed;
                    currentUser["seed"] = seed;
                }

                objList[0].Save();
                currentUser.SaveAsync((NCMBException er) =>
                {
                    if (er != null)
                    {
                        Debug.Log("保存失敗");
                    }

                    else
                    {
                        Debug.Log("保存成功!");
                    }
                });
            }

        });

    }

    public void OnClick()
    {
        if (seed1 == true && seed2 == false)
        {
            
            seed = 1;
            Debug.Log(seed);

            save_seed(seed);

            SceneManager.LoadScene("selectlocation_scene");

        }
        else if (seed2 == true && seed1 == false)
        {
            
            seed = 2;
            Debug.Log(seed);

            save_seed(seed);

            SceneManager.LoadScene("selectlocation_scene");
        }

    }

}

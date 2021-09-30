using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class data_get : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    [SerializeField] Button next_button;
    [SerializeField] Button pre_button;
    [SerializeField] private float distance;

    public string classname;
    int buttonCount = 0;

    List<NCMBObject> instance = new List<NCMBObject>();

    // Start is called before the first frame update
    void Start()
    {
        next_button.interactable = true;
        pre_button.interactable = false;
        
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>(classname);

        query.OrderByAscending("Index");

        query.FindAsync((List<NCMBObject> DiaryList, NCMBException e) =>

        {
            //検索成功したら    
            if (e == null)
            {
                foreach(var a in DiaryList) {
                    Debug.Log(a["Index"]);
                }

                Debug.Log("Yes");
                Debug.Log(DiaryList[0]["name"]);
                for (int i = 0; i < DiaryList.Count; i++)
                {
                    instance.Add(DiaryList[i]);
                    
                }

                ButtonMake(0);

            }
            else
            {
                Debug.Log("No");
            }





        });
    }

    void ButtonMake(int startIndex)
    {
        GameObject[] students = GameObject.FindGameObjectsWithTag("shokyo");

        foreach(var student in students)
        {
            Destroy(student);
        }

        int LastIndex;

        if(startIndex + 5 >= instance.Count)
        {
            LastIndex = instance.Count;
            next_button.interactable = false;
        }
        else
        {
            LastIndex = startIndex + 5;
            next_button.interactable = true;
        }

        for(int i = startIndex; i < LastIndex; i++)
        {
            GameObject prefab = Resources.Load<GameObject>("Prefab/student_set");
            GameObject student = Instantiate(prefab);
            student.transform.position = new Vector3(0, ((i - startIndex) * -distance - 300), 0);
            student.transform.SetParent(Canvas.transform, false);

            GameObject name = student.transform.GetChild(0).gameObject;
            GameObject number = student.transform.GetChild(1).gameObject;
            GameObject button = student.transform.GetChild(2).gameObject;
            GameObject evaluation = student.transform.GetChild(3).gameObject;

            number.transform.GetChild(0).GetComponent<Text>().text = System.Convert.ToString(instance[i]["Index"]);
            name.transform.GetChild(0).GetComponent<Text>().text = System.Convert.ToString(instance[i]["name"]);

            if(System.Convert.ToString(instance[i]["evaluation"]) == "Z")
            {
                evaluation.transform.GetChild(0).GetComponent<Text>().text = "";
            }
            else
            {
                evaluation.transform.GetChild(0).GetComponent<Text>().text = System.Convert.ToString(instance[i]["evaluation"]);
            }
            var browsing = button.GetComponent<browsing>();
            browsing.name = System.Convert.ToString(instance[i]["name"]);
            browsing.classname = classname;
            
        }
    }


    public void next()
    {
        buttonCount++;
        pre_button.interactable = true;
        ButtonMake(buttonCount * 5);
    }

    public void pre()
    {
        buttonCount--;
        Debug.Log(buttonCount);
        
        ButtonMake(buttonCount * 5);

        if (buttonCount <= 0)
        {
            pre_button.interactable = false;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class diary_button : MonoBehaviour
{
    [SerializeField] GameObject diary;
    [SerializeField] Text diary_text;
    [SerializeField] GameObject pot;
    [SerializeField] GameObject bag;

    [SerializeField] GameObject plantCon;
    [SerializeField] string filename;
    GlowSave glowSave;
    GlowData glowData;

    // Start is called before the first frame update
    void Start()
    {
        glowSave = plantCon.GetComponent<GlowSave>();
        glowData = glowSave.Load(filename);


        Debug.Log("glowData.Once:" + glowData.Once);

        if (!glowData.Once)
        {
            Button btn = GetComponent<Button>();
            btn.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (!(pot.activeSelf || bag.activeSelf))
        {

            if (diary.activeSelf)
            {
                diary.SetActive(false);
            }
            else
            {
                diary.SetActive(true);
            }
        }
    }
}

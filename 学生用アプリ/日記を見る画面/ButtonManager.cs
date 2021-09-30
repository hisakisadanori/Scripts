using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject[] button_objs; //無効化するボタン

    List<Button> buttons = new List<Button>();

    bool Buttonflag = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < button_objs.Length; i++)
        {
            buttons.Add(button_objs[i].GetComponent<Button>());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonOn()
    {
        if (Buttonflag)
        {
            foreach (var button in buttons)
            {
                button.interactable = false;
            }

            Buttonflag = false;
        }
        else
        {
            foreach (var button in buttons)
            {
                button.interactable = true;
            }

            Buttonflag = true;
        }
    }

}

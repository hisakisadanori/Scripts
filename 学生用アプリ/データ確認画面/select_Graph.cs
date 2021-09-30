using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select_Graph : MonoBehaviour
{
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject Graph;
    [SerializeField] GameObject DiaryButton;
    ButtonToDiary Diary_script;
    Data_Graph graph;

    // Start is called before the first frame update
    void Start()
    {
        graph = Graph.GetComponent<Data_Graph>();
        Diary_script = DiaryButton.GetComponent<ButtonToDiary>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if(this.name == "real")
        {
            graph.filename = "savedata.json";
            Diary_script.select_mode = 1;
        }

        else if(this.name == "practice")
        {
            graph.filename = "Pra_savedata.json";
            Diary_script.select_mode = 2;
        }

        Graph.SetActive(true);
        buttons.SetActive(false);
        DiaryButton.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DateSelectionButton : MonoBehaviour
{
    public int index;
    private DateSelection dateSelectionScript;
    // Start is called before the first frame update
    void Start()
    {
        dateSelectionScript = GameObject.Find("Canvas").GetComponent<DateSelection>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        // イベントに登録
        SceneManager.sceneLoaded += GameSceneLoaded;

        // シーン切り替え
        SceneManager.LoadScene("DataConfirmation_Scene");
    }

    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        var script = GameObject.FindWithTag("Data_Confirmation").GetComponent<Data_Confirmation>();
        Debug.Log(dateSelectionScript.Year[index]);
        script.Year = (dateSelectionScript.Year[index]).ToString();
        script.Month = dateSelectionScript.Month[index].ToString();
        script.Day = dateSelectionScript.Day[index].ToString();
        script.Weather = dateSelectionScript.Weather[index].ToString();
        script.Water = dateSelectionScript.Water[index].ToString();
        //Debug.Log("water:" + dateSelectionScript.Water[index].ToString());
        script.Growlevel = dateSelectionScript.Growlevel[index].ToString();
        script.Diary = dateSelectionScript.Diary[index].ToString();

        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}

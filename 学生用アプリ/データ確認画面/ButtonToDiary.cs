using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonToDiary : MonoBehaviour
{
    public int select_mode;

    public void onClick()
    {
        // イベントに登録
        SceneManager.sceneLoaded += GameSceneLoaded;

        // シーン切り替え
        SceneManager.LoadScene("Read_diary");
    }
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        var dayselect = GameObject.FindWithTag("GameController").GetComponent<DaySelect>();

        // データを渡す処理
        dayselect.mode = select_mode;

        // イベントから削除
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}

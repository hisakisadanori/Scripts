using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using UnityEngine.SceneManagement;

public class browsing : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string loadscene;
    public string name;
    public string classname;
    void Start()
    {

    }
    public void onclick()
    {
        // イベントに登録
        SceneManager.sceneLoaded += GameSceneLoaded;

        // シーン切り替え
        //SceneManager.LoadScene("GameScene");
    
    
        Debug.Log("button on");
        ChangeScene();
    }
    private void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        // シーン切り替え後のスクリプトを取得
        var script = GameObject.Find("Canvas").GetComponent<DateSelection>();
        //Debug.Log(gameManager.name);
        //Debug.Log("kakaak");

        // データを渡す処理
        script.name = name;
        script.mode = classname; 
        // イベントから削除
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
    // Update is called once per frame
    void Update()
    {

    }
    void ChangeScene()
    {
        SceneManager.LoadScene(loadscene);
    }
}
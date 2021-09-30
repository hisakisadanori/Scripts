using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadscene_button : MonoBehaviour
{
    [SerializeField] private string scene_name;

    //ボタンを押した時に呼び出される
    public void OnClick()
    {
        
        SceneManager.LoadScene(scene_name);
    }
}

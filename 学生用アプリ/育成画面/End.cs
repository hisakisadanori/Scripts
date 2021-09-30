using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    Text Child_Text;

    void Start()
    {
        Child_Text = transform.GetChild(2).gameObject.GetComponent<Text>();
        StartCoroutine("Corou2");
        
    }

    //コルーチン関数を定義
    private IEnumerator Corou2() //コルーチン関数の名前
    {         //コルーチンの内容
        Child_Text.text = "3秒後にスタート画面に戻ります";
        yield return new WaitForSeconds(1.0f);
        Child_Text.text = "2秒後にスタート画面に戻ります";
        yield return new WaitForSeconds(1.0f);
        Child_Text.text = "1秒後にスタート画面に戻ります";
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("start_scene");

    }
}

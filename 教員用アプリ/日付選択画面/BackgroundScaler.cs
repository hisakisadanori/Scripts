using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    void Start()
    {
        // 背景サイズ調整
        transform.Find("Image_Background").GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}

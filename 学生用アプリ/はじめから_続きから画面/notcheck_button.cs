using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class notcheck_button : MonoBehaviour
{
    public void OnClick()
    {
        transform.parent.gameObject.SetActive(false);
    }
}

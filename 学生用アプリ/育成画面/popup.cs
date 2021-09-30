using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup : MonoBehaviour
{
    [SerializeField] GameObject warning;

    public void Open()
    {
        warning.SetActive(true);
    }

    public void Close()
    {
        warning.SetActive(false);
    }
}

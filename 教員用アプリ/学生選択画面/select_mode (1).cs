using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select_mode : MonoBehaviour
{
    [SerializeField] GameObject student;
    data_get data_Get;

    // Start is called before the first frame update
    void Start()
    {
        data_Get = student.transform.GetChild(0).gameObject.GetComponent<data_get>();
    }

    public void OnReal()
    {
        data_Get.classname = "Diary";

        student.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnPractice()
    {
        data_Get.classname = "Pra_Diary";

        student.SetActive(true);
        this.gameObject.SetActive(false);
    }
}

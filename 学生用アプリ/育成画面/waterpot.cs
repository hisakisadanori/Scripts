using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class waterpot : MonoBehaviour
{
    Quaternion rotRH;


    float firstrot_z;
    [SerializeField] GameObject water;
    [SerializeField] GameObject view_water;
    [SerializeField] float min_z;
    [SerializeField] float max_z;
    [SerializeField] float first;

    [SerializeField] public int water_value;
    [SerializeField] float speed = 0.01f;

    bool firstframe = true;

    private void Awake()
    {
        Input.gyro.enabled = true;

    }

    void Start()
    {

    }

    private void OnEnable()
    {
        InitRotation();
    }

    void InitRotation()
    {
        rotRH = Input.gyro.attitude;
        Debug.Log(rotRH.GetType());
        firstrot_z = rotRH.z;
        water_value = 0;
        view_water.SetActive(true);
    }

    private void OnDisable()
    {
        view_water.SetActive(false);
    }

    void Update()
    {

        rotRH = Input.gyro.attitude;
        var rot = new Quaternion(0, 0, rotRH.z - firstrot_z, rotRH.w);

        transform.localRotation = rot;

        Quaternion rotation = this.transform.localRotation;
        Vector3 rotationAngles = rotation.eulerAngles;
        rotationAngles.z = rotationAngles.z - first;
        rotation = Quaternion.Euler(rotationAngles);

        if (firstframe)
        {
            Debug.Log(firstrot_z);
            Debug.Log(rotRH.z);
            Debug.Log(rotRH.z - firstrot_z);

            firstframe = false;
        }

        this.transform.localRotation = rotation;


        if (transform.localEulerAngles.z < 360f-min_z && transform.localEulerAngles.z > 360-max_z)
        {
            water.SetActive(true);
        }
        else
        {
            water.SetActive(false);
        }

        

        if (water.activeSelf)
        {
            water_value +=(int)(transform.localEulerAngles.z * speed);
        }
        
        //Debug.Log(rotRH.z - firstrot_z);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plant_image : MonoBehaviour
{
    [SerializeField] GameObject plant_Con;

    GameObject plant;

    SpriteRenderer plantIm;
    Image myImage;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in plant_Con.transform)
        {
            if (child.gameObject.activeSelf)
            {
                Debug.Log(child.gameObject.name);
                plantIm = child.GetComponent<SpriteRenderer>();

            }
        }

        myImage = GetComponent<Image>();

        Sprite sprite;

        sprite = plantIm.sprite;

        myImage.sprite = sprite;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        

        
    }
}

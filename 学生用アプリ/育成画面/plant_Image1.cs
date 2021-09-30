using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;
using UnityEngine.SceneManagement;

public class plant_Image1 : MonoBehaviour
{
    NCMBUser currentUser;
    private Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        currentUser = NCMBUser.CurrentUser;
        Image plant = GetComponent<Image>();

        int mode = System.Convert.ToInt32(currentUser["mode"]);

        if(mode == 1)
        {
            int seed = System.Convert.ToInt32(currentUser["seed"]);

            if(seed == 1)
            {
                sprite = Resources.Load<Sprite>("tomato/" + this.name);
            }
            else
            {
                sprite = Resources.Load<Sprite>("toumorokoshi/" + this.name);
            }

            plant.sprite = sprite;
        }
        else
        {
            int seed = System.Convert.ToInt32(currentUser["Pra_seed"]);

            if (seed == 1)
            {
                sprite = Resources.Load<Sprite>("tomato" + this.name);
            }
            else
            {
                sprite = Resources.Load<Sprite>("toumorokoshi" + this.name);
            }

            plant.sprite = sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

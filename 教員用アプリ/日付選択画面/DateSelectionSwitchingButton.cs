using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateSelectionSwitchingButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickPrev()
    {
        var script = transform.parent.GetComponent<DateSelection>();
        script.changePageTo(script.Page - 1);
    }

    public void onClickNext()
    {
        var script = transform.parent.GetComponent<DateSelection>();
        script.changePageTo(script.Page + 1);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorButton : MonoBehaviour
{
    public bool isRed;
    public bool isBlue;
    public bool isGreen;
    public bool isYellow;

    Color thisColor; 

    ColorManager colorMan;

    // Use this for initialization
    void Start ()
    {
        colorMan = FindObjectOfType(typeof(ColorManager)) as ColorManager;
        thisColor = this.GetComponent<Image>().color;
    }

    public void OnClick()
    {
        if (isRed)
        {
            colorMan.SetRedRGB(thisColor.r * 255, thisColor.g * 255, thisColor.b * 255);
            colorMan.ChangeSelectedRed(this.gameObject);
        }
        else if (isBlue)
        { 
            colorMan.SetBlueRGB(thisColor.r * 255, thisColor.g * 255, thisColor.b * 255);
            colorMan.ChangeSelectedBlue(this.gameObject);
        }
        else if (isGreen) 
        {
            colorMan.SetGreenRGB(thisColor.r * 255, thisColor.g * 255, thisColor.b * 255);
            colorMan.ChangeSelectedGreen(this.gameObject);
        }
        else if (isYellow) 
        {
            colorMan.SetYellowRGB(thisColor.r * 255, thisColor.g * 255, thisColor.b * 255);
            colorMan.ChangeSelectedYellow(this.gameObject);
        }
    }

}

using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using System.Collections.Generic;

public class ColorManager : MonoBehaviour
{
    public Color redRGB = new Color(223f, 39f, 39f, 1f);
    public Color blueRGB = new Color(51f, 51f, 255f, 1f);
    public Color greenRGB = new Color(0f, 235f, 0f, 1f);
    public Color yellowRGB = new Color(231f, 255f, 0f, 1f);

    [SerializeField]
    private GameObject objSelectedRed;
    [SerializeField]
    private GameObject objSelectedBlue;
    [SerializeField]
    private GameObject objSelectedGreen;
    [SerializeField]
    private GameObject objSelectedYellow;

    public Sprite imgSelected;
    public Sprite imgNotSelected;

    public Image[] arrImgColors;

    public GameObject EksColorSquareRed;
    public GameObject EksColorSquareBlue;
    public GameObject EksColorSquareGreen;
    public GameObject EksColorSquareYellow;

    // Use this for initialization
    void Start()
    {
        LoadColors();
    }

    private void LoadColors()
    {
        //Get Colors: 
        try
        {
            //Red
            string[] arrRedRGB = PlayerPrefs.GetString("ColorRed").Split(',');
            redRGB = new Color(float.Parse(arrRedRGB[0]) / 255, float.Parse(arrRedRGB[1]) / 255, float.Parse(arrRedRGB[2]) / 255, 1f);
            //Blue
            string[] arrBlueRGB = PlayerPrefs.GetString("ColorBlue").Split(',');
            blueRGB = new Color(float.Parse(arrBlueRGB[0]) / 255, float.Parse(arrBlueRGB[1]) / 255, float.Parse(arrBlueRGB[2]) / 255, 1f);
            //Green
            string[] arrGreenRGB = PlayerPrefs.GetString("ColorGreen").Split(',');
            greenRGB = new Color(float.Parse(arrGreenRGB[0]) / 255, float.Parse(arrGreenRGB[1]) / 255, float.Parse(arrGreenRGB[2]) / 255, 1f);
            //Yellow
            string[] arrYellowRGB = PlayerPrefs.GetString("ColorYellow").Split(',');
            yellowRGB = new Color(float.Parse(arrYellowRGB[0]) / 255, float.Parse(arrYellowRGB[1]) / 255, float.Parse(arrYellowRGB[2]) / 255, 1f);
        }
        catch
        {
            PlayerPrefs.SetString("ColorRed", "223,39,39");
            PlayerPrefs.SetString("ColorBlue", "51,51,255");
            PlayerPrefs.SetString("ColorGreen", "0,235,0");
            PlayerPrefs.SetString("ColorYellow", "231,255,0");
        }

        foreach (Image img in arrImgColors)
        {
            if (img.color == redRGB )
            {
                img.sprite = imgSelected;
                objSelectedRed = img.gameObject;
            }
            else if (img.color == blueRGB)
            {
                img.sprite = imgSelected;
                objSelectedBlue = img.gameObject;
            }
            else if (img.color == greenRGB)
            {
                img.sprite = imgSelected;
                objSelectedGreen = img.gameObject;
            }
            else if(img.color == yellowRGB)
            {
                img.sprite = imgSelected;
                objSelectedYellow = img.gameObject;
            } 

            else
            {
                img.sprite = imgNotSelected;
            }
        }

        EksColorSquareRed.GetComponent<Image>().color = redRGB;
        EksColorSquareBlue.GetComponent<Image>().color = blueRGB;
        EksColorSquareGreen.GetComponent<Image>().color = greenRGB;
        EksColorSquareYellow.GetComponent<Image>().color = yellowRGB;
    }

    public void SetRedRGB(float r, float g, float b)
    {
        PlayerPrefs.SetString("ColorRed", r + "," + g + "," + b);
        redRGB = new Color(r / 255, g / 255, b / 255);
        EksColorSquareRed.GetComponent<Image>().color = redRGB;
    }

    public void SetBlueRGB(float r, float g, float b)
    {
        PlayerPrefs.SetString("ColorBlue", r + "," + g + "," + b);
        blueRGB = new Color(r / 255, g / 255, b / 255);
        EksColorSquareBlue.GetComponent<Image>().color = blueRGB;
    }
    public void SetGreenRGB(float r, float g, float b)
    {
        PlayerPrefs.SetString("ColorGreen", r + "," + g + "," + b);
        greenRGB = new Color(r / 255, g / 255, b / 255);
        EksColorSquareGreen.GetComponent<Image>().color = greenRGB;
    }
    public void SetYellowRGB(float r, float g, float b)
    {
        PlayerPrefs.SetString("ColorYellow", r + "," + g + "," + b);
        yellowRGB = new Color(r / 255, g / 255, b / 255);
        EksColorSquareYellow.GetComponent<Image>().color = yellowRGB;
    }

    public void ChangeSelectedRed(GameObject newSelected)
    {
        objSelectedRed.GetComponent<Image>().sprite = imgNotSelected;
        objSelectedRed = newSelected;
        objSelectedRed.GetComponent<Image>().sprite = imgSelected;
    }

    public void ChangeSelectedBlue(GameObject newSelected)
    {
        objSelectedBlue.GetComponent<Image>().sprite = imgNotSelected;
        objSelectedBlue = newSelected;
        objSelectedBlue.GetComponent<Image>().sprite = imgSelected;
    }

    public void ChangeSelectedGreen(GameObject newSelected)
    {
        objSelectedGreen.GetComponent<Image>().sprite = imgNotSelected;
        objSelectedGreen = newSelected;
        objSelectedGreen.GetComponent<Image>().sprite = imgSelected;
    }

    public void ChangeSelectedYellow(GameObject newSelected)
    {
        objSelectedYellow.GetComponent<Image>().sprite = imgNotSelected;
        objSelectedYellow = newSelected;
        objSelectedYellow.GetComponent<Image>().sprite = imgSelected;
    }
}

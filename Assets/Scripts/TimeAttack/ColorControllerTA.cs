using UnityEngine;
using System.Collections;

public class ColorControllerTA : MonoBehaviour
{
    public Color redRGB = new Color(223f, 39f, 39f, 1f);
    public Color blueRGB = new Color(51f, 51f, 255f, 1f);
    public Color greenRGB = new Color(0f, 235f, 0f, 1f);
    public Color yellowRGB = new Color(231f, 255f, 0f, 1f);

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
            print("Error Getting Colors!"); //Denne fejl burde aldrig ske, da lle playerprefabs bliver sat i menu scenen, som altid bliver åbnet før denne scene!
        }
    }
}
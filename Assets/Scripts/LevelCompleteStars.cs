using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelCompleteStars : MonoBehaviour
{
    private GUILevelUI lvlUI;

    public int myStars;

    public CanvasRenderer star1;
    public CanvasRenderer star2;
    public CanvasRenderer star3;

    private int myLevel;

    public Text txtForground;
    public Text txtBackground;

    // Use this for initialization
    void Start()
    {
        //Get star info
        lvlUI = this.transform.root.GetComponent<GUILevelUI>();

        //Set star colors:
        try
        {
            star1.SetColor(lvlUI.star1.GetColor());
        }
        catch
        {
            print("error retriving star1 color");
        }
        try
        {
            star2.SetColor(lvlUI.star2.GetColor());
        }
        catch
        {
            print("error retriving star2 color");
        }
        try
        {
            star3.SetColor(lvlUI.star3.GetColor());
        }
        catch
        {
            print("error retriving star3 color");
        }


        //Hide Starbar!
        lvlUI.ShowStarBar(false);

        myLevel = PlayerPrefs.GetInt("Level");

        txtForground.text = "LEVEL " + myLevel;
        txtBackground.text = txtForground.text;

    }
}

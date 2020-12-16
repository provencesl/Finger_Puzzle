using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using UnityEngine.SceneManagement;


public class ButtonLevelSelect : MonoBehaviour
{
    public int myLevel; //Hvilken plads i listen over alle levels, denne kna level er! så level 1 = 0 og level 20 = 19 osv
    public int myStars;

    public CanvasRenderer star1;
    public CanvasRenderer star2;
    public CanvasRenderer star3;

    private MainMenuManager menMan;
        

    void Start()
    {
        menMan = FindObjectOfType(typeof(MainMenuManager)) as MainMenuManager;
        
    }
    public void GetStats(int lvl, int category)
    {
        myLevel = lvl;

        //code for loading stars
        string playerPrefs = "LevelData" + category;
        
        myStars = int.Parse(PlayerPrefs.GetString(playerPrefs).Split(',')[lvl - 1].ToString());

        ChangeStars();
    }

    public void OnClick()
    {
        PlayerPrefs.SetInt("Level", myLevel);
        menMan.PlayGame();
    }

    private void ChangeStars()
    {

        if (myStars == 3)
        {
            star1.SetColor(Color.yellow);
            star2.SetColor(Color.yellow);
            star3.SetColor(Color.yellow);
        }
        else if (myStars == 2)
        {
            star1.SetColor(Color.yellow);
            star2.SetColor(Color.yellow);
            star3.SetColor(Color.white);
        }
        else if (myStars == 1)
        {
            star1.SetColor(Color.yellow);
            star2.SetColor(Color.white);
            star3.SetColor(Color.white);
        }
        else
        {
            star1.SetColor(Color.white);
            star2.SetColor(Color.white);
            star3.SetColor(Color.white);
        }
    }

}

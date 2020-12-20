using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    Animator animatorShowOptions;

    private bool showSettings = false;

    private bool showLevelSelect = false;

    private bool showLevels = false;

    private bool showHighScore = false;
    private bool showLocalHighScore = true;

    public Text txtSelectedCategoryFront;
    public Text txtSelectedCategoryBack;

    private LoadAllLevels loadlvls;

    private HighscoreManager highScoreMan;

    public GameObject lvlContainer;

    // Use this for initialization
    void Start()
    {
        animatorShowOptions = GetComponent<Animator>();
        loadlvls = GetComponent<LoadAllLevels>();
        highScoreMan = this.transform.Find("HighScore").GetComponent<HighscoreManager>();

        //PlayerPrefs.DeleteAll();
    }


    public void PlayGame()
    {
        DontDestroyOnLoad(lvlContainer);
        SceneManager.LoadScene("Game");
    }

    public void LevelSelect()
    {
        //if (!showLevelSelect)
        //{
        //    showLevelSelect = true;
        //    animatorShowOptions.SetBool("ShowCategory", showLevelSelect);
        //}
        PlayGame();
    }

    public void CloseLevelSelect()
    {
        if (showLevelSelect)
        {
            showLevelSelect = false;
            animatorShowOptions.SetBool("ShowCategory", showLevelSelect);
        }
    }

    public void ShowCategories()
    {
        if (!showLevels)
        {
            showLevels = true;
            animatorShowOptions.SetBool("ShowAllLevels", showLevels);
        }
        loadlvls.PopulateList();
    }
    public void HideCategories()
    {
        if (showLevels)
        {
            showLevels = false;
            animatorShowOptions.SetBool("ShowAllLevels", showLevels);
        }
    }

    public void PlayTimeAttack()
    {

        SceneManager.LoadScene("TimeAttack");
    }

    public void HighScore()
    {
        if (!showHighScore)
        {
            showHighScore = true;
            animatorShowOptions.SetBool("ShowHighScore", showHighScore);
        }
    }

    public void CloseHighScore()
    {
        if (showHighScore)
        {
            showHighScore = false;
            animatorShowOptions.SetBool("ShowHighScore", showHighScore);
        }
    }

    public void HighScoreCategory()
    {
        showLocalHighScore = !showLocalHighScore;
        if (showLocalHighScore)
        {
            highScoreMan.LoadLocalHighScore();
        }
        else
        {
            highScoreMan.LoadOnlineHighScore();
        }
    }

    public void Settings()
    {
        if (!showSettings)
        {
            showSettings = true;
            animatorShowOptions.SetBool("ShowSettings", showSettings);
        }
    }

    public void CloseSettings()
    {
        if (showSettings)
        {
            showSettings = false;
            animatorShowOptions.SetBool("ShowSettings", showSettings);
        }
    }

    public void Sounds()
    {

    }

    #region Levels
    public void TutorialLvls()
    {
        txtSelectedCategoryFront.text = "Tutorial";
        txtSelectedCategoryBack.text = "Tutorial";

        PlayerPrefs.SetInt("LvlCategory", 0);
        ShowCategories();
    }
    public void EasyLvls()
    {
        txtSelectedCategoryFront.text = "Easy";
        txtSelectedCategoryBack.text = "Easy";

        PlayerPrefs.SetInt("LvlCategory", 1);
        ShowCategories();

    }
    public void MediumLvls()
    {
        txtSelectedCategoryFront.text = "Medium";
        txtSelectedCategoryBack.text = "Medium";

        PlayerPrefs.SetInt("LvlCategory", 2);
        ShowCategories();

    }
    public void HardLvls()
    {
        txtSelectedCategoryFront.text = "Hard";
        txtSelectedCategoryBack.text = "Hard";

        PlayerPrefs.SetInt("LvlCategory", 3);
        ShowCategories();

    }

    public void NextCategoryLvls() //Roterer mellem alle level kategorierne
    {
        int lvlCat = PlayerPrefs.GetInt("LvlCategory");

        switch (lvlCat)
        {
            case 0:
                PlayerPrefs.SetInt("LvlCategory", 1);
                EasyLvls();
                break;
            case 1:
                PlayerPrefs.SetInt("LvlCategory", 2);
                MediumLvls();
                break;
            case 2:
                PlayerPrefs.SetInt("LvlCategory", 3);
                HardLvls();
                break;
            case 3:
                PlayerPrefs.SetInt("LvlCategory", 0);
                TutorialLvls();
                break;
        }
    }
    #endregion

    public void Exit()
    {
        Application.Quit();
    }
}

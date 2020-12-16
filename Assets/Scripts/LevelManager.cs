using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    //connections needed for en stjerne vil skifte fra level til level!
    public int numberOfConnectionsFor1star = 2;
    public int numberOfConnectionsFor2Stars = 3;
    public int numberOfConnectionsFor3Stars = 4;

    public int currentConnections = 0;  //Hvor mange connections spilleren har lige nu!

    private TouchManager touchManager;
    [SerializeField]
    public bool isComplete = false;

    public List<GameObject> lstGameObjGoal;
    private int numberOfGoals;

    private GUILevelUI lvlUI;

    private DestroyMapEffect desMapEff;

    float lastStarBarUpdate = 0; //Hvilket tal vi sidst sendte til starbar methoden
    int lastStarUpdate = 0; //Hvilket tal vi sidst sendte til star methoden

    public GameObject levelCompletePanel;
    private Canvas sceneCanvas;
    private int currentStar = 0;


    // Use this for initialization
    void Start()
    {
        touchManager = FindObjectOfType(typeof(TouchManager)) as TouchManager;
        lvlUI = FindObjectOfType(typeof(GUILevelUI)) as GUILevelUI;
        desMapEff = FindObjectOfType(typeof(DestroyMapEffect)) as DestroyMapEffect;

        foreach (Transform figure in this.transform)
        {
            if (figure.tag == "Figure")
            {
                if (figure.GetComponent<gameObjInfo>().mySprite == 1) //Hvis det er et mål (B)
                {
                    lstGameObjGoal.Add(figure.gameObject);
                }
            }
        }


        sceneCanvas = (Canvas)FindObjectOfType(typeof(Canvas));

        numberOfGoals = lstGameObjGoal.Count;

        if (lvlUI != null)
        {
            lvlUI.ShowStarBar(true);
            lvlUI.UpdateStarRequirement(numberOfConnectionsFor1star, numberOfConnectionsFor2Stars, numberOfConnectionsFor3Stars);
        }
    }

    private int lastLstEF1Count = 0;
    private int lastLstEF2Count = 0;
    private int lastLstEF3Count = 0;

    public bool goal1Completed = false;
    public bool goal2Completed = false;
    public bool goal3Completed = false;

    // Update is called once per frame
    void Update()
    {

        currentConnections = touchManager.currLines;


        int goalCompleted = 0;  //Hvor mange mål spilleren har connected

        if (goal1Completed)
        {
            goalCompleted++;
        }
        if (goal2Completed)
        {
            goalCompleted++;
        }
        if (goal3Completed)
        {
            goalCompleted++;
        }

        #region count goal completed
        //Check om alle objects i lstGameObjGoal, er connected!

        if (touchManager.lstEndFigure1.Count > lastLstEF1Count)
        {
            foreach (GameObject figure in touchManager.lstEndFigure1)
            {
                if (lstGameObjGoal.Contains(figure))
                {
                    goalCompleted++;
                    goal1Completed = true;
                }
            }

            lastLstEF1Count = touchManager.lstEndFigure1.Count;

        }
        else if (touchManager.lstEndFigure1.Count < lastLstEF1Count)
        {
            goal1Completed = false;
            lastLstEF1Count = touchManager.lstEndFigure1.Count;
        }

        if (touchManager.lstEndFigure2.Count > lastLstEF2Count)
        {
            foreach (GameObject figure in touchManager.lstEndFigure2)
            {
                if (lstGameObjGoal.Contains(figure))
                {
                    goalCompleted++;
                    goal2Completed = true;
                }
            }

            lastLstEF2Count = touchManager.lstEndFigure2.Count;

        }
        else if (touchManager.lstEndFigure2.Count < lastLstEF2Count)
        {
            goal2Completed = false;
            lastLstEF2Count = touchManager.lstEndFigure2.Count;
        }


        if (touchManager.lstEndFigure3.Count > lastLstEF3Count)
        {
            foreach (GameObject figure in touchManager.lstEndFigure3)
            {
                if (lstGameObjGoal.Contains(figure))
                {
                    goalCompleted++;
                    goal3Completed = true;
                }
            }

            lastLstEF3Count = touchManager.lstEndFigure3.Count;

        }
        else if (touchManager.lstEndFigure3.Count < lastLstEF3Count)
        {
            goal3Completed = false;
            lastLstEF3Count = touchManager.lstEndFigure3.Count;
        }

        #endregion


        //Update Starbar
        UpdateStarBar();

        //Update stars:
        UpdateStars();

        if (numberOfGoals >= 1 && goalCompleted == numberOfGoals && currentConnections >= numberOfConnectionsFor1star)
        {

            //GG du vandt!
            if (!isComplete)
            {
                try
                {
                    desMapEff.DestroyMap();

                    SaveLevelData();

                }
                catch
                {
                    print("Der skete en fejl ved lvl complete, eller du er i level editoren :P");
                }
            }

            //Set level til complete!
            isComplete = true;
            touchManager.isCompleted = isComplete;
        }
        else
        {
            isComplete = false;
            touchManager.isCompleted = isComplete;
        }
    }

    private void SaveLevelData()
    {
        int category = PlayerPrefs.GetInt("LvlCategory");
        int currLevel = PlayerPrefs.GetInt("Level");

        string[] oldSave = PlayerPrefs.GetString("LevelData" + category).Split(',');

        if (int.Parse(oldSave[currLevel - 1]) < currentStar)
        {
            oldSave[currLevel - 1] = currentStar.ToString();
        }
        string save = oldSave[0];
        for (int i = 1; i < oldSave.Length; i++)
        {
            save += "," + oldSave[i];
        }

        PlayerPrefs.SetString("LevelData" + category, save);
    }

    void UpdateStarBar()
    {
        if (currentConnections != lastStarBarUpdate)
        {
            lastStarBarUpdate = currentConnections;
            if (lvlUI != null) //For at fjerne error hell fra lvl editor :P
            {
                lvlUI.StarBarStats(currentConnections);
            }
        }
    }

    void UpdateStars()
    {
        currentStar = 0;

        if (currentConnections >= numberOfConnectionsFor3Stars)
        {
            currentStar = 3;
        }
        else if (currentConnections >= numberOfConnectionsFor2Stars)
        {
            currentStar = 2;
        }
        else if (currentConnections >= numberOfConnectionsFor1star)
        {
            currentStar = 1;
        }
        else
        {
            currentStar = 0;
        }

        if (currentStar != lastStarUpdate)
        {
            lastStarUpdate = currentStar;
            if (lvlUI != null) //For at fjerne error hell fra lvl editor :P
            {
                lvlUI.ShowNumberOfStars(currentStar);
            }
        }
    }

    public void ShowLevelComplete()
    {
        GameObject objLvLCompletePanel = Instantiate(levelCompletePanel) as GameObject;
        objLvLCompletePanel.transform.SetParent(sceneCanvas.transform, false);
    }

}

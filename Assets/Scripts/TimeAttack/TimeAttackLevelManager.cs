using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeAttackLevelManager : MonoBehaviour 
{

    //connections needed for en stjerne vil skifte fra level til level!
    public int numberOfConnectionsFor1star = 2;

    public int currentConnections = 0;  //Hvor mange connections spilleren har lige nu!
    //Skal koble currentConnections sammen med currLines i TouchManager!

    private TouchManager touchManager;
    private TimeAttackLevelCreator tALvlCreator;
    [SerializeField]
    public bool isComplete = false;

    [SerializeField]
    private List<GameObject> lstGameObjGoal = new List<GameObject>();
    private int numberOfGoals;
    
    private TimeAttackScoreManager taScoreMan;
    private TimeAttackGUILevelUI taLvlUI;

    private DestroyMapEffect desMapEff;

    float lastStarBarUpdate = 0; //Hvilket tal vi sidst sendte til starbar methoden
    int lastStarUpdate = 0; //Hvilket tal vi sidst sendte til star methoden

    private Canvas sceneCanvas;
    private int currentStar = 0;

    // Use this for initialization
    void Start()
    {
        touchManager = FindObjectOfType(typeof(TouchManager)) as TouchManager;
        taScoreMan = FindObjectOfType(typeof(TimeAttackScoreManager)) as TimeAttackScoreManager;
        tALvlCreator = FindObjectOfType(typeof(TimeAttackLevelCreator)) as TimeAttackLevelCreator;
        taLvlUI = FindObjectOfType(typeof(TimeAttackGUILevelUI)) as TimeAttackGUILevelUI;
        desMapEff = FindObjectOfType(typeof(DestroyMapEffect)) as DestroyMapEffect;


        sceneCanvas = (Canvas)FindObjectOfType(typeof(Canvas));


       StartCoroutine(taLvlUI.CountDown());

       if (taLvlUI != null)
       {
           taLvlUI.ShowStarBar(true);
           taLvlUI.UpdateStarRequirement(numberOfConnectionsFor1star, tALvlCreator.lvlSize - 1); //minus 1 fordi start ikke tæller med!
       }
    }

    public IEnumerator UpdateGoals() //Skal vente 1 frame, fordi unity venter 1 frame med at delete objects, og det sucks at have missing obj's :P
    {
        yield return new WaitForSeconds(.1f);

        if (lstGameObjGoal.Count != 0)
        {
            lstGameObjGoal.Clear();
            numberOfGoals = 0;
        }

        foreach (GameObject figure in GameObject.FindGameObjectsWithTag("Figure"))
        {
            if (figure.GetComponent<gameObjInfo>().mySprite == 1) //Hvis det er et mål (B)
            {
                lstGameObjGoal.Add(figure);
            }
        }

        numberOfGoals = lstGameObjGoal.Count;
    }

    
    void Update()
    {
        currentConnections = touchManager.currLines;

        int goalCompleted = 0;  //Hvor mange mål spilleren har connected
        //Check om alle objects i lstGameObjGoal, er connected!
        foreach (GameObject figure in touchManager.lstEndFigure)
        {
            if (lstGameObjGoal.Contains(figure))
            {
                goalCompleted++;
            }
        }
        
        //Update Starbar
        UpdateStarBar();
        
        //Update stars:
        UpdateStars();

        if (goalCompleted == numberOfGoals && currentConnections >= numberOfConnectionsFor1star - 1)  
        {
            //GG du vandt!
            if (!isComplete)
            {
                //Pause timer:
                taLvlUI.isPaused = true;

                //Nyt lvl:
                try
                {
                    desMapEff.DestroyMap();

                    //Create new level!
                    //touchManager.RemoveAllLines();
                    isComplete = false;
                    touchManager.isCompleted = isComplete;
                    currentConnections = 0;
                    numberOfConnectionsFor1star = tALvlCreator.routeDistance;
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

    public void CreateNextLevel()
    {
        tALvlCreator.CreateNewLevel();

        taLvlUI.UpdateStarRequirement(numberOfConnectionsFor1star, tALvlCreator.lvlSize - 1); //minus 1 fordi start ikke tæller med!

        //Start timer:
        taLvlUI.isPaused = false;
    }

    void UpdateStarBar()
    {
        if (currentConnections != lastStarBarUpdate)
        {
            lastStarBarUpdate = currentConnections;
            if (taLvlUI != null) //For at fjerne error hell fra lvl editor :P
            {
                taLvlUI.StarBarStats(currentConnections);
            }
        }
    }

    void UpdateStars()
    {
        currentStar = 0;

        if (currentConnections >= numberOfConnectionsFor1star - 1) //er minus 1 så vi får star effect når spilleren har nok con til at kunne connect med mål!
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
            if (taLvlUI != null) //For at fjerne error hell fra lvl editor :P
            {
                taLvlUI.ShowNumberOfStars(currentStar);
            }
        }
    }

    void OnDestroy()
    {
        try
        {
            taLvlUI.ResetStarBar();
        }
        catch
        {

        }
    }

}

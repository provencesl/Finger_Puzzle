using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeAttackLevelCreator : MonoBehaviour
{

    public GameObject objFigur;
    public GameObject objBackgroundBlock;
    public GameObject objBackgroundBlockStartGoal;
    public GameObject objBackgroundBlockBlank;

    //Hvor mange maps vi har klaret!
    public int lvlsCompleted = -1;
    public int lvlSize = 9;

    public int[,] lvlGrid = new int[5, 9];


    //Den nye start, skal være på det forrige måls plads!
    public int lastGoalPositionX = 4;
    public int lastGoalPositionY = 2;

    //X og Y positionen over hvor vi er!
    private int currX = 4;
    private int currY = 2;
    //X og Y over den nyeste start (bruges kun ved rute fejl!)
    private int currStartX = 4;
    private int currStartY = 2;

    //Hvor mange figur, et mål som minimum skal være væk fra start!
    public int minimumGoalStartDistance = 2;

    //Hvor lang den route som altid skal kunne klares er:
    public int routeDistance = 2;
    
    //klassen der indeholder methoderne til at skabe gridet over mappet:
    private TimeAttackGridCreator taGridCreator;

    //Klassen der inheholder methoderne til at lave background lines:
    private BackgroundLineCreator bgLineCreator;

    //Skal bruges til at update goal info!
    private TimeAttackLevelManager taLvlMan;

    //Skal bruges til at lave stien gennem mappet:
    TimeAttackPathFinder taPathFinder;

    public List<string> lstGameRoute = new List<string>();

    // Use this for initialization
    void Start()
    {
        taGridCreator = FindObjectOfType(typeof(TimeAttackGridCreator)) as TimeAttackGridCreator;
        bgLineCreator = FindObjectOfType(typeof(BackgroundLineCreator)) as BackgroundLineCreator;
        taLvlMan = FindObjectOfType(typeof(TimeAttackLevelManager)) as TimeAttackLevelManager;
        taPathFinder = FindObjectOfType(typeof(TimeAttackPathFinder)) as TimeAttackPathFinder;
        CreateNewLevel();
    }


    /// <summary>
    /// -1 = empty
    /// 0 = udenfor map
    /// 1 = Start
    /// 2 = Goal
    /// 3 = En figur som skal danne en vej fra start til slut (en garanti for at det nye lvl kan klares)
    /// 4 = rand figur/farve
    /// 5 = en del af mappet, som endnu ikke har fået tildelt en rolle!
    /// 6 = en del af mappet, som endnu ikke har fået tildelt en rolle, men som ikke kan være goal!
    /// 7 = en del af mappet, som endnu ikke har fået tildelt en rolle, men som kan bruges til at danne en route fra a til b
    /// </summary>
    public void CreateNewLevel()
    {
        lvlsCompleted += 1;

        //Delete Old lvl:
        DeleteOldLevel();

        //Set currX/Y til starts plads!
        currX = lastGoalPositionX;
        currY = lastGoalPositionY;

        currStartX = lastGoalPositionX;
        currStartY = lastGoalPositionY;

        //Hent grid over mapform/size... 5 & 6 = map
        lvlGrid = taGridCreator.CreateLvlGrid(lvlSize, lastGoalPositionX, lastGoalPositionY, minimumGoalStartDistance);

        //Set start:
        lvlGrid[lastGoalPositionY, lastGoalPositionX] = 1;

        
        //Dan route
        lvlGrid = taPathFinder.CreateRoute(lvlGrid, routeDistance, lastGoalPositionX, lastGoalPositionY);


        //Spawn the map:
        SpawnMap();

        //Spawn the background lines:
        bgLineCreator.CreateBackgrounds();

        //Gør klar til næste map:
        if (lvlSize < 25)
        {
            lvlSize++;
        }
        routeDistance = 1 + int.Parse(Mathf.Round(lvlSize / 4).ToString()); //efter hånden som mappet bliver størrer, skal ruten også blive det
        minimumGoalStartDistance = 1 + int.Parse(Mathf.Round(routeDistance / 3).ToString());

        if (lvlsCompleted > 30)
        {
            routeDistance += 3;
        }
        else if (lvlsCompleted > 20)
        {
            routeDistance += 2;
        }
        else if (lvlsCompleted > 10)
        {
            routeDistance++;
        }
    }

    private void DeleteOldLevel()
    {
        //Slet backgrounds:
        foreach (Transform backObj in this.transform.Find("Backgrounds").transform)
        {
            GameObject.Destroy(backObj.gameObject);
        }

        //Slet alle figur
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Figure"))
        {
            GameObject.Destroy(obj);
        }
    }


    /// <summary>
    /// -1 = empty
    /// 0 = udenfor map
    /// 1 = Start
    /// 2 = Goal
    /// 3 = En figur som skal danne en vej fra start til slut (en garanti for at det nye lvl kan klares)
    /// 4 = rand figur/farve
    /// </summary>
    /// 
    private void SpawnMap()
    {
        SpawnRoute();

        int[] arrColors = new int[4]; //Skal bruges til ikke at have for mange af samme farve i mappet!
        int[] arrFigures = new int[4];

        for (int y = 0; y < lvlGrid.GetLength(0); y++)
        {
            for (int x = 0; x < lvlGrid.GetLength(1); x++)
            {
                if (lvlGrid[y, x] == 1) //1 = Start
                {
                    GameObject newFigure = (GameObject)Instantiate(objFigur);
                    newFigure.transform.SetParent(this.transform);


                    newFigure.GetComponent<gameObjInfo>().x = x - 4;
                    newFigure.GetComponent<gameObjInfo>().y = y - 2;

                    newFigure.GetComponent<gameObjInfo>().mySprite = 0;

                    //Spawn BackgroundBlock
                    GameObject newBackgroundBlock = (GameObject)Instantiate(objBackgroundBlockStartGoal);
                    newBackgroundBlock.transform.position = new Vector3((x - 4) * 1.6f, (y - 2) * 1.6f, 1);
                    newBackgroundBlock.transform.SetParent(GameObject.Find("Backgrounds").transform);
                }
                else if (lvlGrid[y, x] == 2) //2 = Goal
                {
                    GameObject newFigure = (GameObject)Instantiate(objFigur);
                    newFigure.transform.SetParent(this.transform);


                    newFigure.GetComponent<gameObjInfo>().x = x - 4;
                    newFigure.GetComponent<gameObjInfo>().y = y - 2;

                    newFigure.GetComponent<gameObjInfo>().mySprite = 1;

                    //Spawn BackgroundBlock
                    GameObject newBackgroundBlock = (GameObject)Instantiate(objBackgroundBlockStartGoal);
                    newBackgroundBlock.transform.position = new Vector3((x - 4) * 1.6f, (y - 2) * 1.6f, 1);
                    newBackgroundBlock.transform.SetParent(GameObject.Find("Backgrounds").transform);
                }
                else if (lvlGrid[y, x] >= 4) //4+ = rand figur/farve
                {

                        GameObject newFigure = (GameObject)Instantiate(objFigur);
                        newFigure.transform.SetParent(this.transform);

                        newFigure.GetComponent<gameObjInfo>().x = x - 4;
                        newFigure.GetComponent<gameObjInfo>().y = y - 2;

                    //Spawn mappet uden for mange af den samme farve:
                    int color = Random.Range(1, 5);
                        
                    if (arrColors[color - 1] < 5)
                    {
                        newFigure.GetComponent<gameObjInfo>().myColor = color;

                        arrColors[color - 1]++;
                    }
                    else
                    {
                        if (arrColors[0] < 5)
                        {
                            newFigure.GetComponent<gameObjInfo>().myColor = 1;

                            arrColors[0]++;
                        }
                        else if (arrColors[1] < 5)
                        {
                            newFigure.GetComponent<gameObjInfo>().myColor = 2;

                            arrColors[1]++;
                        }
                        else if (arrColors[2] < 5)
                        {
                            newFigure.GetComponent<gameObjInfo>().myColor = 3;

                            arrColors[2]++;
                        }
                        else
                        {
                            newFigure.GetComponent<gameObjInfo>().myColor = 4;

                            arrColors[3]++;
                        }
                    }

                    //Spawn mappet uden for mange af den samme figur:
                    int figur = Random.Range(2, 6);

                    if (arrFigures[figur - 2] < 5)
                    {
                        newFigure.GetComponent<gameObjInfo>().mySprite = figur;

                        arrFigures[figur - 2]++;
                    }
                    else
                    {
                        if (arrFigures[0] < 5)
                        {
                            newFigure.GetComponent<gameObjInfo>().mySprite = 2;

                            arrFigures[0]++;
                        }
                        else if (arrFigures[1] < 5)
                        {
                            newFigure.GetComponent<gameObjInfo>().mySprite = 3;

                            arrFigures[1]++;
                        }
                        else if (arrFigures[2] < 5)
                        {
                            newFigure.GetComponent<gameObjInfo>().mySprite = 4;

                            arrFigures[2]++;
                        }
                        else
                        {
                            newFigure.GetComponent<gameObjInfo>().mySprite = 5;

                            arrFigures[3]++;
                        }
                    }

                        //Spawn BackgroundBlock
                        GameObject newBackgroundBlock = (GameObject)Instantiate(objBackgroundBlock);
                        newBackgroundBlock.transform.position = new Vector3((x - 4) * 1.6f, (y - 2) * 1.6f, 1);
                        newBackgroundBlock.transform.SetParent(GameObject.Find("Backgrounds").transform);

                }
                else if (lvlGrid[y, x] == -1)
                    {
                                                //Spawn BackgroundBlock
                        GameObject newBackgroundBlock = (GameObject)Instantiate(objBackgroundBlockBlank);
                        newBackgroundBlock.transform.position = new Vector3((x - 4) * 1.6f, (y - 2) * 1.6f, 1);
                        newBackgroundBlock.transform.SetParent(GameObject.Find("Backgrounds").transform);

                    }

            }
        }

        StartCoroutine(taLvlMan.UpdateGoals());
    }

    private void SpawnRoute()
    {
        int lastColor = 1;
        int lastSprite = 2;

        int lastRoll = -1;
        int timesOfSameRoll = 0;

        for (int i = 0; i < lstGameRoute.Count; i++)
        {
            GameObject newFigure = (GameObject)Instantiate(objFigur);
            newFigure.transform.SetParent(this.transform);


            newFigure.GetComponent<gameObjInfo>().x = int.Parse(lstGameRoute[i].Split(',')[1]) - 4;
            newFigure.GetComponent<gameObjInfo>().y = int.Parse(lstGameRoute[i].Split(',')[0]) - 2;

            int SpriteOrColor = Random.Range(0, 2);

            //Gør så man højest kan roll det samme 3 gange i streg!
            if (timesOfSameRoll == 2)
            {
                if (SpriteOrColor == 0)
                {
                    SpriteOrColor = 1;
                    timesOfSameRoll = 1;
                }
                else
                {
                    SpriteOrColor = 0;
                    timesOfSameRoll = 1;
                }
            }
            else if (lastRoll == SpriteOrColor)
            {
                timesOfSameRoll++;
            }
            else
            {
                timesOfSameRoll = 1;
            }
            
            lastRoll = SpriteOrColor;

            if (SpriteOrColor == 0)
            {
                //Set Sprite:
                newFigure.GetComponent<gameObjInfo>().mySprite = lastSprite;
                
                //Set Color
                int nextColor = Random.Range(1, 5);
                if (nextColor == lastColor)
                {
                    if (nextColor < 4)
                    {
                        nextColor++;
                    }
                    else
                    {
                        nextColor--;
                    }
                }
                lastColor = nextColor;
                newFigure.GetComponent<gameObjInfo>().myColor = nextColor;
            }
            else
            {
                //Set Color
                newFigure.GetComponent<gameObjInfo>().myColor = lastColor;
               
                //Set Sprite:
                int nextSprite = Random.Range(2, 6);

                if (lastSprite == nextSprite)
                {
                    if (nextSprite < 5)
                    {
                        nextSprite++;
                    }
                    else
                    {
                        nextSprite--;
                    }
                }
                lastSprite = nextSprite;
                newFigure.GetComponent<gameObjInfo>().mySprite = nextSprite;
            }

            //Spawn BackgroundBlock
            GameObject newBackgroundBlock = (GameObject)Instantiate(objBackgroundBlock);
            newBackgroundBlock.transform.position = new Vector3((int.Parse(lstGameRoute[i].Split(',')[1]) - 4) * 1.6f, (int.Parse(lstGameRoute[i].Split(',')[0]) - 2) * 1.6f, 1);
            newBackgroundBlock.transform.SetParent(GameObject.Find("Backgrounds").transform);


        }
    }
}
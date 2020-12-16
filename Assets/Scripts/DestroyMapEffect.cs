using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyMapEffect : MonoBehaviour
{
    public float speed = 13f;
    public bool isDestroying = false;
    public bool isTesting = false;

    private int[,] lvlGrid = new int[5, 9];

    TouchManager touchMan;
    LevelManager lvlMan;
    TimeAttackLevelManager taLvlMan;

    [SerializeField]
    private int index = 0;
    private int index2 = 0;
    private int index3 = 0;

    [SerializeField]
    private int waitForfigDes = 0;
    public int completedFigDes = 0;


    [SerializeField]
    bool isDoneWithLine1 = false;
    private GameObject currentLine1;
    [SerializeField]
    private Vector3 currentLine1Start; //LineRenderer har ikke en Get position, så vi skaber vores egen ud fra de 2 figure linjen er mellem!
    [SerializeField]
    private Vector3 currentLine1End;


    [SerializeField]
    bool isDoneWithLine2 = false;
    private GameObject currentLine2;
    [SerializeField]
    private Vector3 currentLine2Start;
    [SerializeField]
    private Vector3 currentLine2End;


    [SerializeField]
    bool isDoneWithLine3 = false;
    private GameObject currentLine3;
    [SerializeField]
    private Vector3 currentLine3Start;
    [SerializeField]
    private Vector3 currentLine3End;

    //Score:
    public GameObject objPlusScore;

    //Time
    private TimeAttackGUILevelUI taGUIlvlUI;

    //Level info (is the game mode timeattack?)
    private LevelInfo lvlInf;

    // Use this for initialization
    void Start()
    {
        touchMan = FindObjectOfType(typeof(TouchManager)) as TouchManager;
        lvlMan = FindObjectOfType(typeof(LevelManager)) as LevelManager;
        taLvlMan = FindObjectOfType(typeof(TimeAttackLevelManager)) as TimeAttackLevelManager;
        taGUIlvlUI = FindObjectOfType(typeof(TimeAttackGUILevelUI)) as TimeAttackGUILevelUI;
        lvlInf = FindObjectOfType(typeof(LevelInfo)) as LevelInfo;

    }

    void Update()
    {
        if (isTesting)
        {
            DestroyMap();
            isTesting = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDestroying)
        {

            //Line && Figure 1
            if (!isDoneWithLine1)
            {
                if (Mathf.Abs(currentLine1Start.x - currentLine1End.x) > 0.13f || Mathf.Abs(currentLine1Start.y - currentLine1End.y) > 0.13f)
                {
                    float x = Mathf.Lerp(currentLine1Start.x, currentLine1End.x, Time.deltaTime * speed);
                    float y = Mathf.Lerp(currentLine1Start.y, currentLine1End.y, Time.deltaTime * speed);
                    currentLine1Start = new Vector3(x, y, 0);

                    currentLine1.GetComponent<LineRenderer>().SetPosition(0, new Vector3(x, y, 0));
                }
                else
                {
                    Destroy(currentLine1);
                    index++;

                    if (touchMan.lstStartFigure1.Count > index)
                    {
                        touchMan.lstStartFigure1[index].GetComponent<DestroyFigureEffect>().StartDestroyEffect(index);
                    }


                    if (touchMan.lstGameLines1.Count > index)
                    {
                        currentLine1 = touchMan.lstGameLines1[index];
                        currentLine1Start = new Vector3(touchMan.lstStartFigure1[index].transform.position.x, touchMan.lstStartFigure1[index].transform.position.y, 0);

                        if (touchMan.lstStartFigure1.Count > index + 1)
                        {
                            currentLine1End = new Vector3(touchMan.lstStartFigure1[index + 1].transform.position.x, touchMan.lstStartFigure1[index + 1].transform.position.y, 0);
                        }
                        else
                        {
                            currentLine1End = new Vector3(touchMan.lstEndFigure1[index].transform.position.x, touchMan.lstEndFigure1[index].transform.position.y, 0);
                        }
                    }
                    else
                    {
                        if (lvlInf.isTimeAttack)
                        {
                            GameObject newPlustScore = GameObject.Instantiate(objPlusScore, touchMan.lstEndFigure1[touchMan.lstEndFigure1.Count - 1].transform.position, Quaternion.identity) as GameObject;

                            newPlustScore.GetComponent<PlusScoreEffect>().displayNumber = 200; //200 for at klare banen!
                            taGUIlvlUI.AddMoreTime(5);
                        }
                        isDoneWithLine1 = true;
                    }
                }
            }


            //Line && Figure 2
            if (!isDoneWithLine2)
            {
                if (Mathf.Abs(currentLine2Start.x - currentLine2End.x) > 0.13f || Mathf.Abs(currentLine2Start.y - currentLine2End.y) > 0.13f)
                {
                    float x = Mathf.Lerp(currentLine2Start.x, currentLine2End.x, Time.deltaTime * speed);
                    float y = Mathf.Lerp(currentLine2Start.y, currentLine2End.y, Time.deltaTime * speed);
                    currentLine2Start = new Vector3(x, y, 0);

                    currentLine2.GetComponent<LineRenderer>().SetPosition(0, new Vector3(x, y, 0));
                }
                else
                {
                    Destroy(currentLine2);
                    index2++;

                    if (touchMan.lstStartFigure2.Count > index2)
                    {
                        touchMan.lstStartFigure2[index2].GetComponent<DestroyFigureEffect>().StartDestroyEffect(index2);
                    }


                    if (touchMan.lstGameLines2.Count > index2)
                    {
                        currentLine2 = touchMan.lstGameLines2[index2];
                        currentLine2Start = new Vector3(touchMan.lstStartFigure2[index2].transform.position.x, touchMan.lstStartFigure2[index2].transform.position.y, 0);

                        if (touchMan.lstStartFigure2.Count > index2 + 1)
                        {
                            currentLine2End = new Vector3(touchMan.lstStartFigure2[index2 + 1].transform.position.x, touchMan.lstStartFigure2[index2 + 1].transform.position.y, 0);
                        }
                        else
                        {
                            currentLine2End = new Vector3(touchMan.lstEndFigure2[index2].transform.position.x, touchMan.lstEndFigure2[index2].transform.position.y, 0);
                        }
                    }
                    else
                    {
                        if (lvlInf.isTimeAttack)
                        {
                            GameObject newPlustScore = GameObject.Instantiate(objPlusScore, touchMan.lstEndFigure2[touchMan.lstEndFigure2.Count - 1].transform.position, Quaternion.identity) as GameObject;

                            newPlustScore.GetComponent<PlusScoreEffect>().displayNumber = 200; //200 for at klare banen!
                            taGUIlvlUI.AddMoreTime(5);
                        }
                        isDoneWithLine2 = true;
                    }
                }
            }


            //Line && Figure 3
            if (!isDoneWithLine3)
            {
                if (Mathf.Abs(currentLine3Start.x - currentLine3End.x) > 0.13f || Mathf.Abs(currentLine3Start.y - currentLine3End.y) > 0.13f)
                {
                    float x = Mathf.Lerp(currentLine3Start.x, currentLine3End.x, Time.deltaTime * speed);
                    float y = Mathf.Lerp(currentLine3Start.y, currentLine3End.y, Time.deltaTime * speed);
                    currentLine3Start = new Vector3(x, y, 0);

                    currentLine3.GetComponent<LineRenderer>().SetPosition(0, new Vector3(x, y, 0));
                }
                else
                {
                    Destroy(currentLine3);
                    index3++;

                    if (touchMan.lstStartFigure3.Count > index3)
                    {
                        touchMan.lstStartFigure3[index3].GetComponent<DestroyFigureEffect>().StartDestroyEffect(index3);
                    }


                    if (touchMan.lstGameLines3.Count > index3)
                    {
                        currentLine3 = touchMan.lstGameLines3[index3];
                        currentLine3Start = new Vector3(touchMan.lstStartFigure3[index3].transform.position.x, touchMan.lstStartFigure3[index3].transform.position.y, 0);

                        if (touchMan.lstStartFigure3.Count > index3 + 1)
                        {
                            currentLine3End = new Vector3(touchMan.lstStartFigure3[index3 + 1].transform.position.x, touchMan.lstStartFigure3[index3 + 1].transform.position.y, 0);
                        }
                        else
                        {
                            currentLine1End = new Vector3(touchMan.lstEndFigure3[index3].transform.position.x, touchMan.lstEndFigure3[index3].transform.position.y, 0);
                        }
                    }
                    else
                    {
                        if (lvlInf.isTimeAttack)
                        {
                            GameObject newPlustScore = GameObject.Instantiate(objPlusScore, touchMan.lstEndFigure3[touchMan.lstEndFigure3.Count - 1].transform.position, Quaternion.identity) as GameObject;

                            newPlustScore.GetComponent<PlusScoreEffect>().displayNumber = 200; //200 for at klare banen!
                            taGUIlvlUI.AddMoreTime(5);
                        }
                        isDoneWithLine3 = true;
                    }
                }
            }

            //Check if done!
            if (isDoneWithLine1 && isDoneWithLine2 && isDoneWithLine3 && completedFigDes == waitForfigDes)
            {
                isDestroying = false;
                touchMan.ClearAllLists();

                if (lvlInf.isTimeAttack)
                {
                    taLvlMan.CreateNextLevel();
                }
                else
                {
                    lvlMan.ShowLevelComplete();
                }
            }
        }
    }

    public void DestroyMap()
    {
        completedFigDes = 0;
        waitForfigDes = 0;
        isDoneWithLine1 = false;
        isDoneWithLine2 = false;
        isDoneWithLine3 = false;
        index = 0;
        index2 = 0;
        index3 = 0;

        if (touchMan.lstGameLines1.Count != 0)
        {
            currentLine1 = touchMan.lstGameLines1[index];
            currentLine1Start = new Vector3(touchMan.lstStartFigure1[index].transform.position.x, touchMan.lstStartFigure1[index].transform.position.y, 0);
            currentLine1End = new Vector3(touchMan.lstStartFigure1[index + 1].transform.position.x, touchMan.lstStartFigure1[index + 1].transform.position.y, 0);
        }
        else
        {
            isDoneWithLine1 = true;
        }

        if (touchMan.lstGameLines2.Count != 0)
        {
            currentLine2 = touchMan.lstGameLines2[index2];
            currentLine2Start = new Vector3(touchMan.lstStartFigure2[index2].transform.position.x, touchMan.lstStartFigure2[index2].transform.position.y, 0);
            currentLine2End = new Vector3(touchMan.lstStartFigure2[index2 + 1].transform.position.x, touchMan.lstStartFigure2[index2 + 1].transform.position.y, 0);
        }
        else
        {
            isDoneWithLine2 = true;
        }

        if (touchMan.lstGameLines3.Count != 0)
        {
            currentLine3 = touchMan.lstGameLines3[index3];
            currentLine3Start = new Vector3(touchMan.lstStartFigure3[index3].transform.position.x, touchMan.lstStartFigure3[index3].transform.position.y, 0);
            currentLine3End = new Vector3(touchMan.lstStartFigure3[index3 + 1].transform.position.x, touchMan.lstStartFigure3[index3 + 1].transform.position.y, 0);
        }
        else 
        {
            isDoneWithLine3 = true;
        }

        //Slet backgrounds:
        foreach (GameObject backObj in GameObject.FindGameObjectsWithTag("BackgroundLine"))
        {
            GameObject.Destroy(backObj);
        }

       

        //Calc wait for figure to destroy
        if (touchMan.lstStartFigure1.Count > 1)
        {
            int lstCount = touchMan.lstStartFigure1.Count;

            if (touchMan.lstStartFigure1[lstCount - 1].GetComponent<gameObjInfo>().mySprite == 1) //Hvis den sidste figur er et mål! (sker kun ved den linje man "tegner" sidst.)
            {
                lstCount -= 2;
            }
            else
            {
                lstCount -= 1;
            }

            waitForfigDes += lstCount;
        }
        if (touchMan.lstStartFigure2.Count > 1)
        {
            int lstCount = touchMan.lstStartFigure2.Count;

            if (touchMan.lstStartFigure2[lstCount - 1].GetComponent<gameObjInfo>().mySprite == 1) //Hvis den sidste figur er et mål! (sker kun ved den linje man "tegner" sidst.)
            {
                lstCount -= 2;
            }
            else
            {
                lstCount -= 1;
            }

            waitForfigDes += lstCount;
        }
        if (touchMan.lstStartFigure3.Count > 1)
        {
            int lstCount = touchMan.lstStartFigure3.Count;

            if (touchMan.lstStartFigure3[lstCount - 1].GetComponent<gameObjInfo>().mySprite == 1) //Hvis den sidste figur er et mål! (sker kun ved den linje man "tegner" sidst.)
            {
                lstCount -= 2;
            }
            else
            {
                lstCount -= 1;
            }

            waitForfigDes += lstCount;
        }

        isDestroying = true;
    }


}

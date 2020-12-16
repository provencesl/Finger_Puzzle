using UnityEngine;
using System.Collections;

public class DestroyFigureEffect : MonoBehaviour
{
    public bool destroyFigure;
    private bool expandFigure = true;

    private float expandSize = 1.6f;
    private float scaleFactor = 0.25f;

    private int plusScore = 0;
    private int plusTime = 1;

    private TimeAttackLevelManager taLvlMan;
    private DestroyMapEffect desMapEff;
    private TimeAttackGUILevelUI taGUIlvlUI;

    //Score:
    public GameObject objPlusScore;

    //Level info (is the game mode timeattack?)
    private LevelInfo lvlInf;


    // Use this for initialization
    void Start()
    {
        taLvlMan = FindObjectOfType(typeof(TimeAttackLevelManager)) as TimeAttackLevelManager;
        desMapEff = FindObjectOfType(typeof(DestroyMapEffect)) as DestroyMapEffect;
        lvlInf = FindObjectOfType(typeof(LevelInfo)) as LevelInfo;
        if (lvlInf)
        {
            taGUIlvlUI = FindObjectOfType(typeof(TimeAttackGUILevelUI)) as TimeAttackGUILevelUI;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (destroyFigure)
        {
            if (expandFigure)
            {
                if (this.transform.localScale.x < expandSize)
                {
                    this.transform.localScale += new Vector3(scaleFactor, scaleFactor, 0);
                }
                else
                {

                    expandFigure = false;
                    if (lvlInf.isTimeAttack)
                    {
                        GameObject newPlustScore = GameObject.Instantiate(objPlusScore, this.transform.position, Quaternion.identity) as GameObject;

                        newPlustScore.GetComponent<PlusScoreEffect>().displayNumber = plusScore;

                        taGUIlvlUI.AddMoreTime(plusTime);
                    }
                }
            }
            else
            {
                if (this.transform.localScale.x > 0)
                {
                    this.transform.localScale -= new Vector3(scaleFactor, scaleFactor, 0);
                }
                else
                {
                    DestroyFigure();
                }
            }
        }
    }

    public void StartDestroyEffect(int figureNumber)
    {
        if (lvlInf.isTimeAttack)
        {
            if (taLvlMan.numberOfConnectionsFor1star - 1 < figureNumber)
            {
                plusScore = 50;
            }
            else
            {
                plusScore = 25;
            }

        }
        this.GetComponent<gameObjInfo>().enabled = false; //Slå gameObjInfo fra, så den ikke modvirker ændringerne på figurens størelse.
        destroyFigure = true;
    }

    public void DestroyFigure()
    {
        desMapEff.completedFigDes++;
        Destroy(this.gameObject);
    }
}

using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour 
{
    private GameObject[,] arrGameFigures;
    private LevelEditorManager lvlEMan;

    public Transform backgroundParent;

    public GameObject bgN;
    public GameObject bgNE;
    public GameObject bgE;
    public GameObject bgSE;
    public GameObject objBackgroundBlock;
    public GameObject objBackgroundBlockStartGoal;
    public GameObject objBackgroundBlockBlank;


    public int XPlus; //X og y går fra -maxValue til maxValue så for at lave et hurtigt og nemt 2d arr, smider jeg bare max oven i dens x og y plads!
    public int YPlus;

	// Use this for initialization
	void Start () 
    {
        lvlEMan = GetComponent<LevelEditorManager>();
        XPlus = int.Parse(lvlEMan.sliderX.maxValue.ToString());
        YPlus = int.Parse(lvlEMan.sliderY.maxValue.ToString());

        arrGameFigures = new GameObject[XPlus * 2 + 1, YPlus * 2 + 1];
    }

    /// <summary>
    /// Slet alle gamle backgrounds
    /// Lav et 2d array over alle figure
    /// Gå mit 2d array igennem og tjek for passende backgrounds
    /// </summary>
    public void CreateBackgrounds()
    {
        DeletAllBackgrounds();

        //lav et grid over alle figure
        GetFigures();

        RemoveUnUsedConnection();

        for (int x = 0; x < arrGameFigures.GetLength(0); x++)
        {
            for (int y = 0; y < arrGameFigures.GetLength(1); y++)
            {
                if (arrGameFigures[x,y] != null)
                {
                    //N
                    if (arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToN)
                    {
                        GameObject newBgN = GameObject.Instantiate(bgN, arrGameFigures[x, y].transform.position, Quaternion.identity) as GameObject;
                        newBgN.transform.parent = backgroundParent;
                    }

                    //NE
                    if (arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToNE)
                    {
                        GameObject newBgNE = GameObject.Instantiate(bgNE, arrGameFigures[x, y].transform.position, Quaternion.identity) as GameObject;
                        newBgNE.transform.parent = backgroundParent;
                    }

                    //E
                    if (arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToE)
                    {
                        GameObject newBgE = GameObject.Instantiate(bgE, arrGameFigures[x, y].transform.position, Quaternion.identity) as GameObject;
                        newBgE.transform.parent = backgroundParent;
                    }

                    //SE
                    if (arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToSE)
                    {
                        GameObject newBgSE = GameObject.Instantiate(bgSE, arrGameFigures[x, y].transform.position, Quaternion.identity) as GameObject;
                        newBgSE.transform.parent = backgroundParent;
                    }

                    //Backgroundblock
                    
                    if (arrGameFigures[x, y].GetComponent<gameObjInfo>().mySprite == 0 || arrGameFigures[x, y].GetComponent<gameObjInfo>().mySprite == 1)
                    {

                        GameObject newBackgroundBlock = (GameObject)Instantiate(objBackgroundBlockStartGoal);
                        newBackgroundBlock.transform.position = new Vector3((x - 2) * 1.6f, (y - 2) * 1.6f, 1);
                        newBackgroundBlock.transform.SetParent(GameObject.Find("Backgrounds").transform);

                    }
                    else if (arrGameFigures[x, y].GetComponent<gameObjInfo>().mySprite >= 2)
                    {
                        //Spawn BackgroundBlock
                        GameObject newBackgroundBlock = (GameObject)Instantiate(objBackgroundBlock);
                        newBackgroundBlock.transform.position = new Vector3((x - 2) * 1.6f, (y - 2) * 1.6f, 1);
                        newBackgroundBlock.transform.SetParent(GameObject.Find("Backgrounds").transform);

                    }
                    
                }
            }
        }
    }

    private void GetFigures()
    {
        foreach (GameObject gObj in GameObject.FindGameObjectsWithTag("Figure"))
        {
            int x = gObj.GetComponent<gameObjInfo>().x + XPlus;
            int y = gObj.GetComponent<gameObjInfo>().y + YPlus;
           
            arrGameFigures[x, y] = gObj;
        }
    }

    public void DeletAllBackgrounds()
    {
        foreach (Transform backObj in backgroundParent)
        {
            GameObject.Destroy(backObj.gameObject);
        }
    }

    public GameObject[,] GetFigureGrid()
    {
        GetFigures();
        return arrGameFigures;
    }

    private void RemoveUnUsedConnection()
    {
        for (int x = 0; x < arrGameFigures.GetLength(0); x++)
        {
            for (int y = 0; y < arrGameFigures.GetLength(1); y++)
            {
                if (arrGameFigures[x, y] != null)
                {
                    try
                    { 
                        if (arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToNW && arrGameFigures[x - 1, y + 1] == null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToNW = false;
                        }
                    }
                    catch
                    {
                        arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToNW = false;
                    }



                    //N
                    try
                    {
                        if (arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToN && arrGameFigures[x, y + 1] == null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToN = false;
                        }
                    }
                    catch
                    {
                        arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToN = false;
                    }


                    //NE
                    try
                    {
                        if (arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToNE && arrGameFigures[x + 1, y + 1] == null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToNE = false;
                        }
                    }
                    catch
                    {
                        arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToNE = false;
                    }

                    //E
                    try
                    {
                        if (arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToE && arrGameFigures[x + 1, y] == null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToE = false;
                        }
                    }
                    catch
                    {
                        arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToE = false;
                    }

                    //SE
                    try
                    {
                        if (arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToSE && arrGameFigures[x + 1, y - 1] == null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToSE = false;
                        }
                    }
                    catch
                    {
                        arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToSE = false;
                    }

                    //S
                    try
                    {
                        if (arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToS && arrGameFigures[x, y - 1] == null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToS = false;
                        }
                    }
                    catch
                    {
                        arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToS = false;
                    }

                    //SW
                    try
                    {
                        if (arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToSW && arrGameFigures[x - 1, y - 1] == null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToSW = false;
                        }
                    }
                    catch
                    {
                        arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToSW = false;
                    }

                    //W
                    try
                    {
                        if (arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToW && arrGameFigures[x - 1, y] == null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToW = false;
                        }
                    }
                    catch
                    {
                        arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToW = false;
                    }
                }
            }
        }
    }

    public void AutoConnect()
    {
        for (int x = 0; x < arrGameFigures.GetLength(0); x++)
        {
            for (int y = 0; y < arrGameFigures.GetLength(1); y++)
            {
                if (arrGameFigures[x, y] != null)
                {
                    try
                    {
                        if (!arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToNW && arrGameFigures[x - 1, y + 1] != null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToNW = true;
                            arrGameFigures[x - 1, y + 1].GetComponent<gameObjInfo>().isConnectedToSE = true;
                        }
                    }
                    catch
                    {
                        // arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToNW = false;
                    }



                    //N
                    try
                    {
                        if (!arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToN && arrGameFigures[x, y + 1] != null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToN = true;
                            arrGameFigures[x, y + 1].GetComponent<gameObjInfo>().isConnectedToS = true;
                        }
                    }
                    catch
                    {
                        // arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToN = false;
                    }


                    //NE
                    try
                    {
                        if (arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToNE && arrGameFigures[x + 1, y + 1] != null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToNE = true;
                            arrGameFigures[x + 1, y + 1].GetComponent<gameObjInfo>().isConnectedToSW = true;
                        }
                    }
                    catch
                    {
                        // arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToNE = false;
                    }

                    //E
                    try
                    {
                        if (!arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToE && arrGameFigures[x + 1, y] != null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToE = true;
                            arrGameFigures[x + 1, y].GetComponent<gameObjInfo>().isConnectedToW = true;
                        }
                    }
                    catch
                    {
                        //  arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToE = false;
                    }

                    //SE
                    try
                    {
                        if (!arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToSE && arrGameFigures[x + 1, y - 1] != null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToSE = true;
                            arrGameFigures[x + 1, y - 1].GetComponent<gameObjInfo>().isConnectedToNW = true;
                        }
                    }
                    catch
                    {
                        // arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToSE = false;
                    }

                    //S
                    try
                    {
                        if (!arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToS && arrGameFigures[x, y - 1] != null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToS = true;
                            arrGameFigures[x, y - 1].GetComponent<gameObjInfo>().isConnectedToN = true;
                        }
                    }
                    catch
                    {
                        // arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToS = false;
                    }

                    //SW
                    try
                    {
                        if (!arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToSW && arrGameFigures[x - 1, y - 1] != null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToSW = true;
                            arrGameFigures[x - 1, y - 1].GetComponent<gameObjInfo>().isConnectedToNE = true;
                        }
                    }
                    catch
                    {
                        //arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToSW = false;
                    }

                    //W
                    try
                    {
                        if (!arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToW && arrGameFigures[x - 1, y] != null)
                        {
                            arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToW = true;
                            arrGameFigures[x - 1, y].GetComponent<gameObjInfo>().isConnectedToE = true;
                        }
                    }
                    catch
                    {
                        //arrGameFigures[x, y].GetComponent<gameObjInfo>().isConnectedToW = false;
                    }
                }
            }
        }
    }
}

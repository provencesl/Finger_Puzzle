using UnityEngine;
using System.Collections;

public class IsConnectedEditor : MonoBehaviour
{
    LevelEditorManager lvlEditMan;
    BackgroundManager bgMan;
    Transform selectedFigure;

    public SpriteRenderer NorthCon;
    public SpriteRenderer NorthEastCon;
    public SpriteRenderer EastCon;
    public SpriteRenderer SouthEastCon;
    public SpriteRenderer SouthCon;
    public SpriteRenderer SouthWestCon;
    public SpriteRenderer WestCon;
    public SpriteRenderer NorthWestCon;

    private Ray ray;
    private RaycastHit2D hit;

    private int xPlus;
    private int yPlus;


    private GameObject[,] arrGameFigures;

    TouchManager tMan;
    bool isTesting = true;

    // Use this for initialization
    void Start()
    {
        lvlEditMan = FindObjectOfType(typeof(LevelEditorManager)) as LevelEditorManager;
        bgMan = FindObjectOfType(typeof(BackgroundManager)) as BackgroundManager;
        xPlus = bgMan.XPlus;
        yPlus = bgMan.YPlus;

        tMan = FindObjectOfType(typeof(TouchManager)) as TouchManager;
    }

    
    void Update()
    {
        //Disable on test!
        if (tMan.lstStartFigure.Count > 0)
        {
            if (!isTesting)
            {
                foreach (Transform child in this.gameObject.transform)
                {
                    child.gameObject.SetActive(false);
                }

                isTesting = true;
            }
        }
        else if (isTesting) //Enable afte test!
        {
            foreach (Transform child in this.gameObject.transform)
            {
                child.gameObject.SetActive(true);
            }

            isTesting = false;
        }


        //OnClick:
        if (Input.GetMouseButtonDown(0)) //Ved første klik!
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit && hit.transform.tag == "ConnectionTrigger") //Se om vi har ramt noget!
            {
                ChangeConnection(hit.transform);
            }
        }


        //Edit Cons:
        if (lvlEditMan.gameFigure != null && selectedFigure != lvlEditMan.gameFigure)
        {
            arrGameFigures = bgMan.GetFigureGrid();
            selectedFigure = lvlEditMan.gameFigure;
        }

        if (selectedFigure != null && this.transform.position != selectedFigure.position)
        {
            this.transform.position = selectedFigure.position;
        }

        if (selectedFigure != null)
        {
            DisplayConnections();
        }
    }

    void ChangeConnection(Transform hit)
    {
        if (selectedFigure != null)
        {
            int x = selectedFigure.GetComponent<gameObjInfo>().x + xPlus;
            int y = selectedFigure.GetComponent<gameObjInfo>().y + yPlus;

            switch (hit.name)
            {
                case "N":
                    try
                    {
                        arrGameFigures[x, y + 1].GetComponent<gameObjInfo>().isConnectedToS = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToN;
                        print("Test");
                    }
                    catch { }

                    selectedFigure.GetComponent<gameObjInfo>().isConnectedToN = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToN;
                    break;

                case "NE":
                    try
                    {
                        arrGameFigures[x + 1, y + 1].GetComponent<gameObjInfo>().isConnectedToSW = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToNE;
                    }
                    catch { }

                    selectedFigure.GetComponent<gameObjInfo>().isConnectedToNE = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToNE;
                    break;

                case "E":
                    try
                    {
                        arrGameFigures[x + 1, y].GetComponent<gameObjInfo>().isConnectedToW = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToE;
                    }
                    catch { }

                    selectedFigure.GetComponent<gameObjInfo>().isConnectedToE = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToE;
                    break;

                case "SE":
                    try
                    {
                        arrGameFigures[x + 1, y - 1].GetComponent<gameObjInfo>().isConnectedToNW = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToSE;
                    }
                    catch { }

                    selectedFigure.GetComponent<gameObjInfo>().isConnectedToSE = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToSE;
                    break;

                case "S":
                    try
                    {
                        arrGameFigures[x, y - 1].GetComponent<gameObjInfo>().isConnectedToN = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToS;
                    }
                    catch { }


                    selectedFigure.GetComponent<gameObjInfo>().isConnectedToS = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToS;
                    break;

                case "SW":
                    try
                    {
                        arrGameFigures[x - 1, y - 1].GetComponent<gameObjInfo>().isConnectedToNE = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToSW;
                    }
                    catch { }

                    selectedFigure.GetComponent<gameObjInfo>().isConnectedToSW = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToSW;
                    break;

                case "W":
                    try
                    {
                        arrGameFigures[x - 1, y].GetComponent<gameObjInfo>().isConnectedToE = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToW;
                    }
                    catch { }

                    selectedFigure.GetComponent<gameObjInfo>().isConnectedToW = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToW;
                    break;

                case "NW":
                    try
                    {
                        arrGameFigures[x - 1, y + 1].GetComponent<gameObjInfo>().isConnectedToSE = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToNW;
                    }
                    catch { }

                    selectedFigure.GetComponent<gameObjInfo>().isConnectedToNW = !selectedFigure.GetComponent<gameObjInfo>().isConnectedToNW;
                    break;
            }
        }


    }

    void DisplayConnections()
    {
        //North
        if (selectedFigure.GetComponent<gameObjInfo>().isConnectedToN)
        {
            NorthCon.color = new Color(255, 255, 255, 0.8f);
        }
        else
        {
            NorthCon.color = new Color(255, 255, 255, 0.1f);
        }

        //NE
        if (selectedFigure.GetComponent<gameObjInfo>().isConnectedToNE)
        {
            NorthEastCon.color = new Color(255, 255, 255, 0.8f);
        }
        else
        {
            NorthEastCon.color = new Color(255, 255, 255, 0.1f);
        }

        //E
        if (selectedFigure.GetComponent<gameObjInfo>().isConnectedToE)
        {
            EastCon.color = new Color(255, 255, 255, 0.8f);
        }
        else
        {
            EastCon.color = new Color(255, 255, 255, 0.1f);
        }

        //SE
        if (selectedFigure.GetComponent<gameObjInfo>().isConnectedToSE)
        {
            SouthEastCon.color = new Color(255, 255, 255, 0.8f);
        }
        else
        {
            SouthEastCon.color = new Color(255, 255, 255, 0.1f);
        }

        //S
        if (selectedFigure.GetComponent<gameObjInfo>().isConnectedToS)
        {
            SouthCon.color = new Color(255, 255, 255, 0.8f);
        }
        else
        {
            SouthCon.color = new Color(255, 255, 255, 0.1f);
        }

        //SW
        if (selectedFigure.GetComponent<gameObjInfo>().isConnectedToSW)
        {
            SouthWestCon.color = new Color(255, 255, 255, 0.8f);
        }
        else
        {
            SouthWestCon.color = new Color(255, 255, 255, 0.1f);
        }

        //W
        if (selectedFigure.GetComponent<gameObjInfo>().isConnectedToW)
        {
            WestCon.color = new Color(255, 255, 255, 0.8f);
        }
        else
        {
            WestCon.color = new Color(255, 255, 255, 0.1f);
        }

        //NW
        if (selectedFigure.GetComponent<gameObjInfo>().isConnectedToNW)
        {
            NorthWestCon.color = new Color(255, 255, 255, 0.8f);
        }
        else
        {
            NorthWestCon.color = new Color(255, 255, 255, 0.1f);
        }
    }
}

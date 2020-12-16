using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line;
    //store mouse position on the screen
    private Vector3 mousePos;
    public Material material;

    //number of lines drawn
    public int currLines = 0;
    public DisplayNumbers displayCurrLines;

    public List<GameObject> lstStartFigure;
    public List<GameObject> lstEndFigure;
    public List<GameObject> lstGameLines;

    public List<GameObject> lstStartFigure1;
    public List<GameObject> lstEndFigure1;
    public List<GameObject> lstGameLines1;

    public List<GameObject> lstStartFigure2;
    public List<GameObject> lstEndFigure2;
    public List<GameObject> lstGameLines2;

    public List<GameObject> lstStartFigure3;
    public List<GameObject> lstEndFigure3;
    public List<GameObject> lstGameLines3;


    public gameObjInfo startFigureInf;
    private gameObjInfo endFigureInf;

    private Ray ray;
    private RaycastHit2D hit;

    public float distanceBeforeLineReset = 0.65f;
    public float startPointOffset = 0.65f; //Hvor langt fra ens figur, linjen skal dectes fra!

    public bool isCompleted = false;
    public bool isGameOver = false;

    private bool lineReset = false;

    LevelInfo lvlInf;
    LevelManager lvlMan;

    void Start()
    {
        lvlInf = this.GetComponent<LevelInfo>();
        lvlMan = FindObjectOfType(typeof(LevelManager)) as LevelManager;
    }

    void Update()
    {
        lineReset = false;

        //Update Currline Text:
        try
        {
            //Get connections
            int numberOfGoals = 0; //Skal bruges til at fjerne mål, fra current connections.

            if (lstEndFigure1.Count > 0 && lstEndFigure1[lstEndFigure1.Count - 1].GetComponent<gameObjInfo>().mySprite == 1)
            {
                numberOfGoals++;
            }
            if (lstEndFigure2.Count > 0 && lstEndFigure2[lstEndFigure2.Count - 1].GetComponent<gameObjInfo>().mySprite == 1)
            {
                numberOfGoals++;
            }
            if (lstEndFigure3.Count > 0 && lstEndFigure3[lstEndFigure3.Count - 1].GetComponent<gameObjInfo>().mySprite == 1)
            {
                numberOfGoals++;
            }
            if (line != null)
            {
                numberOfGoals++;
            }

            currLines = lstGameLines1.Count + lstGameLines2.Count + lstGameLines3.Count - numberOfGoals;

            //Set Connections
            if (currLines != displayCurrLines.number)
            {
                displayCurrLines.number = currLines;
            }
        }
        catch
        {
            //Anti level editor crash
        }

        if (!isCompleted && !isGameOver)
        {
            if (Input.GetMouseButtonDown(0)) //Ved første klik!
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit && hit.transform.tag == "Figure") //Se om vi har ramt noget!
                {
                    FindCurrentList(hit); //Kun relevant i baner med mere end en start og mål

                    if (lstStartFigure.Contains(hit.transform.gameObject)) //hvis listen over start punkter, indeholder den figure spillere har klikket på så:
                    {
                        CheckForLineResetPoint(hit);
                        lineReset = true;
                    }

                    CheckForNewLineStart(hit);
                }
            }
            else if (Input.GetMouseButtonUp(0) && line) //Når man giver slip
            {
                int difference = Mathf.Abs(lstEndFigure.Count - lstStartFigure.Count);
                for (int i = 0; i < difference; i++ )
                {
                    RemoveLastLine();
                }

                //set line as null once the line is created!
                line = null;
            }
            else if (lstStartFigure.Count > 0 && Input.GetMouseButton(0) && line) //Når man holder musen nede!
            {
                //获取屏幕发出的射线
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                hit = Physics2D.Linecast(CalcStartPoint(lstStartFigure[lstStartFigure.Count - 1].transform.position, ray.origin), ray.origin);

                if (hit && hit.transform.tag == "Figure")
                {
                    if (lstStartFigure.Count > 0)
                    {
                        bool isANewFigure = true;

                        for (int i = 0; i < lstStartFigure.Count; i++)
                        {
                            //如果当前图形在已经连接的集合中，不创建新线段
                            if (lstStartFigure[i].transform == hit.transform)
                            {
                                isANewFigure = false;
                            }
                            else if (i < lstEndFigure.Count && lstEndFigure[i].transform == hit.transform)
                            {
                                isANewFigure = false;
                            }
                        }

                        if (isANewFigure)
                        {
                            gameObjInfo newEndFigInf = hit.collider.gameObject.GetComponent<gameObjInfo>();

                            if (!IsFigureUsed(hit.collider.gameObject)) //Check om figuren er i brug i en af de op til 3 mulige lister! (så vi kan have 3 start og slut punkter per map)
                            {
                                if (IsConnected(startFigureInf, newEndFigInf))
                                {
                                    lstEndFigure.Add(hit.transform.gameObject);
                                    endFigureInf = newEndFigInf;

                                    endFigureInf.isSelected = true;
                                    line.SetPosition(1, new Vector3(newEndFigInf.posX, newEndFigInf.posY, 1f));
                                    
                                    //Code for starting a new line:
                                    //set line as null once the line is created!
                                    line = null;

                                    //Start new line
                                    CheckForNewLineStart(hit);
                                }
                                else
                                {
                                    //Ulovlig move!
                                    newEndFigInf.showErrorEffect = true;
                                }
                            }
                            else
                            {
                                //Ulovlig move!
                                newEndFigInf.showErrorEffect = true;
                            }
                        }
                        //如果当前点击的不是上一个集合的最后一个点
                        else if (hit.transform != lstStartFigure[lstStartFigure.Count - 1].transform)
                        {
                            //Ulovlig move!
                            hit.collider.gameObject.GetComponent<gameObjInfo>().showErrorEffect = true;
                        }
                    }
                }

                //Se om vi skal edit last line:
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;

                CheckForUndoLine();



                if (lstEndFigure.Count > 0 && lstEndFigure.Count == lstStartFigure.Count) //Se om vores lstEndFigure indeholder noget, og om den er lige så lang som vores start.
                {

                    //Se efter om vi skal have fjernet vores end figure:                
                    if (Mathf.Abs(mousePos.x - lstEndFigure[lstEndFigure.Count - 1].transform.position.x) > 0.6f || Mathf.Abs(mousePos.y - lstEndFigure[lstEndFigure.Count - 1].transform.position.y) > 0.6f)
                    {
                        lstEndFigure.RemoveAt(lstEndFigure.Count - 1);
                        line.SetPosition(1, mousePos);
                    }
                }
                //Hvis vi ikke har en end figur:
                else
                {
                    line.SetPosition(1, mousePos);
                }

            }
            else if (lstStartFigure.Count == 0 && Input.GetMouseButton(0) && line) //Hvis man holder musen nede, uden at have ramt en figur ved første klik!
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit && hit.transform.tag == "Figure") //Se om vi har ramt noget!
                {
                    FindCurrentList(hit);

                    if (lstStartFigure.Contains(hit.transform.gameObject)) //Hvis listen over start punkter, indeholder den figure spillere har 2x klikket på så:
                    {
                        if (!lineReset)
                        { 
                           CheckForLineResetPoint(hit);
                        }
                    }

                    CheckForNewLineStart(hit);
                }
            }
        }
        else
        {
            if (lstStartFigure.Count > lstEndFigure.Count)
            {
                RemoveLastLine();
            }
        }
    }

    /// <summary>
    /// For at gøre spillet nemmerer for spilleren, tjekker vi om der er en figur, mellem det hvor spilleren holder fingeren nede, og den figur de startede den nyeste linje fra!
    /// Til dette skal vi have flyttet start punktet, væk fra start figuren, ellers går spillet automatisk udfra at man bliver ved med at klikke på den samme figur!
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="endPosition"></param>
    private Vector3 CalcStartPoint(Vector3 startPosition, Vector3 endPosition)
    {
        float x = 0f;
        float y = 0f;

        if (startPosition.y > endPosition.y)
        {
            y = startPosition.y - startPointOffset;
        }
        else
        {
            y = startPosition.y + startPointOffset;
        }

        if (startPosition.x > endPosition.x)
        {
            x = startPosition.x - startPointOffset;
        }
        else
        {
            x = startPosition.x + startPointOffset;
        }

        return new Vector3(x, y, 0);
    }

    /// <summary>
    /// Find udaf hvilken list, den klikket figure skal høre til!
    /// </summary>
    /// <param name="figure"></param>
    private void FindCurrentList(RaycastHit2D figure)
    {
        gameObjInfo figureHitInfo = hit.collider.gameObject.GetComponent<gameObjInfo>();

        /// <summary>
        /// 0 = A
        /// 1 = B
        /// 2 = Circle
        /// 3 = Diamond
        /// 4 = Square
        /// 5 = Triangle
        /// </summary>

        if (figureHitInfo.mySprite == 0)
        {
            //Hvis alle lister er tomme så brug list 1!
            if (lstStartFigure1.Count == 0 && lstEndFigure2.Count == 0 && lstStartFigure3.Count == 0)
            {
                UseList1();
            }
            else
            {
                //Hvis punktet er i liste 1, så brug den!
                if (lstStartFigure1.Contains(hit.transform.gameObject))
                {
                    UseList1();
                }
                //Hvis punktet er i liste 2, så brug den!
                else if (lstStartFigure2.Contains(hit.transform.gameObject))
                {
                    UseList2();
                }
                //Hvis punktet er i liste 3, så brug den!
                else if (lstStartFigure3.Contains(hit.transform.gameObject))
                {
                    UseList3();
                }
                else //Hvis den ikke er i en af de 3 lister, så find udaf hvilken vi skal bruge!
                {
                    if (lstStartFigure1.Count == 0)
                    {
                        UseList1();
                    }
                    else if (lstStartFigure2.Count == 0)
                    {
                        UseList2();
                    }
                    else
                    {
                        UseList3();
                    }
                }
            }
        }
        else
        {
            if (lstEndFigure1.Count > 0 && lstEndFigure1[lstEndFigure1.Count - 1] == hit.transform.gameObject)
            {
                UseList1();
            }
            else if (lstEndFigure2.Count > 0 && lstEndFigure2[lstEndFigure2.Count - 1] == hit.transform.gameObject)
            {
                UseList2();
            }
            else if (lstEndFigure3.Count > 0 && lstEndFigure3[lstEndFigure3.Count - 1] == hit.transform.gameObject)
            {
                UseList3();
            }
            else if (lstStartFigure1.Contains(hit.transform.gameObject))
            {
                UseList1();
            }
            else if (lstStartFigure2.Contains(hit.transform.gameObject))
            {
                UseList2();
            }
            else if (lstStartFigure3.Contains(hit.transform.gameObject))
            {
                UseList3();
            }
        }
    }

    private bool IsFigureUsed(GameObject objEndFigure)
    {
        if (lstEndFigure1.Contains(objEndFigure))
        {
            return true;
        }
        else if (lstEndFigure2.Contains(objEndFigure))
        {
            return true;
        }
        else if (lstEndFigure3.Contains(objEndFigure))
        {
            return true;
        }

        return false;
    }

    #region ændre sådan så vi bruger de rigtige lister!
    private void UseList1()
    {
        lstStartFigure = lstStartFigure1;
        lstEndFigure = lstEndFigure1;
        lstGameLines = lstGameLines1;
    }
    private void UseList2()
    {
        lstStartFigure = lstStartFigure2;
        lstEndFigure = lstEndFigure2;
        lstGameLines = lstGameLines2;
    }
    private void UseList3()
    {
        lstStartFigure = lstStartFigure3;
        lstEndFigure = lstEndFigure3;
        lstGameLines = lstGameLines3;
    }
    #endregion


    private void CheckForNewLineStart(RaycastHit2D figure)
    {

        startFigureInf = hit.collider.gameObject.GetComponent<gameObjInfo>(); //Skaf figure info

        if (startFigureInf.mySprite == 0 && lstEndFigure.Count == 0
            || lstEndFigure.Count > 0 && hit.transform == lstEndFigure[lstEndFigure.Count - 1].transform) //Hvis det er A (start) eller den vi sidst stoppede på!
        {
            lstStartFigure.Add(hit.transform.gameObject);

            startFigureInf.isSelected = true;


            //检测是否无线段生成
            if (line == null)
            {
                //
                createLine();
            }
            //定位线段起点和终点（一开始起点和终点在一起，update刷新）
            line.SetPosition(0, new Vector3(lstStartFigure[lstStartFigure.Count - 1].GetComponent< gameObjInfo>().posX, lstStartFigure[lstStartFigure.Count - 1].GetComponent<gameObjInfo>().posY, 1f));
            line.SetPosition(1, new Vector3(lstStartFigure[lstStartFigure.Count - 1].GetComponent<gameObjInfo>().posX, lstStartFigure[lstStartFigure.Count - 1].GetComponent<gameObjInfo>().posY, 1f));
        }
    }

    /// <summary>
    /// Hvis en spiller double clicker på et tidligere punkt, så reset lines tilbage til der!
    /// </summary>
    private void CheckForLineResetPoint(RaycastHit2D figure)
    {
        int listIndex = 0;

        foreach (GameObject ele in lstStartFigure)
        {
            if (figure.transform.gameObject == ele)
            {
                break;
            }
            listIndex++;
        }

        //从后向前遍历，删除线段
        for (int i = lstStartFigure.Count; i > listIndex; i--)
        {

            Destroy(lstGameLines[i - 1]); //删除当前集合中最后一条线段

            lstStartFigure[i - 1].GetComponent<gameObjInfo>().isSelected = false; //Fjern isSelected fra figuren!
            lstStartFigure.RemoveAt(i - 1);
            try
            { 
                lstEndFigure.RemoveAt(i - 1);
            }
            catch
            {
                print("Reset line catch, more start figure than end figure");
            }
            lstGameLines.RemoveAt(i - 1);

        }

        if (lstStartFigure.Count > 0)
        {
            startFigureInf = lstStartFigure[lstStartFigure.Count - 1].GetComponent<gameObjInfo>(); //Ændre startFigure classen, til at være den "nye" startfigure!
        }
    }

    /// <summary>
    /// hvis spilleren køre musen tilbage fra den sidste figur mod dens tidligere start figur, 
    /// så fjern linjens end point og gør så det er den vi nu rykker med!
    /// </summary>
    private void CheckForUndoLine()
    {
        if (lstStartFigure.Count > 1)
        {
            //Find where the line between this figure and the previously figure starts:
            Vector3 lastStartPoint = lstStartFigure[lstStartFigure.Count - 2].transform.position;

            float distanceBetweenY = Mathf.Abs(lastStartPoint.y - mousePos.y);
            float distanceBetweenX = Mathf.Abs(lastStartPoint.x - mousePos.x);

            if (distanceBetweenX <= distanceBeforeLineReset && distanceBetweenY <= distanceBeforeLineReset)
            {

                //Remove the line;
                RemoveLastLine();
                //Set the line we are working on to the previously line!
                line = lstGameLines[lstGameLines.Count - 1].GetComponent<LineRenderer>();
            }
        }
    }

    /// <summary>
    /// 移除上一条线段
    /// </summary>
    private void RemoveLastLine()
    {
        //删除线段
        Destroy(line.gameObject);
        
        if (lstStartFigure.Count > 0)
        {
            lstStartFigure[lstStartFigure.Count - 1].GetComponent<gameObjInfo>().isSelected = false; //Fjern isSelected fra figuren!
            lstStartFigure.RemoveAt(lstStartFigure.Count - 1);
        }
        
        if (lstGameLines.Count > 0)
        {
            lstGameLines.RemoveAt(lstGameLines.Count - 1); //Fjerne den nyeste linje!

        }
        if (lstStartFigure.Count > 0)
        {
            startFigureInf = lstStartFigure[lstStartFigure.Count - 1].GetComponent<gameObjInfo>(); //Ændre startFigure classen, til at være den "nye" startfigure!
        }
    }

    /// <summary>
    /// alle figure har 8 mulige connections
    /// Find udaf om det nye end punkt er en af dem
    /// og om de er connected
    /// </summary>
    /// <param name="startFigure"></param>
    /// <param name="endFigure"></param>
    /// <returns></returns>
    private bool IsConnected(gameObjInfo startFigureInfo, gameObjInfo endFigureInfo)
    {
        bool isConnected = false;

        //Hvis der ikke er mere end 1 mellem start x og end x, og start y og end y, så kan de være tæt nok på hinanden til at kunne connectes! 
        if (Mathf.Abs(startFigureInfo.x - endFigureInfo.x) <= 1 && Mathf.Abs(startFigureInfo.y - endFigureInfo.y) <= 1)
        {

            // hvis endF er højre end vores start figure:
            if (Mathf.Abs(startFigureInfo.y - endFigureInfo.y) == 1)    //endfix.y = 1
            {
                //Hvis endFig er nw for startFig, og start har en nw connect så:
                if (Mathf.Abs(startFigureInfo.x - endFigureInfo.x) == 1 && startFigureInfo.x > endFigureInfo.x && startFigureInfo.isConnectedToNW)      //endFig.x = -1
                {
                    isConnected = true;
                }
                else if (Mathf.Abs(startFigureInfo.x - endFigureInfo.x) == 0 && startFigureInfo.isConnectedToN)                                         //endFig.x = 0
                {
                    isConnected = true;
                }
                else if (Mathf.Abs(startFigureInfo.x - endFigureInfo.x) == 1 && startFigureInfo.x < endFigureInfo.x && startFigureInfo.isConnectedToNE) //endFig.x = 1
                {
                    isConnected = true;
                }

                //S
                if (Mathf.Abs(startFigureInfo.x - endFigureInfo.x) == 1 && startFigureInfo.x > endFigureInfo.x && startFigureInfo.isConnectedToSW)      //endFig.x = -1
                {
                    isConnected = true;
                }
                else if (Mathf.Abs(startFigureInfo.x - endFigureInfo.x) == 0 && startFigureInfo.isConnectedToS)                                         //endFig.x = 0
                {
                    isConnected = true;
                }
                else if (Mathf.Abs(startFigureInfo.x - endFigureInfo.x) == 1 && startFigureInfo.x < endFigureInfo.x && startFigureInfo.isConnectedToSE) //endFig.x = 1
                {
                    isConnected = true;
                }
            }
            else if (Mathf.Abs(startFigureInfo.y - endFigureInfo.y) == 0)    //endfix.y = 0
            {
                if (Mathf.Abs(startFigureInfo.x - endFigureInfo.x) == 1 && startFigureInfo.x > endFigureInfo.x && startFigureInfo.isConnectedToW)      //endFig.x = -1
                {
                    isConnected = true;
                }
                else if (Mathf.Abs(startFigureInfo.x - endFigureInfo.x) == 1 && startFigureInfo.x < endFigureInfo.x && startFigureInfo.isConnectedToE) //endFig.x = 1
                {
                    isConnected = true;
                }
            }
        }


        if (isConnected && IsSameColorOrFigure(startFigureInfo, endFigureInfo))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 是否是相同颜色或者形状
    /// </summary>
    /// <param name="startFigureInfo">起点</param>
    /// <param name="endFigureInfo">终点</param>
    /// <returns></returns>
    private bool IsSameColorOrFigure(gameObjInfo startFigureInfo, gameObjInfo endFigureInfo)
    {
        bool isSameFigureOrColor = false;

        if (startFigureInf.mySprite != 1 && endFigureInfo.mySprite != 0)
        {
            if (startFigureInfo.mySprite == 0 || endFigureInfo.mySprite == 1)
            {
                if (endFigureInfo.mySprite == 1 && currLines >= lvlInf.GetOneStarConnections() -1)
                {
                    isSameFigureOrColor = true;
                }
                else if (lvlMan != null && endFigureInfo.mySprite == 1 && lvlMan.lstGameObjGoal.Count > 1) //Hvis der er mere end 1 mål, og der stadig er 1+ mere end dette mål!
                {
                    int countCompleted = 0;

                    if (lvlMan.goal1Completed)
                    {
                        countCompleted++;
                    }
                    if (lvlMan.goal2Completed)
                    {
                        countCompleted++;
                    }
                    if (lvlMan.goal3Completed)
                    {
                        countCompleted++;
                    }

                    if (countCompleted < lvlMan.lstGameObjGoal.Count - 1)
                    {
                        isSameFigureOrColor = true;
                    }
                }
                else if (endFigureInfo.mySprite != 1)
                {
                    isSameFigureOrColor = true;
                }                
            }
            else if (startFigureInfo.mySprite == endFigureInfo.mySprite)
            {
                isSameFigureOrColor = true;
            }
            else if (startFigureInfo.myColor == endFigureInfo.myColor)
            {
                isSameFigureOrColor = true;
            }
        }

        return isSameFigureOrColor;
    }

    /// <summary>
    /// 销毁所有生成的line物体
    /// </summary>
    public void RemoveAllLines()
    {
        for (int i = currLines; i > 0; i--)
        {
            if (lstGameLines[i - 1] != null)
            {
                Destroy(lstGameLines[i - 1]);
            }

            lstStartFigure[i - 1].GetComponent<gameObjInfo>().isSelected = false;

           if (lstEndFigure[lstEndFigure.Count - 1].GetComponent<gameObjInfo>().mySprite != 1)
           { 
            currLines--;
           }
        }

        ClearAllLists();

        startFigureInf = null;

        foreach (GameObject errorLine in GameObject.FindGameObjectsWithTag("Line"))
        {
            GameObject.Destroy(errorLine);
        }
    }

    public void ClearAllLists()
    {
        lstStartFigure.Clear();
        lstStartFigure1.Clear();
        lstStartFigure2.Clear();
        lstStartFigure3.Clear();

        lstEndFigure.Clear();
        lstEndFigure1.Clear();
        lstEndFigure2.Clear();
        lstEndFigure3.Clear();

        lstGameLines.Clear();
        lstGameLines1.Clear();
        lstGameLines2.Clear();
        lstGameLines3.Clear();


        currLines = 0;
        startFigureInf = null;
    }

    /// <summary>
    /// 生成线段
    /// </summary>
    private void createLine()
    {
        //create a new empty gameobject and line renderer component
        line = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
        line.tag = "Line";
        //Add lines til vores list over spawnet linjer!
        lstGameLines.Add(line.gameObject);

        //assign the material to the line
        line.material = material;
        //set the number of points to the line
        line.SetVertexCount(2);
        //set the width
        line.SetWidth(0.15f, 0.15f);
        //render line to the world origin and not to the object's position
        line.useWorldSpace = true;
    }

}
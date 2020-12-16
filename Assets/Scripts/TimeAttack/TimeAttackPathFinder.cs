using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeAttackPathFinder : MonoBehaviour
{
    private int[,] lvlGrid = new int[5, 9];
    private int[,] lvlGridPath = new int[5, 9];
    List<string> lstGameRoute = new List<string>();

    private int routeDistance = 2;

    private int currStartX = 0;
    private int currStartY = 0;

    private int currX = 0;
    private int currY = 0;

    private int chanceForEmpty = 10; //Hvor stor en chance der er for at en plads tom!

    TimeAttackLevelCreator taLvlCreator;

    private void Start()
    {
    }

    public int[,] CreateRoute(int[,] lvlBase, int routeSize, int startX, int startY)
    {
        lvlGrid = lvlBase;
        routeDistance = routeSize;
        currStartX = startX;
        currStartY = startY;

        currX = currStartX;
        currY = currStartY;

        lvlGrid = FindRoute(lvlBase);
        FillRestOfGrid();
        
        return lvlGrid;
    }
    
    private int[,] FindRoute(int[,] lvlBase)
    {
        lvlGridPath = lvlBase;

        lstGameRoute.Clear(); //Clear lstGameRoute, så vi kan skabe en ny route til det nye map!
        int maxTries = 0;

        //Kør en for løkke, så vi gennemgår antal af connections, der skal gå fra A til B
        //Gå en connection frem adgangen, og roll derefter fra den!
        //Efter vær ny connection check på ny, hvilke pladser der er en mulig vej at gå, for at komme til B.
        for (int i = 1; i < routeDistance; i++)
        {

            //Find mulige connections:
            List<string> lstPossibleConnections = FindPossibleConnections();

            if (lstPossibleConnections.Count > 0)
            {
                //maxTries = 0;

                //Roll en random af de mulige connections:
                int nextPos = Random.Range(0, lstPossibleConnections.Count);

                //Gem den nye possition, i listen over connections, så vi kan spawne mindst en mulig løsning til mappet!:
                lstGameRoute.Add(lstPossibleConnections[nextPos]);


                //Sæt curr x og y, til den nye plads så vi kan starte forfra:
                currY = int.Parse(lstPossibleConnections[nextPos].Split(',')[0]);
                currX = int.Parse(lstPossibleConnections[nextPos].Split(',')[1]);

                lvlGridPath[currY, currX] = 3;
            }
            else
            {
                if (lstGameRoute.Count > 0 && maxTries < 10)
                {
                    maxTries++;

                    lvlGridPath[currY, currX] = 5;
                    
                    lstGameRoute.RemoveAt(lstGameRoute.Count - 1);
                    i--;

                    if (lstGameRoute.Count > 0)
                    {
                        lvlGridPath[currY, currX] = 5;

                        
                        lstGameRoute.RemoveAt(lstGameRoute.Count - 1);
                        i--;
                    }
                }
                else
                {
                    for (int x = 0; x < lvlBase.GetLength(0); x++)
                    {
                        for (int y = 0; y < lvlBase.GetLength(1); y++)
                        {
                            if (lvlBase[x, y] == 3 || lvlBase[x, y] == 2)
                            {
                                lvlBase[x, y] = 5;
                            }
                        }
                    }

                    lstGameRoute.Clear();

                    FindRoute(lvlBase);
                }
            }
        }

        if (lstGameRoute.Count == routeDistance -1)
        {
            FindGoal(lvlBase);
        }
        else
        {

            for (int x = 0; x < lvlBase.GetLength(0); x++)
            {
                for (int y = 0; y < lvlBase.GetLength(1); y++)
                {
                    if (lvlBase[x, y] == 3 || lvlBase[x, y] == 2)
                    {
                        lvlBase[x, y] = 5;
                    }
                }
            }

            lstGameRoute.Clear();

            FindRoute(lvlBase);
        }

        return lvlGridPath;
    }

    private List<string> FindPossibleConnections()
    {
        List<string> lstPossibleConnections = new List<string>();

        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (lvlGridPath.GetLength(0) > currY + y &&
                    lvlGridPath.GetLength(1) > currX + x &&
                    currX + x >= 0 &&
                    currY + y >= 0)
                {

                    if (lvlGridPath[currY + y, currX + x] >= 5)
                    {

                        if (isPossibleConnection(currX + x, currY + y))
                        {
                            int newX = currX + x;
                            int newY = currY + y;

                            lstPossibleConnections.Add(newY + "," + newX);
                        }

                    }
                }
            }
        }

        return lstPossibleConnections;
    }

    //Denne metode, tjekker om den valgte figur, har en nabofigur, som man kan danne en sti til, for at komme frem til mål!
    private bool isPossibleConnection(int x, int y)
    {
        if (y + 1 < lvlGridPath.GetLength(0) && x > 0 && x < lvlGridPath.GetLength(1))
        {
            if (lvlGridPath[y + 1, x - 1] >= 5)
            {
                return true;
            }
        }
        if (y + 1 < lvlGridPath.GetLength(0) && x < lvlGridPath.GetLength(1))
        {
            if (lvlGridPath[y + 1, x] >= 5)
            {
                return true;
            }
        }
        if (y + 1 < lvlGridPath.GetLength(0) && x + 1 < lvlGridPath.GetLength(1))
        {
            if (lvlGridPath[y + 1, x + 1] >= 5)
            {
                return true;
            }
        }
        if (x > 0 && y < lvlGridPath.GetLength(0))
        {
            if (lvlGridPath[y, x - 1] >= 5)
            {
                return true;
            }
        }
        if (x + 1 < lvlGridPath.GetLength(1) && y < lvlGridPath.GetLength(0))
        {
            if (lvlGridPath[y, x + 1] >= 5)
            {
                return true;
            }
        }
        if (x > 0 && y > 0)
        {
            if (lvlGridPath[y - 1, x - 1] >= 5)
            {
                return true;
            }
        }
        if (y > 0 && x < lvlGridPath.GetLength(1))
        {
            if (lvlGridPath[y - 1, x] >= 5)
            {
                return true;
            }
        }
        if (y > 0 && x + 1 < lvlGridPath.GetLength(1))
        {
            if (lvlGridPath[y - 1, x + 1] >= 5)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Denne methode skal fylde resten af vores grid ud med rand firgure/farve (3) og empty (4)
    /// </summary>
    private void FillRestOfGrid()
    {
        for (int x = 0; x < lvlGrid.GetLength(0); x++)
        {
            for (int y = 0; y < lvlGrid.GetLength(1); y++)
            {
                if (lvlGrid[x, y] >= 5)
                {
                    int rollFigureOrEmpty = Random.Range(0, 100);

                    if (rollFigureOrEmpty <= chanceForEmpty)
                    {
                        lvlGrid[x, y] = -1;
                    }
                    else
                    {
                        lvlGrid[x, y] = 4;
                    }
                }
            }
        }
    }

    private void FindGoal(int[,] lvlBase)
    {
        taLvlCreator = FindObjectOfType(typeof(TimeAttackLevelCreator)) as TimeAttackLevelCreator;

        //Find mulige connections:
        List<string> lstPossibleConnections = FindPossibleConnections();

        if (lstPossibleConnections.Count > 0)
        {
            //Roll en random af de mulige connections:
            int nextPos = Random.Range(0, lstPossibleConnections.Count);

            //Gem den nye possition, i listen over connections, så vi kan spawne mindst en mulig løsning til mappet!:
            lstGameRoute.Add(lstPossibleConnections[nextPos]);


            //Sæt curr x og y, til den nye plads så vi kan starte forfra:
            currY = int.Parse(lstPossibleConnections[nextPos].Split(',')[0]);
            currX = int.Parse(lstPossibleConnections[nextPos].Split(',')[1]);

            taLvlCreator.lastGoalPositionX = currX;
            taLvlCreator.lastGoalPositionY = currY;

            SetRoute();         

        }
        else
        {
            lvlGridPath = lvlGrid;
            FindRoute(lvlBase);
        }
    }

    //Sætter alle de enkelte route dele ind i map griddet.
    private void SetRoute()
    {
        taLvlCreator.lstGameRoute.Clear();

        foreach (string s in lstGameRoute)
        {
            int x = int.Parse(s.Split(',')[1]);
            int y = int.Parse(s.Split(',')[0]);

            if (x == currX && y == currY)
            {
                lvlGridPath[y, x] = 2;
            }
            else
            {
                taLvlCreator.lstGameRoute.Add(s);
            }
        }
    }
}

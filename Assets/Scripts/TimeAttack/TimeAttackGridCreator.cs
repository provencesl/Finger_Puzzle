using UnityEngine;
using System.Collections;

public class TimeAttackGridCreator : MonoBehaviour 
{

    private int minimumGoalStartDistance = 2;
    private int startX = 5;
    private int startY = 3;

    /* Kode til at teste gridet!
    public int test = 25;
    private int lastTest;

    public GameObject testObj;
    
    void Update()
    { Kode til at teste gridet!
        if (test != lastTest)
        {
            foreach (GameObject gObj in GameObject.FindGameObjectsWithTag("Figure"))
            {
            Destroy(gObj);
        }
            int[,] arrTest = CreateLvlGrid(test);
            lastTest = test;


            for (int y = 0; y < arrTest.GetLength(0); y++)
            {
                for (int x = 0; x < arrTest.GetLength(1); x++)
                {
                    if (arrTest[y,x] == 5)
                    {
                        GameObject newFigure = (GameObject)Instantiate(testObj);


                        newFigure.GetComponent<gameObjInfo>().x = x - 4;
                        newFigure.GetComponent<gameObjInfo>().y = y - 2;
                    }
                }
            }
        }
    }*/


    /// <summary>
    /// lvlSize, indeholder hvor mange figur, spillet skal indeholde!
    /// 
    /// Denne methode skal udfylde de relevante lvl pladser ud med 5 taller.
    /// Sådan så vi i TimeAttackLevelCreator, kan bruge de pladser til at skabe mappet!
    /// </summary>
    /// <param name="lvlSize"> Hvor mange figure mappet er lavet af </param>
    /// <param name="startX"> X positionen af mappets start!</param>
    /// <param name="startY"> Y -||- </param>
    /// <returns></returns>
    public int[,] CreateLvlGrid(int lvlSize, int lastGoalX, int lastGoalY, int minGoalStartDistance)
    {
        minimumGoalStartDistance = minGoalStartDistance;
        startX = lastGoalX;
        startY = lastGoalY;

        int[,] arrLvlGrid = new int[5, 9]; //2d array = [col, row], hvilket svare til [y,x] oversat til den måde jeg har opbygget mine lvls på!

        if (lvlSize >= 45) //Hvis lvlSize er 45 eller derover, så skal hele mappet fyldes ud!
        {
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    arrLvlGrid[y, x] = 5;
                }
            }
        }
        else
        {
            if (Random.Range(1, 1) == 1)
            {
                SquareMap(arrLvlGrid, lvlSize);
            }
            else
            {
                DiamonMap(arrLvlGrid, lvlSize);
            }
        }

        return arrLvlGrid;
    }

    /// <summary>
    /// Denne methode skal lave et map, som er så tæt på en firkant som den kan!
    /// 
    /// 1 op, 1 til højre
    /// 2 ned, 2 til venstre
    /// 3 op, 3 til højre
    /// 4 ned, 4 til venstre
    /// etc
    /// </summary>
    /// <param name="arrLvlGrid"></param>
    /// <returns></returns>
    private int[,] SquareMap(int[,] arrLvlGrid, int mapSize)
    {
        int directionsMoved = 0; //Hvor mange gange vi har skiftet retning! lige nummer = y, ulige = x

        int cellChange = 1; //Bliver ændre mellem plus og minus 1, sådan så vi skifter i den rigtige retning!

        int x = 4;
        int y = 2;

        int directionMoves = 1; //Hvor mange ryk vi skal lave i en col/row!

        bool yPlus = true; //Skal holde styr på om vi skal plus eller minus y!
        bool xPlus = true;

        //Find udaf om midter figuren kan være et mål!
        if (Mathf.Abs(y - startY) < minimumGoalStartDistance && Mathf.Abs(x - startX) < minimumGoalStartDistance) //Hvis pladsen er for tæt på start, til at være et mål så skal vi give den en værdi af 6
        {
            arrLvlGrid[y, x] = 6;
        }
        else                                                                          //Ellers 5
        {
            arrLvlGrid[y, x] = 5;
        }


        for (int i = 1; i < mapSize;) //i er ligmed 1 fordi vi ovenfor sætter midter punktet til 5, hvilket svar til første step :)
        {
            if (directionsMoved % 2 == 0) //Hvis tallet er lige: så ændre på y!
            {
                //Ændre cellChange til plus eller minus, sådan så vi skifter cellerne i den rigtige retning!
                if (yPlus)
                {
                    cellChange = 1;
                }
                else
                {
                    cellChange = -1;
                }

                for (int j = 0; j < directionMoves && i != mapSize; j++)
                {
                    y += cellChange;

                    if (Mathf.Abs(y - startY) < minimumGoalStartDistance  && Mathf.Abs(x - startX) < minimumGoalStartDistance) //Hvis pladsen er for tæt på start, til at være et mål så skal vi give den en værdi af 6
                    {
                        arrLvlGrid[y, x] = 6;
                    }
                    else                                                                          //Ellers 5
                    {
                        arrLvlGrid[y, x] = 5;
                    }

                    i++;
                }

                directionsMoved++;
                yPlus = !yPlus;
            }
            else //Ændre på X!
            {
                //Ændre cellChange til plus eller minus, sådan så vi skifter cellerne i den rigtige retning!
                if (xPlus)
                {
                    cellChange = 1;
                }
                else
                {
                    cellChange = -1;
                }

                for (int j = 0; j < directionMoves && i != mapSize; j++)
                {
                    x += cellChange;

                    if (Mathf.Abs(y - startY) < minimumGoalStartDistance && Mathf.Abs(x - startX) < minimumGoalStartDistance) //Hvis pladsen er for tæt på start, til at være et mål så skal vi give den en værdi af 6
                    {
                        arrLvlGrid[y, x] = 6;
                    }
                    else                                                                          //Ellers 5
                    {
                        arrLvlGrid[y, x] = 5;
                    }

                    i++;
                    
                }

                directionMoves++;
                directionsMoved++;
                xPlus = !xPlus;
            }
        }

            return arrLvlGrid;
    }
    
    /// <summary>
    /// Denne metode, skal skabe et map, så tæt på diamand form, som muligt!
    /// </summary>
    /// <param name="arrLvlGrid"></param>
    /// <returns></returns>
    private int[,] DiamonMap(int[,] arrLvlGrid, int mapSize)
    {

        return arrLvlGrid;
    }
}

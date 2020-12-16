using UnityEngine;
using System.Collections;

public class BackgroundLineCreator : MonoBehaviour
{
    public GameObject bgN;
    public GameObject bgNE;
    public GameObject bgNEX;
    public GameObject bgE;
    public GameObject bgSE;

    private TimeAttackLevelCreator tALC;

    private int[,] lvlGrid = new int[5, 9];

    // Use this for initialization
    void Start ()
    {        
    }
	
	public void CreateBackgrounds()
    {
        tALC = gameObject.GetComponent<TimeAttackLevelCreator>();
        lvlGrid = tALC.lvlGrid;

        for (int x = 0; x < lvlGrid.GetLength(1); x++)
        {
            for (int y = 0; y < lvlGrid.GetLength(0); y++)
            {
                if (lvlGrid[y, x] >= 1)
                { 
                    //N
                    if (lvlGrid.GetLength(0) > y + 1 && lvlGrid[y + 1, x] >= 1)
                    {
                        GameObject newBgN = GameObject.Instantiate(bgN, new Vector3((x - 4) * 1.6f, (y - 2) * 1.6f, 1), Quaternion.identity) as GameObject;
                        newBgN.transform.SetParent(GameObject.Find("Backgrounds").transform);
                    }

                    //NE
                    if (lvlGrid.GetLength(0) > y + 1 && lvlGrid.GetLength(1) > x + 1 && lvlGrid[y + 1, x + 1] >= 1)
                    {
                        //Når en NE linje, går over en SE linje, ser det dumt ud, så brug NEX til disse steder!
                        GameObject NELine;
                        if (lvlGrid[y + 1, x] >= 1 && lvlGrid[y, x + 1] >= 1)
                        {
                            NELine = bgNEX;
                        }
                        else
                        {
                            NELine = bgNE;
                        }

                        GameObject newBgNE = GameObject.Instantiate(NELine, new Vector3((x - 4) * 1.6f, (y - 2) * 1.6f, 1), Quaternion.identity) as GameObject;
                        newBgNE.transform.SetParent(GameObject.Find("Backgrounds").transform);
                    }

                    //E
                    if (lvlGrid.GetLength(1) > x + 1 && lvlGrid[y, x + 1] >= 1)
                    {
                        GameObject newBgE = GameObject.Instantiate(bgE, new Vector3((x - 4) * 1.6f, (y - 2) * 1.6f, 1), Quaternion.identity) as GameObject;
                        newBgE.transform.SetParent(GameObject.Find("Backgrounds").transform);
                    }

                    //SE
                    if (y > 0 && lvlGrid.GetLength(1) > x + 1 && lvlGrid[y - 1, x + 1] >= 1)
                    {
                        GameObject newBgSE = GameObject.Instantiate(bgSE, new Vector3((x - 4) * 1.6f, (y -2) * 1.6f, 1), Quaternion.identity) as GameObject;
                        newBgSE.transform.SetParent(GameObject.Find("Backgrounds").transform);
                    }
                }
            }
        }
    }

}

using UnityEngine;
using System.Collections;

public class LvlManager : MonoBehaviour {

    private LevelContainer lvlCon;
    public int numberOfLevels = 0;
    public int lvl = 0;
    int category = 0;

    GameObject[] arrCurrentCategory;
    GameObject objCurrLevel;

    // Use this for initialization
    void Start () {
        lvlCon = FindObjectOfType(typeof(LevelContainer)) as LevelContainer;
        category = PlayerPrefs.GetInt("LvlCategory");
        lvl = PlayerPrefs.GetInt("Level");

        switch (category)
        {
            case 0:
                arrCurrentCategory = lvlCon.Tutorial;
                break;

            case 1:
                arrCurrentCategory = lvlCon.Easy;
                break;

            case 2:
                arrCurrentCategory = lvlCon.Medium;
                break;

            case 3:
                arrCurrentCategory = lvlCon.Hard;
                break;
        }

        numberOfLevels = arrCurrentCategory.Length;
        LoadCurrentLevel();
    }
	
    public void LoadCurrentLevel()
    {
        if (objCurrLevel != null)
        {
            GameObject.Destroy(objCurrLevel);
        }

        objCurrLevel = GameObject.Instantiate(arrCurrentCategory[lvl-1]);
    }

    public void LoadNextLevel()
    {
        lvl++;
        PlayerPrefs.SetInt("Level", lvl);
        LoadCurrentLevel();
    }
}

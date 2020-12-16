using UnityEngine;
using System.Collections;

public class ButtonLevelCompleted : MonoBehaviour
{

    LvlManager lvlMan;

    // Use this for initialization
    void Start()
    {
        lvlMan = FindObjectOfType(typeof(LvlManager)) as LvlManager;

        if (lvlMan.numberOfLevels > lvlMan.lvl)
        {
            this.transform.Find("ButtonLevelSelect").gameObject.SetActive(false);
        }
        else
        {
            this.transform.Find("ButtonNext").gameObject.SetActive(false);
        }
    }


    public void LoadCurrentLevel()
    {
        lvlMan.LoadCurrentLevel();
        GameObject.Destroy(this.gameObject);
    }

    public void LoadNextLevel()
    {
        lvlMan.LoadNextLevel();
        GameObject.Destroy(this.gameObject);
    }

    public void LevelSelect()
    {

    }
}

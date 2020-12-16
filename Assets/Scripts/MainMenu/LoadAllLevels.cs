using UnityEngine;
using System.Collections;

public class LoadAllLevels : MonoBehaviour {

    private int lvlCategory = -1;
    int itemPositionX = 0;
    int itemPositionY = 1;

    private LevelContainer lvlCon;

    public Transform contentPanel;
    [SerializeField]
    private RectTransform contentRect;

    public GameObject lvlButton;

    // Use this for initialization
    void Start ()
    {
        lvlCon = FindObjectOfType(typeof(LevelContainer)) as LevelContainer;
        contentRect = contentPanel.GetComponent<RectTransform>();
    }
	

    public void PopulateList()
    {
        lvlCategory = PlayerPrefs.GetInt("LvlCategory");

        //Clear listview for old content:
        foreach (Transform child in contentPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //Reset positions
        itemPositionX = 0;
        itemPositionY = 1;

        //Find category, and repopulate the view!
        switch (lvlCategory)
        {
            case 0:
                PopulateList(lvlCon.Tutorial, lvlCategory);
                break;
            case 1:
                PopulateList(lvlCon.Easy, lvlCategory);
                break;
            case 2:
                PopulateList(lvlCon.Medium, lvlCategory);
                break;
            case 3:
                PopulateList(lvlCon.Hard, lvlCategory);
                break;


        }
    }

    private void PopulateList(GameObject[] arrCategory, int lvlCategory)
    {
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, 20 + 110 * arrCategory.Length);

        for (int i = 0; i < arrCategory.Length; i++)
        {
            GameObject newButton = GameObject.Instantiate(lvlButton);
            newButton.transform.SetParent(contentPanel);
            newButton.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            newButton.transform.localScale = Vector3.one;
            newButton.GetComponent<RectTransform>().localPosition = new Vector3(135 + (266 * itemPositionX), 135 - (266 * itemPositionY), 0);

            //Load level data on the button
            newButton.GetComponent<ButtonLevelSelect>().GetStats(i + 1, lvlCategory);

            //Ready the position for the next button
            if (itemPositionX == 2)
            {
                itemPositionX = 0;
                itemPositionY++;
            }
            else
            {
                itemPositionX++;
            }

        }

    }
}

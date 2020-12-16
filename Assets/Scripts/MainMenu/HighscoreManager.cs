using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighscoreManager : MonoBehaviour
{
    [SerializeField]
    GameObject highScoreItem;

    [SerializeField]
    Transform highscoreList;


    [SerializeField]
    Text txtHighScoreCatFg;
    [SerializeField]
    Text txtHighScoreCatBg;

    // Use this for initialization
    void Start()
    {
        LoadLocalHighScore();
    }

    public void LoadLocalHighScore()
    {
        //Update Highscore category text:
        txtHighScoreCatFg.text = "My High Score";
        txtHighScoreCatBg.text = txtHighScoreCatFg.text;

        //Clear highscore list
        foreach(Transform child in highscoreList)
        {
            GameObject.Destroy(child.gameObject);
        }

        //Load local High Scores
        for (int i = 0; i < 10; i++)
        {
            GameObject itemGO = (GameObject)Instantiate(highScoreItem, highscoreList);
            HighScoreElementLocal item = itemGO.GetComponent<HighScoreElementLocal>();

            if (item != null)
            {
                string s = "";
                switch (i)
                {
                    case 0:
                        s = i + 1 + "st";
                        break;
                    case 1:
                        s = i + 1 + "nd";
                        break;
                    case 2:
                        s = i + 1 + "rd";
                        break;
                    default:
                        s = i + 1 + "th";
                        break;
                }
                item.Setup(s, PlayerPrefs.GetInt("localTopScore" + i));

                item.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1); //havde et problem med at local scale blev sat til et eller andet højt!
            }
        }
    }

    public void LoadOnlineHighScore()
    {
        //Update Highscore category text:
        txtHighScoreCatFg.text = "Online High Score";
        txtHighScoreCatBg.text = txtHighScoreCatFg.text;

        //Clear highscore list
        foreach (Transform child in highscoreList)
        {
            GameObject.Destroy(child.gameObject);
        }

        //Load online High Scores
    }
}

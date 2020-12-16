using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreElementLocal : MonoBehaviour
{
    [SerializeField]
    Text usernameText;

    [SerializeField]
    Text scoreText;


    public void Setup(string username, int score)
    {
        usernameText.text = username + ":";
        scoreText.text = string.Format("{0:### ### ###}", score); //score.ToString();
    }
}

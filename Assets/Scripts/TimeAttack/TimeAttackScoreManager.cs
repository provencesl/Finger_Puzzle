using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeAttackScoreManager : MonoBehaviour 
{
    [SerializeField]
    private int currentScore = 0; //Hvad spilleren score er
    private float score = 0; //Hvad score der bliver vist (den tæller op mod currentScore, for at give en bedre effect, end hvis score blot blev instant updated)
    public float scoreUpdateSpeed = 5;

    public ScoreDisplay disScore;

    void Start()
    {
        disScore = FindObjectOfType(typeof(ScoreDisplay)) as ScoreDisplay;
    }

    /// <summary>
    /// Denne methode skal udregene ens score hver gang man klare et lvl.
    /// 
    /// </summary>
    /// <param name="figureConnected"></param>
    /// Hvor mange connections spilleren har opnået
    /// <param name="minConnections"></param>
    /// Hvor mange connections man som minimum skulle have i det gennemførte lvl.
    public void UpdateScore(int plusScore)
    {
        currentScore += plusScore;
    }

    private void UpdateGUI()
    {
        //Update gui'en til den nye score.
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
	    if (score < currentScore)
        {
            score += scoreUpdateSpeed;
           
            if (score > currentScore)
            {
                score = currentScore;
            }


            disScore.score = Mathf.RoundToInt(score);
        }
	}
}

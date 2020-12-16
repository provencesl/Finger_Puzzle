using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlusScoreEffect : MonoBehaviour
{
    public List<Sprite> lstNumbers;
    public int displayNumber = 0;
    private int numberDisplayed = 0;

    public SpriteRenderer firstNumber;
    public SpriteRenderer secondNumber;
    public SpriteRenderer thirdNumber;

    private Vector3 moveTo = new Vector3(0, 5, 0);
    public float moveSpeed = 2;

    private float scaleFactor = 0.05f;

    private TimeAttackScoreManager taScoreMan;


    // Use this for initialization
    void Start()
    {
        taScoreMan = FindObjectOfType(typeof(TimeAttackScoreManager)) as TimeAttackScoreManager;

    }

    
    void Update()
    {
        if (displayNumber != numberDisplayed)
        {
            int i = 1;
            foreach (char ch in displayNumber.ToString())
            {
                int number = int.Parse(ch.ToString());

                if (i == 1)
                {
                    firstNumber.sprite = lstNumbers[number];
                }
                else if (i == 2)
                {

                    secondNumber.sprite = lstNumbers[number];
                }
                else
                {

                    thirdNumber.sprite = lstNumbers[number];
                }
                i++;
            }
        }

        if (this.transform.position != moveTo)
        {
            float x = Mathf.Lerp(this.transform.position.x, moveTo.x, Time.deltaTime * moveSpeed);
            float y = Mathf.Lerp(this.transform.position.y, moveTo.y, Time.deltaTime * moveSpeed);

            this.transform.position = new Vector3(x, y, 0);

            if (5 - y < 0.6)
            {
                this.transform.localScale -= new Vector3(scaleFactor, scaleFactor, 0);

                if (this.transform.localScale.x < 0)
                {
                    taScoreMan.UpdateScore(displayNumber);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}

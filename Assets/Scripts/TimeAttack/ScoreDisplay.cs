using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public List<Sprite> lstNumbers;

    public int score = 0;
    private int displayedScore = -1;
    
    private GameObject number1;
    private GameObject number2;
    private GameObject number3;
    private GameObject number4;
    private GameObject number5;
    private GameObject number6;
    private GameObject number7;

    // Use this for initialization
    void Start ()
    {
        //Find score pladserne
        number1 = this.transform.GetChild(0).gameObject;
        number2 = this.transform.GetChild(1).gameObject;
        number3 = this.transform.GetChild(2).gameObject;
        number4 = this.transform.GetChild(3).gameObject;
        number5 = this.transform.GetChild(4).gameObject;
        number6 = this.transform.GetChild(5).gameObject;
        number7 = this.transform.GetChild(6).gameObject;

        //Skjule scoren til vi får brug for den
        number1.SetActive(false);
        number2.SetActive(false);
        number3.SetActive(false);
        number4.SetActive(false);
        number5.SetActive(false);
        number6.SetActive(false);
        number7.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (displayedScore != score)
        {
            if (score < 10)
            {
                //Vis score:
                number1.SetActive(true);
                //Skjul:
                number2.SetActive(false);
                number3.SetActive(false);
                number4.SetActive(false);
                number5.SetActive(false);
                number6.SetActive(false);
                number7.SetActive(false);

                number1.transform.localPosition = new Vector3(0, 0, 0);

                number1.GetComponent<Image>().sprite = lstNumbers[score];

                displayedScore = score;
            }
            else if (score < 100)
            {
                //Vis score:
                number1.SetActive(true);
                number2.SetActive(true);
                //Skjul:
                number3.SetActive(false);
                number4.SetActive(false);
                number5.SetActive(false);
                number6.SetActive(false);
                number7.SetActive(false);

                number1.transform.localPosition = new Vector3(-7.5f, 0, 0);
                number2.transform.localPosition = new Vector3(7.5f, 0, 0);

                char[] arrScore = score.ToString().ToCharArray();

                number1.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[0].ToString())];
                number2.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[1].ToString())];

                displayedScore = score;
            }
            else if  (score < 1000)
            {
                //Vis score:
                number1.SetActive(true);
                number2.SetActive(true);
                number3.SetActive(true);
                //Skjul:
                number4.SetActive(false);
                number5.SetActive(false);
                number6.SetActive(false);
                number7.SetActive(false);

                number1.transform.localPosition = new Vector3(-15, 0, 0);
                number2.transform.localPosition = new Vector3(0, 0, 0);
                number3.transform.localPosition = new Vector3(15, 0, 0);

                char[] arrScore = score.ToString().ToCharArray();

                number1.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[0].ToString())];
                number2.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[1].ToString())];
                number3.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[2].ToString())];

                displayedScore = score;
            }
            else if (score < 10000)
            {
                //Vis score:
                number1.SetActive(true);
                number2.SetActive(true);
                number3.SetActive(true);
                number4.SetActive(true);
                //Skjul:
                number5.SetActive(false);
                number6.SetActive(false);
                number7.SetActive(false);

                number1.transform.localPosition = new Vector3(-25f, 0, 0);
                number2.transform.localPosition = new Vector3(-5f, 0, 0);
                number3.transform.localPosition = new Vector3(10f, 0, 0);
                number4.transform.localPosition = new Vector3(25f, 0, 0);

                char[] arrScore = score.ToString().ToCharArray();

                number1.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[0].ToString())];
                number2.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[1].ToString())];
                number3.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[2].ToString())];
                number4.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[3].ToString())];

                displayedScore = score;
            }
            else if (score < 100000)
            {
                //Vis score:
                number1.SetActive(true);
                number2.SetActive(true);
                number3.SetActive(true);
                number4.SetActive(true);
                number5.SetActive(true);
                //Skjul:
                number6.SetActive(false);
                number7.SetActive(false);

                number1.transform.localPosition = new Vector3(-35f, 0, 0);
                number2.transform.localPosition = new Vector3(-20f, 0, 0);
                number3.transform.localPosition = new Vector3(0, 0, 0);
                number4.transform.localPosition = new Vector3(15, 0, 0);
                number5.transform.localPosition = new Vector3(30, 0, 0);

                char[] arrScore = score.ToString().ToCharArray();

                number1.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[0].ToString())];
                number2.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[1].ToString())];
                number3.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[2].ToString())];
                number4.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[3].ToString())];
                number5.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[4].ToString())];

                displayedScore = score;
            }
            else if (score < 1000000)
            {
                //Vis score:
                number1.SetActive(true);
                number2.SetActive(true);
                number3.SetActive(true);
                number4.SetActive(true);
                number5.SetActive(true);
                number6.SetActive(true);
                //Skjul:
                number7.SetActive(false);

                number1.transform.localPosition = new Vector3(-40f, 0, 0);
                number2.transform.localPosition = new Vector3(-25f, 0, 0);
                number3.transform.localPosition = new Vector3(-10f, 0, 0);
                number4.transform.localPosition = new Vector3(10f, 0, 0);
                number5.transform.localPosition = new Vector3(25.5f, 0, 0);
                number6.transform.localPosition = new Vector3(40f, 0, 0);

                char[] arrScore = score.ToString().ToCharArray();

                number1.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[0].ToString())];
                number2.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[1].ToString())];
                number3.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[2].ToString())];
                number4.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[3].ToString())];
                number5.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[4].ToString())];
                number6.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[5].ToString())];

                displayedScore = score;
            }
            else if (score < 10000000)
            {
                //Vis score:
                number1.SetActive(true);
                number2.SetActive(true);
                number3.SetActive(true);
                number4.SetActive(true);
                number5.SetActive(true);
                number6.SetActive(true);
                number7.SetActive(true);

                number1.transform.localPosition = new Vector3(-50f, 0, 0);
                number2.transform.localPosition = new Vector3(-30f, 0, 0);
                number3.transform.localPosition = new Vector3(-15f, 0, 0);
                number4.transform.localPosition = new Vector3(-0f, 0, 0);
                number5.transform.localPosition = new Vector3(20f, 0, 0);
                number6.transform.localPosition = new Vector3(35f, 0, 0);
                number7.transform.localPosition = new Vector3(50f, 0, 0);

                char[] arrScore = score.ToString().ToCharArray();

                number1.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[0].ToString())];
                number2.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[1].ToString())];
                number3.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[2].ToString())];
                number4.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[3].ToString())];
                number5.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[4].ToString())];
                number6.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[5].ToString())];
                number7.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[6].ToString())];

                displayedScore = score;
            }
        }
	}
}

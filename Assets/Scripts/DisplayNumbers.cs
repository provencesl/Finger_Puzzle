using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class DisplayNumbers : MonoBehaviour 
{
    public bool canShowTwoDigit = false;
    public List<Sprite> lstNumbers;

    public int number = 0;
    private int displayedNumber = -1;

    private float y;
    [SerializeField]
    private float airBetweenNumbers;

    private GameObject number1;
    private GameObject number2;
    
	// Use this for initialization
    void Start()
    {
        number1 = this.transform.GetChild(0).gameObject;
        number1.SetActive(false);
    
        if (canShowTwoDigit)
        { 
            number2 = this.transform.GetChild(1).gameObject;
            number2.SetActive(false);
        }

        y = number1.transform.localPosition.y;
        airBetweenNumbers = number1.GetComponent<RectTransform>().rect.width / 8.5f;
      }
	
	// Update is called once per frame
	void Update () 
    {
        if (displayedNumber != number)
        {
            if (number < 10 && number >= 0)
            {
                number1.SetActive(true);

                number1.transform.localPosition = new Vector3(0, y, 0);

                number1.GetComponent<Image>().sprite = lstNumbers[number];

                displayedNumber = number;


                if (canShowTwoDigit)
                {
                    number2.SetActive(false);
                }
            }
            else if (canShowTwoDigit && number >= 10)
            {
                //Vis score:
                number1.SetActive(true);
                number2.SetActive(true);


                number1.transform.localPosition = new Vector3(-airBetweenNumbers, y, 0);
                number2.transform.localPosition = new Vector3(airBetweenNumbers, y, 0);

                char[] arrNumber = number.ToString().ToCharArray();

                number1.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrNumber[0].ToString())];
                number2.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrNumber[1].ToString())];

                displayedNumber = number;
            }
        }
	
	}
}

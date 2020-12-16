using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LvlNumber : MonoBehaviour
{
    public List<Sprite> lstNumbers;

    public int number = 0;
    private int displayedNumber = -1;

    private GameObject number1;
    private GameObject number2;

    private ButtonLevelSelect btnLvlSelect;

    // Use this for initialization
    void Start()
    {
        number1 = this.transform.GetChild(0).gameObject;
        number2 = this.transform.GetChild(1).gameObject;

        number1.SetActive(false);
        number2.SetActive(false);

        btnLvlSelect = this.GetComponent<ButtonLevelSelect>();
        number = btnLvlSelect.myLevel;
    }

    
    void Update()
    {
        if (number != btnLvlSelect.myLevel)
        {
            number = btnLvlSelect.myLevel;
        }


        if (displayedNumber != number)
        {
            if (number < 10)
            {
                //Vis score:
                number1.SetActive(true);
                //Skjul:
                number2.SetActive(false);

                number1.transform.localPosition = new Vector3(0, 50, 0);

                number1.GetComponent<Image>().sprite = lstNumbers[number];

                displayedNumber = number;
            }
            else if (number < 100)
            {
                //Vis score:
                number1.SetActive(true);
                number2.SetActive(true);

                number1.transform.localPosition = new Vector3(-25f, 50, 0);
                number2.transform.localPosition = new Vector3(25f, 50, 0);

                char[] arrScore = number.ToString().ToCharArray();

                number1.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[0].ToString())];
                number2.GetComponent<Image>().sprite = lstNumbers[int.Parse(arrScore[1].ToString())];

                displayedNumber = number;
            }
        }
    }
}

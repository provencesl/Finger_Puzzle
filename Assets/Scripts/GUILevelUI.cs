using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUILevelUI : MonoBehaviour
{
    public Canvas canvas;

    //Stars:
    public CanvasRenderer star1;
    public CanvasRenderer star2;
    public CanvasRenderer star3;

    //Star text:
    private DisplayNumbers star1Number;
    private DisplayNumbers star2Number;
    private DisplayNumbers star3Number;

    //starbar:
    public Slider starBar;
    public float targetStarBarValue;
    public GameObject starBarContainer;

    //StarEffect
    private bool startStarEffect1 = false;
    private bool startStarEffect2 = false;
    private bool startStarEffect3 = false;

    private float starScaleFactor = 0.5f;
    private float starNormaleSize;
    private float starMaxSize;
    private float starNormaleRotation;

    private RectTransform star1RectTransform;
    private RectTransform star2RectTransform;
    private RectTransform star3RectTransform;

    private int stars = 0;

    // Use this for initialization
    void Start ()
    {
        ShowStarBar(false);
        targetStarBarValue = 0;

        //Get star RectTransforms:
        star1RectTransform = star1.GetComponent<RectTransform>();
        star2RectTransform = star2.GetComponent<RectTransform>();
        star3RectTransform = star3.GetComponent<RectTransform>();

        star1Number = star1.GetComponent<DisplayNumbers>();
        star2Number = star2.GetComponent<DisplayNumbers>();
        star3Number = star3.GetComponent<DisplayNumbers>();

        //Get the normale rect width of the stars:
        starNormaleSize = star1RectTransform.rect.width;
        starMaxSize = starNormaleSize * 1.25f;
        starNormaleRotation = star1RectTransform.rotation.z;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (starBar.value != targetStarBarValue)
        {
            starBar.value = Mathf.Lerp(starBar.value, targetStarBarValue, Time.deltaTime * 2.5f);
        }

        //Star1:
        //Star Grove Effect:
        if (startStarEffect1)
        {
            if (star1RectTransform.rect.width < starMaxSize - 0.25f)
            {
                //Find new size
                float newStarSize = Mathf.Lerp(star1RectTransform.rect.width, starMaxSize, Time.deltaTime * 15f);
                //Set new Size
                star1RectTransform.sizeDelta = new Vector2(newStarSize, newStarSize);
            }
            else
            {
                //Stop Grove Effect!
                startStarEffect1 = false;
            }
        }
        //Star shrink effect:
        else if (star1RectTransform.rect.width > starNormaleSize)
        {
            //Find new size
            float newStarSize = Mathf.Lerp(star1RectTransform.rect.width, starNormaleSize, Time.deltaTime * 15f);
            //Set new Size
            star1RectTransform.sizeDelta = new Vector2(newStarSize, newStarSize);
            
        }


        //Star2:
        //Star Grove Effect:
        if (startStarEffect2)
        {
            if (star2RectTransform.rect.width < starMaxSize - 0.25f)
            {
                //Find new Size
                float newStarSize = Mathf.Lerp(star2RectTransform.rect.width, starMaxSize, Time.deltaTime * 15f);
                //Set new Size
                star2RectTransform.sizeDelta = new Vector2(newStarSize, newStarSize);
                
            }
            else
            {
                startStarEffect2 = false;
            }
        }
        //Star shrink effect:
        else if (star2RectTransform.rect.width > starNormaleSize)
        {
            float newStarSize = Mathf.Lerp(star2RectTransform.rect.width, starNormaleSize, Time.deltaTime * 15f);

            star2RectTransform.sizeDelta = new Vector2(newStarSize, newStarSize);

        }

        //Star3:
        //Star Grove Effect:
        if (startStarEffect3)
        {
            if (star3RectTransform.rect.width < starMaxSize - 0.25f)
            {
                float newStarSize = Mathf.Lerp(star3RectTransform.rect.width, starMaxSize, Time.deltaTime * 15f);
                star3RectTransform.sizeDelta = new Vector2(newStarSize, newStarSize);

            }
            else
            {
                startStarEffect3 = false;
            }
        }
        //Star shrink effect:
        else if (star3RectTransform.rect.width > starNormaleSize)
        {
            float newStarSize = Mathf.Lerp(star3RectTransform.rect.width, starNormaleSize, Time.deltaTime * 15f);

            star3RectTransform.sizeDelta = new Vector2(newStarSize, newStarSize);

        }

    }

    public void StarBarStats(float barPercentage)
    {
        targetStarBarValue = barPercentage;
    }

    public void ShowStarBar(bool showBar)
    {
        if (showBar)
        {
            starBarContainer.SetActive(true);

            ShowNumberOfStars(stars);
        }
        else
        {
            starBarContainer.SetActive(false);
        }
    }

    public void ShowNumberOfStars(int numberOfStars)
    {
        stars = numberOfStars;

        if (numberOfStars == 0)
        {
            startStarEffect1 = false;
            startStarEffect2 = false;
            startStarEffect3 = false;

            star1.SetColor(Color.white);
            star2.SetColor(Color.white);
            star3.SetColor(Color.white);
        }
        else if (numberOfStars == 1)
        {
            startStarEffect1 = true;
            startStarEffect2 = false;
            startStarEffect3 = false;

            star1.SetColor(Color.yellow);
            star2.SetColor(Color.white);
            star3.SetColor(Color.white);

        }
        else if (numberOfStars == 2)
        {
            startStarEffect2 = true;
            startStarEffect3 = false;

            star1.SetColor(Color.yellow);
            star2.SetColor(Color.yellow);
            star3.SetColor(Color.white);
        }
        else
        {
            startStarEffect3 = true;

            star1.SetColor(Color.yellow);
            star2.SetColor(Color.yellow);
            star3.SetColor(Color.yellow);
        }
    }

    public void Reset
        
        
        ()
    {
        if (star1 != null) //Når man lukker spillet ned vil den forsøge at kalde denne metode som det sidste, hvilket fik den til at crash, da den inden denne metode blev kaldt, slettede de 3 stars :P
        {
            star1.SetColor(Color.white);
            star2.SetColor(Color.white);
            star3.SetColor(Color.white);
            targetStarBarValue = 0;
            stars = 0;
        }
    }



    public void UpdateStarRequirement(int starValue1, int starValue2, int starValue3)
    {
        float barWidth = starBarContainer.GetComponent<RectTransform>().rect.width;
        float starPositionProcent;

        if (starValue2 >= 50)
        {
            star2.gameObject.SetActive(false);
            star3.gameObject.SetActive(false);

            starBar.maxValue = starValue1;
            starPositionProcent = barWidth / starValue1;

        }
        else
        {
            star2.gameObject.SetActive(true);
            star3.gameObject.SetActive(true);

            starBar.maxValue = starValue3;
            starPositionProcent = barWidth / starValue3;


            star2Number.number = starValue2;
            star3Number.number = starValue3;

            float star2Pos = starPositionProcent * starValue2;
            float star3Pos = starPositionProcent * starValue3;


            star2.GetComponent<RectTransform>().anchoredPosition = new Vector2(star2Pos, star2.GetComponent<RectTransform>().anchoredPosition.y);
            star3.GetComponent<RectTransform>().anchoredPosition = new Vector2(star3Pos, star3.GetComponent<RectTransform>().anchoredPosition.y);
        }

        star1Number.number = starValue1;
        

        float star1Pos = starPositionProcent * starValue1;

        star1.GetComponent<RectTransform>().anchoredPosition = new Vector2(star1Pos, star1.GetComponent<RectTransform>().anchoredPosition.y);
    }
}

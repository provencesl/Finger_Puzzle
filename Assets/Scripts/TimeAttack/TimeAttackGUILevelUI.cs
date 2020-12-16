using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeAttackGUILevelUI : MonoBehaviour
{
    public Canvas canvas;

    //Stars:
    public CanvasRenderer star1;

    //Star text:
    private DisplayNumbers star1Number;

    //starbar:
    public Slider starBar;
    public float targetStarBarValue = 0;
    public GameObject starBarContainer;

    //Timer:
    public Image timerCircle;
    public DisplayNumbers displayNumber;
    [SerializeField]
    private float targetTimerBarValue = 1;
    public float maxSec = 30;
    [SerializeField]
    private float oneSec = 0.033f; //0.033f = 30sek

    //StarEffect
    private bool startStarEffect1 = false;
    private float starScaleFactor = 0.5f;
    private float starNormaleSize;
    private float starMaxSize;
    private float starNormaleRotation;
    private RectTransform star1RectTransform;

    //Button
    public GameObject ReturnToLevelSelecetButton;

    private int stars = 0;

    public bool isPaused = false;

    //Game over:
    private bool isGameOver = false;
    public GameObject objGameOver;
    public ScoreDisplay puzzleCompleted;
    public Text txtNewHighScore;
    TouchManager touchManager;
    Animator animatorShowGameOver;
    TimeAttackLevelCreator tALvlC;

    void Awake()
    {
        //Get star RectTransforms:
        star1RectTransform = star1.GetComponent<RectTransform>();
        star1Number = star1.GetComponent<DisplayNumbers>();
    }

    // Use this for initialization
    void Start()
    {
        oneSec = 1 / maxSec;


        //Get the normale rect width of the stars:
        starNormaleSize = star1RectTransform.rect.width;
        starMaxSize = starNormaleSize * 1.25f;
        starNormaleRotation = star1RectTransform.rotation.z;

        displayNumber.number = (int)maxSec;

        if (objGameOver == null)
        {
            objGameOver = GameObject.Find("GameOver");
        }
        objGameOver.SetActive(false);

        touchManager = FindObjectOfType(typeof(TouchManager)) as TouchManager;
        animatorShowGameOver = GetComponent<Animator>();
        tALvlC = FindObjectOfType(typeof(TimeAttackLevelCreator)) as TimeAttackLevelCreator;
    }

    // Update is called once per frame
    void Update()
    {
        if (starBar.value != targetStarBarValue)
        {
            starBar.value = Mathf.Lerp(starBar.value, targetStarBarValue, Time.deltaTime * 2.5f);
        }
        if (timerCircle.fillAmount != targetTimerBarValue)
        {
            timerCircle.fillAmount = Mathf.Lerp(timerCircle.fillAmount, targetTimerBarValue - oneSec, Time.deltaTime / 1);
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
            ReturnToLevelSelecetButton.SetActive(true);

            ShowNumberOfStars(stars);
        }
        else
        {
            starBarContainer.SetActive(false);
            ReturnToLevelSelecetButton.SetActive(false);
        }
    }

    public void ShowNumberOfStars(int numberOfStars)
    {
        stars = numberOfStars;

        if (numberOfStars == 0)
        {
            startStarEffect1 = false;

            star1.SetColor(Color.white);
        }
        else if (numberOfStars == 1)
        {
            startStarEffect1 = true;

            star1.SetColor(Color.yellow);

        }
    }

    public void ResetStarBar()
    {
        if (star1 != null) //Når man lukker spillet ned vil den forsøge at kalde denne metode som det sidste, hvilket fik den til at crash, da den inden denne metode blev kaldt, slettede de 3 stars :P
        {
            star1.SetColor(Color.white);
            targetStarBarValue = 0;
            stars = 0;
        }
    }

    //Skal vente 1 frame, fordi unity venter 1 frame med at delete objects, og det sucks at have missing obj's :P
    public IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);

        if (!isPaused)
        {
            targetTimerBarValue -= oneSec;

            float timeLeft = targetTimerBarValue / oneSec;
            displayNumber.number = (int)Mathf.Round(timeLeft);
        }

        if (targetTimerBarValue <= 0)
        {
            //Game over man!
            int currScore = this.transform.Find("Score").GetComponent<ScoreDisplay>().score;
            bool isNewHighScore = false;
            string place = "";

            for (int i = 0; i < 10; i++)
            {
                int oldScore = PlayerPrefs.GetInt("localTopScore" + i);

                if (oldScore <= currScore)
                {
                    PlayerPrefs.SetInt("localTopScore" + i, currScore);
                    currScore = oldScore;

                    isNewHighScore = true;
                    if (place == "")
                    {
                        switch (i)
                        {
                            case 0:
                                place = i + 1 + "st";
                                break;
                            case 1:
                                place = i + 1 + "nd";
                                break;
                            case 2:
                                place = i + 1 + "rd";
                                break;
                            default:
                                place = i + 1 + "th";
                                break;
                        }
                    }
                }
            }

            if (isNewHighScore)
            {
                txtNewHighScore.text = place + " place";
            }


            //Show Gameover
            isGameOver = true;
            isPaused = true;
            // print("//Games Over man!... Game over!");
            touchManager.isGameOver = true;
            animatorShowGameOver.SetBool("IsGameOver", isGameOver);
            objGameOver.SetActive(true);


            //Puzzle Completed
            puzzleCompleted.score = tALvlC.lvlsCompleted;


        }

        if (!isGameOver)
        {
            StartCoroutine(CountDown());
        }
    }

    public void AddMoreTime(int plusTime)
    {
        float newTime = targetTimerBarValue += (plusTime * oneSec) / 2;

        if (newTime > 1)
        {
            targetTimerBarValue = 1;
        }
        else
        {
            targetTimerBarValue = newTime;
        }

        float timeLeft = targetTimerBarValue / oneSec;
        displayNumber.number = (int)Mathf.Round(timeLeft);
    }

    public void UpdateStarRequirement(int starValue1, int lvlSize)
    {
        starValue1 -= 1;

        star1Number.number = starValue1;

        starBar.maxValue = lvlSize;

        float barWidth = starBarContainer.GetComponent<RectTransform>().rect.width;

        float starPositionProcent = barWidth / lvlSize;

        float star1Pos = starPositionProcent * starValue1;

        star1.GetComponent<RectTransform>().anchoredPosition = new Vector2(star1Pos, star1.GetComponent<RectTransform>().anchoredPosition.y);
    }
}

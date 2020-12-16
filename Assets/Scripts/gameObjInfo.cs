using UnityEngine;
using System.Collections;

public class gameObjInfo : MonoBehaviour
{
    //Positionen:
    public int x; 
    public int y;

    public float posX;
    public float posY;

    /// <summary>
    /// som udgangs punk vil der altid være en sti fra et punkt og til alle dens naboer!
    /// </summary>
    public bool isConnectedToN = true;          //North
    public bool isConnectedToNE = true;
    public bool isConnectedToE = true;          //East
    public bool isConnectedToSE = true;
    public bool isConnectedToS = true;          //South
    public bool isConnectedToSW = true;
    public bool isConnectedToW = true;          //West
    public bool isConnectedToNW = true;

    public bool isSelected = false;             //Hvis vi har selected dette punkt så indikerer det visuelt!

    public bool showErrorEffect = false;        //Hvis denne figure er en ulovlig move, så vis det!

    //Error effects:
    private int numberOfShakes = 0;
    private int maxNumberOfShakes = 4;
    private float shakeR = 0.1f;                //radius of the shakes!
    private float shakeSpeed = 12f;

    //isSelected scale:
    Vector3 normalScale;
    private float scaleFactor = 0.02f;

    /// <summary>
    /// 0 = hvid
    /// 1 = rød
    /// 2 = blå
    /// 3 = grøn
    /// 4 = gul
    /// </summary>
    public int myColor = 0;
    [SerializeField]
    private int lastChangedColor = 0;
    //生成的图片颜色（默认白色）
    private Color redRGB;
    private Color blueRGB; 
    private Color greenRGB; 
    private Color yellowRGB; 
    //生成的图片形状
    public Sprite sA;
    public Sprite sB;
    public Sprite sCircle;
    public Sprite sDiamond;
    public Sprite sSquare;
    public Sprite sTriangle;

    /// <summary>
    /// 0 = A
    /// 1 = B
    /// 2 = Circle
    /// 3 = Diamond
    /// 4 = Square
    /// 5 = Triangle
    /// </summary>
    public int mySprite = 0;
    [SerializeField]
    private int lastChangedSprite = 0;


    ColorControllerTA colorConTA;

    // Use this for initialization
    void Start ()
    {
        normalScale = this.transform.localScale;

        posX = x * 1.6f;
        posY = y * 1.6f;


        //Get Colors: 
        colorConTA = FindObjectOfType(typeof(ColorControllerTA)) as ColorControllerTA;
        redRGB = colorConTA.redRGB;
        blueRGB = colorConTA.blueRGB;
        greenRGB = colorConTA.greenRGB;
        yellowRGB = colorConTA.yellowRGB;

        //Set Sprite/color
        setSprite(mySprite);
        changeColor(myColor);

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (posX != x * 1.6f)
        { 
            posX = x * 1.6f;
        }
        if (posY != y * 1.6f)
        { 
            posY = y * 1.6f;    
        }

        if (isSelected)
        { 
            if (this.transform.localScale.x < normalScale.x + 0.2f)
            {
                this.transform.localScale += new Vector3(scaleFactor, scaleFactor, 0);
            }
        }
        else
        {
            if (this.transform.localScale.x > normalScale.x)
            {

                this.transform.localScale -= new Vector3(scaleFactor, scaleFactor, 0);
            }
        }

        if (showErrorEffect)
        {
            if (numberOfShakes < maxNumberOfShakes)
            {
                if (numberOfShakes % 2 == 0) //even number!
                {
                    float targetX = posX + shakeR;
                    Vector3 targetPos = new Vector3(targetX, this.transform.localPosition.y);
                    
                    if (this.transform.position.x < targetX - 0.02f)
                    {
                        this.transform.localPosition = Vector3.Slerp(this.transform.localPosition, targetPos, Time.deltaTime * shakeSpeed);
                    }
                    else
                    {
                        numberOfShakes++;
                    }
                }
                else //Odd number!
                {
                    float targetX = posX - shakeR;
                    Vector3 targetPos = new Vector3(targetX, this.transform.position.y);

                    if (this.transform.position.x > targetX + 0.02f)
                    {
                        this.transform.position = Vector3.Slerp(this.transform.position, targetPos, Time.deltaTime * shakeSpeed);
                    }
                    else
                    {
                        numberOfShakes++;
                    }
                }
            }
            else
            {
                Vector3 targetPos = new Vector3(posX, this.transform.position.y);

                if (this.transform.position.x > posX + 0.02f)
                {
                    this.transform.position = Vector3.Slerp(this.transform.position, targetPos, Time.deltaTime * shakeSpeed);
                }
                else
                {
                    this.transform.position = targetPos;
                    showErrorEffect = false;
                    numberOfShakes = 0;
                }
            }
        }
        else
        {
            if (this.transform.position != new Vector3(posX, posY))
            {
                this.transform.position = new Vector3(posX, posY);
            }
        }

	    if (myColor != lastChangedColor)
        {
            changeColor(myColor);
        }
        if (mySprite != lastChangedSprite)
        {
            setSprite(mySprite);
        }
    }

    void changeColor(int _Color)
    {
        switch (_Color)
        {
            case 0:
                GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case 1:
                GetComponent<SpriteRenderer>().color = redRGB;
                break;
            case 2:
                GetComponent<SpriteRenderer>().color = blueRGB;
                break;
            case 3:
                GetComponent<SpriteRenderer>().color = greenRGB;
                break;
            case 4:
                GetComponent<SpriteRenderer>().color = yellowRGB;
                break;
        }

        lastChangedColor = _Color;
    }

    void setSprite(int _Sprite)
    {
        switch (_Sprite)
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = sA;
                break;
            case 1:
                GetComponent<SpriteRenderer>().sprite = sB;
                break;
            case 2:
                GetComponent<SpriteRenderer>().sprite = sCircle;
                break;
            case 3:
                GetComponent<SpriteRenderer>().sprite = sDiamond;
                break;
            case 4:
                GetComponent<SpriteRenderer>().sprite = sSquare;
                break;
            case 5:
                GetComponent<SpriteRenderer>().sprite = sTriangle;
                break;
        }

        lastChangedSprite = _Sprite;
    }

}

using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    Vector3[] arrV3Steps;
    [SerializeField]
    int currStep = 0;

    public GameObject objHand;
    private Transform hand; //Skal bruges til at skjule hånden
    private Vector3 startPos;

    [SerializeField]
    float speed = 3.5f;

    [SerializeField]
    float breakTime = 0.25f;

    private bool isOnBreak = false;

    TouchManager tMan;

    //Display error!
    public bool haveErrorFigure = false;
    [SerializeField]
    GameObject errorFigure;
    public int errorIndex = 0;


    // Use this for initialization
    void Start()
    {
        tMan = GameObject.Find("Main Camera").GetComponent<TouchManager>();
        startPos = objHand.transform.position;
        hand = objHand.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (tMan.lstStartFigure.Count == 0)
        {
            if (!isOnBreak)
            {
                hand.gameObject.SetActive(true);
                objHand.transform.localPosition = Vector3.MoveTowards(objHand.transform.localPosition, arrV3Steps[currStep], speed * Time.deltaTime);

                if (objHand.transform.localPosition == arrV3Steps[currStep])
                {

                    isOnBreak = true;
                    StartCoroutine(BreakTimer());


                }
            }
        }
        else
        {
            hand.gameObject.SetActive(false);

            currStep = 0;
            objHand.transform.position = startPos;
        }
    }

    private IEnumerator BreakTimer()
    {
        if (haveErrorFigure && currStep == errorIndex)
        {
            errorFigure.GetComponent<gameObjInfo>().showErrorEffect = true;
        }

        yield return new WaitForSeconds(breakTime);

        isOnBreak = false;

        currStep++;

        if (currStep == arrV3Steps.Length)
        {
            currStep = 0;
            objHand.transform.position = startPos;
        }


    }
}

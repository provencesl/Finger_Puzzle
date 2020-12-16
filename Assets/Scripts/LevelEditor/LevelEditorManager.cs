using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; // Required when Using UI elements.

public class LevelEditorManager : MonoBehaviour
{
    public GameObject gObjFigure;
    public GameObject gObjEmptyBackground;
    public Transform levelParent;

    public Slider sliderFigure;
    public Slider sliderColor;

    public Slider sliderX;
    public Slider sliderY;

    public InputField input1Star;
    public InputField input2Stars;
    public InputField input3Stars;

    private Ray ray;
    private RaycastHit2D hit;
    
    public Transform gameFigure;
    [SerializeField]
    private int targetsFigure = 0;
    [SerializeField]
    private int targetsColor = 0;

    [SerializeField]
    private int targetsX = 0;
    [SerializeField]
    private int targetsY = 0;
    [SerializeField]
    private List<GameObject> lstFigures;
    private int lstLocation;
    private GameObject objRemoveFigure;

    // Use this for initialization
    void Start ()
    {
        foreach (GameObject gObj in GameObject.FindGameObjectsWithTag("Figure"))
        {
            lstFigures.Add(gObj);

        }
        /*
        input1Star.text = levelParent.GetComponent<LevelManager>().numberOfConnectionsFor1star.ToString();
        input2Stars.text = levelParent.GetComponent<LevelManager>().numberOfConnectionsFor2Stars.ToString();
        input3Stars.text = levelParent.GetComponent<LevelManager>().numberOfConnectionsFor3Stars.ToString();*/
    }

    
    void Update()
    {
        //Create Figure
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnFigure();
        }
        //Move Figure
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveX(-1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveX(1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveY(1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveY(-1);
        }
        //Skift mellem figure
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SelectNewFigure(-1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectNewFigure(1);
        }
        //Delete Figure
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            RemoveFigure();
        }
        //Change Figure icon
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeFigure(-1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeFigure(1);
        }
        //ChangeColor
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeColor(-1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeColor(1);
        }

        if (Input.GetMouseButtonDown(0)) //Ved første klik!
        {
            CastRay();
          
        }
        
        if (gameFigure != null)
        { 
            gameFigure.GetComponent<gameObjInfo>().mySprite = targetsFigure;
            gameFigure.GetComponent<gameObjInfo>().myColor = targetsColor;
            gameFigure.GetComponent<gameObjInfo>().x = targetsX;
            gameFigure.GetComponent<gameObjInfo>().y = targetsY;

            //Flyt slider (så de passer med det man har ændre med wasd)
            sliderX.value = targetsX;
            sliderY.value = targetsY;
            sliderFigure.value = targetsFigure;
            sliderColor.value = targetsColor;
        }
    }

    void CastRay()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit && hit.transform.tag == "Figure") //Se om vi har ramt noget!
        {
            Debug.DrawLine(ray.origin, hit.point);
            Debug.Log("Hit object: " + hit.collider.transform.name);
            
            if (hit.transform.tag == "Figure")
            {
                SelectFigure(hit.collider.transform);
            }
        }
    }

    void SelectFigure(Transform figure)
    {
        if (gameFigure != null)
        {
            gameFigure.GetComponent<gameObjInfo>().isSelected = false;
        }
        gameFigure = figure;
        gameFigure.GetComponent<gameObjInfo>().isSelected = true;

        targetsFigure = gameFigure.GetComponent<gameObjInfo>().mySprite;
        targetsColor = gameFigure.GetComponent<gameObjInfo>().myColor;
        targetsX = gameFigure.GetComponent<gameObjInfo>().x;
        targetsY = gameFigure.GetComponent<gameObjInfo>().y;

        sliderFigure.value = targetsFigure;
        sliderColor.value = targetsColor;
        sliderX.value = targetsX;
        sliderY.value = targetsY;

        //Find figurens plads i lstFigures
        for (int i = 0; i < lstFigures.Count; i++)
        {
            if (lstFigures[i].transform == figure)
            {
                lstLocation = i;
            }
        }
    }

    private void SelectNewFigure(int selectDir)
    {
        lstLocation += selectDir;

        if (lstLocation < 0)
        {
            lstLocation = lstFigures.Count - 1;
        }
        else if (lstLocation > lstFigures.Count - 1)
        {
            lstLocation = 0;
        }        
   
        SelectFigure(lstFigures[lstLocation].transform);
    }

    public void ChangeColor()
    {
        targetsColor = int.Parse(sliderColor.value.ToString());
    }
    private void ChangeColor(int colorDir)
    {
        int currentColor = int.Parse(sliderColor.value.ToString());

        if (colorDir == -1)
        {
            if (currentColor > sliderColor.minValue)
            {
                targetsColor += colorDir;
            }
            else
            {
                targetsColor = int.Parse(sliderColor.maxValue.ToString());
            }
        }
        else
        {
            if (currentColor < sliderColor.maxValue)
            {
                targetsColor += colorDir;
            }
            else
            {
                targetsColor = int.Parse(sliderColor.minValue.ToString());
            }
        }
    }

    public void ChangeFigure()
    {
        targetsFigure = int.Parse(sliderFigure.value.ToString());
    }
    private void ChangeFigure(int figureDir)
    {
        int currentFigure = int.Parse(sliderFigure.value.ToString());

        if (figureDir == -1)
        {
            if (currentFigure > sliderFigure.minValue)
            {
                targetsFigure += figureDir;
            }
            else
            {
                targetsFigure = int.Parse(sliderFigure.maxValue.ToString());
            }
        }
        else
        {
            if (currentFigure < sliderFigure.maxValue)
            {
                targetsFigure += figureDir;
            }
            else
            {
                targetsFigure = int.Parse(sliderFigure.minValue.ToString());
            }
        }
    }

    public void ChangeX()
    {
        targetsX = int.Parse(sliderX.value.ToString());
    }
    private void MoveX(int moveDirection)
    {
        if (moveDirection < 0 && targetsX > sliderX.minValue || moveDirection > 0 && targetsX < sliderX.maxValue)
        { 
            targetsX += moveDirection;
        }
    }

    public void ChangeY()
    {
            targetsY = int.Parse(sliderY.value.ToString());
        
    }
    private void MoveY(int moveDirection)
    {

        if (moveDirection < 0 && targetsY > sliderY.minValue || moveDirection > 0 && targetsY < sliderY.maxValue)
        {
            targetsY += moveDirection;
        }
    }

    public void SpawnFigure()
    {
        GameObject newFigure = (GameObject)Instantiate(gObjFigure);
        newFigure.transform.parent = levelParent;
        SelectFigure(newFigure.transform);
        
        gameFigure = newFigure.transform;
        lstFigures.Add(newFigure);
    }

    private void RemoveFigure()
    {
        lstLocation = -1;

        //Find gameFigure i lstFigures og fjern den.
        for (int i = 0; i < lstFigures.Count; i++ )
        {
            if (lstFigures[i].transform == gameFigure)
            {
                lstLocation = i;
                objRemoveFigure = lstFigures[i];
                lstFigures.RemoveAt(i);
                break;
            }
        }


        if (lstLocation > -1)
        { 
            //Delete GameObject
            GameObject.Destroy(objRemoveFigure);
        }

        if (lstFigures.Count > 0)
        {
            //Find den nye selected figure
            if (lstFigures.Count > lstLocation)
            {
                SelectFigure(lstFigures[lstLocation].transform);
            }
            else if (lstFigures.Count > 0)
            {
                SelectFigure(lstFigures[lstLocation - 1].transform);
            }
        }
    }

    public void Change1Star()
    {
        try
        {
            levelParent.GetComponent<LevelManager>().numberOfConnectionsFor1star = int.Parse(input1Star.text);
        }
        catch
        { }
    }

    public void Change2Star()
    {
        try
        {
        levelParent.GetComponent<LevelManager>().numberOfConnectionsFor2Stars = int.Parse(input2Stars.text);
        }
        catch
        { }
    }

    public void Change3Star()
    {
        try
        { 
        levelParent.GetComponent<LevelManager>().numberOfConnectionsFor3Stars = int.Parse(input3Stars.text);
        }
        catch
        { }
    }
}


using UnityEngine;
using System.Collections;

public class LevelContainer : MonoBehaviour
{
    public string dataVersion = "1.0.0";
    
    public GameObject[] Tutorial;
    public GameObject[] Easy;
    public GameObject[] Medium;
    public GameObject[] Hard;

    void Awake()
    {
        
    }

    // Use this for initialization
    void Start () 
    {
	    if (!PlayerPrefs.HasKey("LevelData0"))
        {
            string lvls0 = "0";
            string lvls1 = "0";
            string lvls2 = "0";
            string lvls3 = "0";

            for (int i = 1; i < Tutorial.Length; i++)
            {
                lvls0 += ",0";
            }
            PlayerPrefs.SetString("LevelData0", lvls0);

            for (int i = 1; i < Easy.Length; i++)
            {
                lvls1 += ",0";
            }
            PlayerPrefs.SetString("LevelData1", lvls1);

            for (int i = 1; i < Medium.Length; i++)
            {
                lvls2 += ",0";
            }
            PlayerPrefs.SetString("LevelData2", lvls2);

            for (int i = 1; i < Hard.Length; i++)
            {
                lvls3 += ",0";
            }
            PlayerPrefs.SetString("LevelData3", lvls3);

            PlayerPrefs.SetString("DataVersion", dataVersion);


            //Set top score
            for (int i = 0; i < 10; i++)
            {
                string prefsScore = "localTopScore" + i;

                PlayerPrefs.SetInt(prefsScore, 0);
            }
        }
        else if (PlayerPrefs.GetString("DataVersion") != dataVersion) //Check om jeg har givet spillet en ny version, siden spillet er blevet hentet/updateret sidste gang
        {
            //Tutorial
            int tutorialCount = PlayerPrefs.GetString("LevelData0").Split(',').Length;

            if (tutorialCount != Tutorial.Length) //Check om der er kommet flere maps i denne kategori
            {
                string newTutorialPrefs = PlayerPrefs.GetString("LevelData0"); //Hent den gamle string ned.

                for (int i = tutorialCount; i < Tutorial.Length; i++)
                {
                    newTutorialPrefs += ",0";
                }

                PlayerPrefs.SetString("LevelData0", newTutorialPrefs);
            }


            //Easy
            int easyCount = PlayerPrefs.GetString("LevelData1").Split(',').Length;

            if (easyCount != Easy.Length) //Check om der er kommet flere maps i denne kategori
            {
                string newEasyPrefs = PlayerPrefs.GetString("LevelData1"); //Hent den gamle string ned.

                for (int i = easyCount; i < Easy.Length; i++)
                {
                    newEasyPrefs += ",0";
                }

                PlayerPrefs.SetString("LevelData1", newEasyPrefs);
            }


            //Medium
            int mediumCount = PlayerPrefs.GetString("LevelData2").Split(',').Length;

            if (mediumCount != Medium.Length) //Check om der er kommet flere maps i denne kategori
            {
                string newMediumPrefs = PlayerPrefs.GetString("LevelData2"); //Hent den gamle string ned.

                for (int i = mediumCount; i < Medium.Length; i++)
                {
                    newMediumPrefs += ",0";
                }
                PlayerPrefs.SetString("LevelData2", newMediumPrefs);
            }


            //Hard
            int hardCount = PlayerPrefs.GetString("LevelData3").Split(',').Length;

            if (hardCount != Hard.Length) //Check om der er kommet flere maps i denne kategori
            {
                string newHardPrefs = PlayerPrefs.GetString("LevelData3"); //Hent den gamle string ned.

                for (int i = hardCount; i < Hard.Length; i++)
                {
                    newHardPrefs += ",0";
                }
                PlayerPrefs.SetString("LevelData3", newHardPrefs);
            }

            PlayerPrefs.SetString("DataVersion", dataVersion);

            //LocalScores:
            for (int i = 0; i < 10; i++)
            {
                string prefsScore = "localTopScore" + i;

                if (!PlayerPrefs.HasKey(prefsScore))
                {
                    PlayerPrefs.SetInt(prefsScore, 0);

                }
            }
        }
    }
	
}

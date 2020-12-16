using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReplayLevelClick()
    {
        SceneManager.LoadScene("TimeAttack");
    }

    public void HomeClick()
    {
        Destroy(GameObject.Find("LevelContainer"));
        SceneManager.LoadScene("Home");
    }

}

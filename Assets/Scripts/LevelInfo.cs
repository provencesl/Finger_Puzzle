using UnityEngine;
using System.Collections;

public class LevelInfo : MonoBehaviour 
{
    public bool isTimeAttack = false;

    public int GetOneStarConnections()
    {
        int oneStarConnnections = 0;

        if (isTimeAttack)
        {
            oneStarConnnections = GameObject.FindGameObjectWithTag("Level").GetComponent<TimeAttackLevelManager>().numberOfConnectionsFor1star;   
        }
        else
        {
            oneStarConnnections = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelManager>().numberOfConnectionsFor1star;  
        }

        return oneStarConnnections; 
    }

}

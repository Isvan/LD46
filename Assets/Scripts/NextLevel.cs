using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public static int carryOverRats = 0;
    public static int roomNum = 0;
    public static string nextSceneStatic;

    public static string transitionLevel = "LevelTransition";

    public string nextScene;
    public bool isLevelDoor;
    public bool isLastLevel;


    private string ratsTag = "Rat";
    private string victory = "Victory";


    void SendMsg()
    {
        if (isLevelDoor || isLastLevel)
        {
            roomNum++;
            int totalFollowingRats = 0;
            GameObject[] rats = GameObject.FindGameObjectsWithTag(ratsTag);
            foreach (GameObject rat in rats)
            {
                rat_pack_tracking ratTracker = rat.GetComponent<rat_pack_tracking>();
                if (ratTracker != null && ratTracker.InBossRange())
                {
                    totalFollowingRats++;
                }
            }
            carryOverRats = totalFollowingRats;
            nextSceneStatic = nextScene;
            if (isLastLevel)
            {
                SceneManager.LoadScene(victory);
                return;
            }
            Debug.Log(transitionLevel);
            SceneManager.LoadScene(transitionLevel);
        }
        else // door is in the transition level
        {
            Debug.Log(nextSceneStatic);

            SceneManager.LoadScene(nextSceneStatic);
        }
    }
}

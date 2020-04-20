using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public static int carryOverRats = 0;
    public static string nextSceneStatic;

    public static string transitionLevel = "LevelTransition";

    public string nextScene;
    public bool isLevelDoor;

    private string ratsTag = "Rat";

    void SendMsg()
    {
        if (isLevelDoor)
        { 
            int totalFollowingRats = 0;
            GameObject[] rats = GameObject.FindGameObjectsWithTag(ratsTag);
            print(rats.Length);
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
            SceneManager.LoadScene(transitionLevel);
        }
        else // door is in the transition level
        {
            SceneManager.LoadScene(nextSceneStatic);
        }
    }
}

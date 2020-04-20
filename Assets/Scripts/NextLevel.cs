using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public static int carryOverRats = 0;
    public static string nextLevel;

    public string nextScene;
    public bool useStaticNextScene;
    public bool updateCarryOver;

    private string ratsTag = "Rat";

    void SendMsg()
    {
        if (updateCarryOver)
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
        }
        Debug.Log(nextScene);
        SceneManager.LoadScene(nextScene);
    }
}

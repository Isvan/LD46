using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBorder : MonoBehaviour
{
    private string ratsTag = "Rat";
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        GameObject[] rats = GameObject.FindGameObjectsWithTag(ratsTag);
        foreach (GameObject rat in rats)
        {
            rat_pack_tracking ratTracker = rat.GetComponent<rat_pack_tracking>();
            if (ratTracker != null)
            {
                ratTracker.randomPlaceLimits = collider;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

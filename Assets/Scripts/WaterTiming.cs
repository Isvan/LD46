using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTiming : MonoBehaviour
{
    public Transform waterSurface;
    public float totalTime;
    float currentTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        Debug.Log(currentTime);
        if(currentTime > totalTime)
        {
            return;
        }
        waterSurface.Translate(0, currentTime * 0.10f, 0);
    }
}
//for player_movement to destroy the bossrat
        //if (hit.gameObject.CompareTag("Trap"))
        //{
        //   Destroy(gameObject, 0);
        //}

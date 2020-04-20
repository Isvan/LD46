using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTiming : MonoBehaviour
{
    public Transform waterSurface;
    public float totalTime;
    float currentTime = 0;
    float ratio;
    // Start is called before the first frame update
    void Start()
    {
       ratio = 30f/totalTime;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > totalTime)
        {
            return;
        }
        waterSurface.Translate(0, 0.025f * ratio, 0);
    }
}


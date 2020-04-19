using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    public float startTime;

    private float currentTime;
    private Text text;
    private LevelHandler levelHandler;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startTime;
        text = GetComponent<Text>();
        levelHandler = Camera.main.GetComponent<LevelHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0)
        {
            levelHandler.FailLevel();
            return;
        }

        int minutes = (int)(currentTime / 60f);
        int seconds = (int)currentTime % 60;
        int centiseconds = (int)(currentTime * 100) % 100;

        text.text = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, centiseconds);
    }
}

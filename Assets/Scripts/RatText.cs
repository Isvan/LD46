using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RatText : MonoBehaviour
{
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(NextLevel.carryOverRats);
        int rats = NextLevel.carryOverRats;
        text = GetComponent<Text>();
        text.text = string.Format("Saved {0} rats", rats);


    }

}

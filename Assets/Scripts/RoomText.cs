using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomText : MonoBehaviour
{
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        int room = NextLevel.roomNum;
        text = GetComponent<Text>();
        text.text = string.Format("Room {0} of 10", room);
    }
}

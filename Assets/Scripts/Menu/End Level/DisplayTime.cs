using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayTime : MonoBehaviour
{
    TMP_Text textObject;

    void Start()
    {
        textObject = GetComponent<TMP_Text>();        

        textObject.text = "Time: " + TimeSpan.FromSeconds(Time.time - ScoreManager.instance.levelStartTime).ToString(@"mm\:ss");
    }
}

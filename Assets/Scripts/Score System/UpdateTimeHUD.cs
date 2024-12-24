using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateTimeHUD : MonoBehaviour
{
    TMP_Text textObject;

    void Start()
    {
        textObject = GetComponent<TMP_Text>();        
    }

    void Update()
    {
        textObject.text = TimeSpan.FromSeconds(Time.time - ScoreManager.instance.levelStartTime).ToString(@"mm\:ss");
    }
}

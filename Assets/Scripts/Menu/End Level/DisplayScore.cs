using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    TMP_Text textObject;

    void Start()
    {
        textObject = GetComponent<TMP_Text>();        

        textObject.text = "Score: " + Mathf.FloorToInt((Time.time - ScoreManager.instance.levelStartTime) / ScoreManager.instance.score * 10000f).ToString();
    }
}

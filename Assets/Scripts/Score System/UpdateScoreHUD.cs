using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateScoreHUD : MonoBehaviour
{
    TMP_Text textObject;

    void Start()
    {
        textObject = GetComponent<TMP_Text>();        
    }

    void Update()
    {
        textObject.text = ScoreManager.instance.score.ToString();
    }
}

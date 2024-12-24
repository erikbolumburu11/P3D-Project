using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayPoints : MonoBehaviour
{
    TMP_Text textObject;

    void Start()
    {
        textObject = GetComponent<TMP_Text>();        

        textObject.text = "Points: " + ScoreManager.instance.score.ToString();
    }
}

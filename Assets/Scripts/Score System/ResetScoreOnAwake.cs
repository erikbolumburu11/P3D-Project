using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScoreOnAwake : MonoBehaviour
{
    void Start(){
        ScoreManager.instance.levelStartTime = Time.time;
        ScoreManager.instance.score = 0;
    }
}

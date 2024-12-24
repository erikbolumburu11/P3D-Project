using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] bool HUDElement = false;
    void Update()
    {
        if(!HUDElement) transform.LookAt(FindObject.FindPlayer().transform);
    }

    public void SetValue(float amount){
        GetComponent<Slider>().value = amount;
    }

    public void SetValue(float current, float max){
        GetComponent<Slider>().value = current / max;
    }
}

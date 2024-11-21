using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObject : MonoBehaviour
{
    public static GameObject FindPlayer(){
        return GameObject.FindGameObjectWithTag("Player");
    }

    public static GameObject FindFPSCamera(){
        return GameObject.FindGameObjectWithTag("FPS Camera");
    }
}

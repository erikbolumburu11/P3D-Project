using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnEnable : MonoBehaviour
{
    public string newScene;

    public void OnEnable(){
        SceneManager.LoadScene(newScene);
    }
}

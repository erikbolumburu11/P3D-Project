using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string playButtonScene;

    public void PlayGameButton(){
        SceneManager.LoadScene(playButtonScene);
    }

    public void ExitGameButton(){
        Application.Quit();
    }
}
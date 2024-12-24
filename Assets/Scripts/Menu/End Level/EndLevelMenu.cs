using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelMenu : MonoBehaviour
{
    public string levelSceneName;

    void Start(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RetryLevelButton(){
        SceneManager.LoadScene(levelSceneName);
    }

    public void QuitGame(){
        Application.Quit();
    }
}

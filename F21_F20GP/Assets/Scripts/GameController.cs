using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName: "MainGame");

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

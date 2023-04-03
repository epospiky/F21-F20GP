using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName: "PlayerLogic");

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

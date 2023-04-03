using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{

    public void Restart()
    {
        SceneManager.LoadScene("PlayerLogic");
    }
}

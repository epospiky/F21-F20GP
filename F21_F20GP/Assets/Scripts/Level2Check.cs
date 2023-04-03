using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Level2Check : MonoBehaviour
{
    public WorldGeneration gen;
    void Update()
    {
        if (gen.returnTotalAgents() == 0)
        {
            SceneManager.LoadScene(sceneName: "Level2");
        }
    }
}

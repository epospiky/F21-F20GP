using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    public static bool isGameOver;
    public static int playerHP = 500;
    public TextMeshProUGUI playerHPText;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        playerHPText.text = "+" + playerHP;
        if (isGameOver)
        {
            SceneManager.LoadScene("PlayerLogic");
        }
    }
    public static void TakeDamge(int damageAmount)
    {
        playerHP -= damageAmount;
        if(playerHP <= 0)
        {
            isGameOver = true;
        }
    }
}

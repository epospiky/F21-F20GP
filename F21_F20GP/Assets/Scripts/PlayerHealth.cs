using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    public int maxHealth, currentHealth;
    public float iFrames = 1f;
    private float iCounter;

    public void Awake()
    {
        instance = this; 
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        if(iCounter > 0) 
        {
            iCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer(int damageAmount)
    {
        if (iCounter <= 0)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }

            iCounter = iFrames;
            UIController.instance.healthSlider.value = currentHealth;
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(int damage)
    {
        //player's health is reduced by the damage amount
        health -= damage;

        if (health <= 0)
        {
            //death effect
            Instantiate(deathEffect, transform.position, transform.rotation);

            // Destroy the player game object
            Destroy(gameObject);
        }
    }

}

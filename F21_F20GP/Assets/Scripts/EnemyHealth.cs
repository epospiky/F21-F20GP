using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth = 5;
    public EnemyController eC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageEnemy(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(eC != null)
        {
            eC.getShot();
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);    
        }
    }
}

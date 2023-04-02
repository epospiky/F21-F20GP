using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy_Cont : MonoBehaviour
{
    public int enemeyHP = 100;
    public GameObject projectile;
    public Transform projectilePoint;
    //Transform player;
    public Animator anim;
    public GameObject player;
    public EnemyHealth enemyHealth;


    public void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Update()
    {
        GameOver();
    }
    public void Shoot()
    {
       Rigidbody rb =  Instantiate(projectile, projectilePoint.position, projectilePoint.rotation).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 5f, ForceMode.Impulse);
        rb.AddForce(transform.up ,  ForceMode.Impulse);
        rb.AddForce( transform.right * -1.3f , ForceMode.Impulse);
    }

    public void TakeDamage(int damageAmout)
    {
        enemeyHP -= damageAmout;
        if(enemeyHP <= 0)
        {
            anim.SetTrigger("death");
            GetComponent<CapsuleCollider>().enabled = false;
        }
        else
        {
            anim.SetTrigger("damage");
        }
    }

    public void GameOver()
    {
        if (!player.activeSelf)
        {
            Destroy(player);
            SceneManager.LoadScene("GameoverScreen");
        }
    }


}

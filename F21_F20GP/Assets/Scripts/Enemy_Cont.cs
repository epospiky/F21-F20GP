using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Cont : MonoBehaviour
{
    public int enemeyHP = 100;
    public GameObject projectile;
    public Transform projectilePoint;
    public Animator anim;

    public void Shoot()
    {
       Rigidbody rb =  Instantiate(projectile, projectilePoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 50f, ForceMode.Impulse);
        rb.AddForce(transform.up * 5,  ForceMode.Impulse);
        rb.AddForce( transform.right * -2, ForceMode.Impulse);
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

}

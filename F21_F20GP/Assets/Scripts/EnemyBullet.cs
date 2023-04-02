using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float radius = 3;
    public int damageAmount = 1;
    public GameObject impactEffect;
    private void OnCollisionEnter(Collision collision)
    {
        //FindObjectOfType<AudioManager>().Play("Explosion");
        //GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider nearbyOject in colliders)
        {
            PlayerDamage.TakeDamge(damageAmount);
        }
        Destroy(gameObject, 2);
    }
}

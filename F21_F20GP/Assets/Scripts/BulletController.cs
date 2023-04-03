using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BulletController : MonoBehaviour
{
    public float movSpeed, lifeTime;
    public Rigidbody rb;
    public GameObject impactEffect;
    public int damage ;
    public bool damagePlayer, damageEnemy;
    public AudioSource shotSound;
    
    // Start is called before the first frame update
    void Start()
    {
        shotSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * movSpeed;
        lifeTime -= Time.deltaTime; 
        
        if(lifeTime <= 0 )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            //Destroy(other.gameObject);
            shotSound.Play();
            other.gameObject.GetComponent<EnemyHealth>().DamageEnemy(damage);
        }

        if(other.gameObject.tag == "Player" && damagePlayer) 
        {
            //Debug.Log("hit" + transform.position);
            shotSound.Play();
            PlayerHealth.instance.DamagePlayer(damage);
        }



        Destroy(gameObject);
        Instantiate(impactEffect, transform.position +(transform.forward * -movSpeed * Time.deltaTime), transform.rotation);
    }
}

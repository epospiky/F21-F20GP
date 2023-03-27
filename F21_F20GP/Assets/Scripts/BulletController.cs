using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float movSpeed, lifeTime;
    public Rigidbody rb;
    public GameObject impactEffect;
    // Start is called before the first frame update
    void Start()
    {
        
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
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }



        Destroy(gameObject);
        Instantiate(impactEffect, transform.position +(transform.forward * -movSpeed * Time.deltaTime), transform.rotation);
    }
}

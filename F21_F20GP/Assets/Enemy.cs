using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float shootDistance = 5f;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    public int bulletDamage = 1;
    public float bulletSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public ParticleSystem bulletParticles;
    public float particleDuration = 1f;
    public float particleDelay = 0.1f;
    public float shotCooldown = 0.5f;
    public Animator myAnimator;

    NavMeshAgent enemyAgent;
    private float timeSinceLastShot;

    private void Start()
    {

    }

    private void Update()
    {
        if (player == null) { return; }

        timeSinceLastShot += Time.deltaTime;
        if (myAnimator.GetBool("isChasing"))
            Shoot();
    }
    void Shoot()
    {


        if (timeSinceLastShot < shotCooldown)
        {
            return;
        }

        timeSinceLastShot = .35f;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        bullet.GetComponent<Bullet>().damage = bulletDamage;
        bulletParticles.Play();
        Invoke("StopParticleEffect", particleDuration);
    }

}

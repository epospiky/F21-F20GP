using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    
    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 5f;
    private Vector3 targetPoint, startpoint ;
    public NavMeshAgent agent;
    public float keepChasingTime = 5f;
    private float chaseCounter;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate, waitBetweenShots = 2f, timeToShoot =1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;
    private bool wasShot;
    public Animator WolfAnimator;
    public int damage;


    // Start is called before the first frame update
    void Start()
    {
        startpoint = transform.position;
        shootTimeCounter = timeToShoot;
        shotWaitCounter = waitBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = Player_Cont.instance.transform.position;
        targetPoint.y = transform.position.y;
        if (!chasing)
        {
            

            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                // Wolf run animation start
                if (WolfAnimator != null)
                {
                    WolfAnimator.Play("Run Forward WO Root");
                }

                chasing = true;
                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots;
            }

            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;
                if (chaseCounter <= 0)
                {
                    agent.destination = startpoint;
                }
            }
        }
        else
        {
            if(WolfAnimator != null)
            {
                distanceToStop = 0.0f;
            
            }

            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                agent.destination = targetPoint;
            }
         else

            {
                // Wolf run animation start
                /*if (WolfAnimator != null)
                {
                    WolfAnimator.Play("Run Forward WO Root");
                }*/

                agent.destination = transform.position;
            }
           
           
            
            if (Vector3.Distance(transform.position,targetPoint) > distanceToLose) 
            {
                if (!wasShot)
                {
                    chasing = false;

                    chaseCounter = keepChasingTime;
                }
            }
            else
            {
                wasShot = false;
            }

            if (shotWaitCounter > 0)
            {
                shotWaitCounter -= Time.deltaTime;
                if(shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }
            }
            else
            {
               

                shootTimeCounter -= Time.deltaTime;

                if (shootTimeCounter > 0)
                {
                    fireCount -= Time.deltaTime;

                    if (fireCount <= 0)
                    {
                        fireCount = fireRate;
                        // Wolf attack animation start
                        if (WolfAnimator != null)
                        {
                            WolfAnimator.Play("Bite Attack");
                        }
                        if (firePoint != null)
                        { 
                            Instantiate(bullet, firePoint.position, firePoint.rotation);
                        }

                    }
                }
                else
                {
                    shotWaitCounter = waitBetweenShots;
                }
            }
        }
    }

    public void getShot()
    {
        wasShot = true;
        chasing = true; 
       
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth.instance.DamagePlayer(damage);

    }
    
    void OnTriggerStay(Collider other)
    {
        PlayerHealth.instance.DamagePlayer(damage);

    }
}

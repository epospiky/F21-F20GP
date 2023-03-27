using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 10f;
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

    public Transform[] waypoints;
    private int currentWaypoint = 0;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    private float timeSinceLastShot;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update()
    {
        if(player == null) { return; }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseDistance)
        {
            navMeshAgent.SetDestination(player.position);

            if (distanceToPlayer <= shootDistance)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotateSpeed * Time.deltaTime);
                timeSinceLastShot += Time.deltaTime;
                Shoot();
            }
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
        }
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

    void StopParticleEffect()
    {
        bulletParticles.Stop();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.TakeDamage(bulletDamage);
            }
        }
    }
}

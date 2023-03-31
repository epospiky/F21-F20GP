using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifespan = 30f;
    private float lifeTimer = 0f;
    public AudioClip shootingSound;
    public GameObject player;


    private void Start()
    {
        AudioSource.PlayClipAtPoint(shootingSound, transform.position);
    }

    void Update()
    {
        // Move the bullet forward
        //Vector3 direction = player.transform.position - transform.position;
        //direction.Normalize();

        // Move the game object in the direction of the target game object
       // transform.Translate(direction * speed * Time.deltaTime);

        // Increment the timer and destroy the bullet when its lifespan is over
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifespan)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If the bullet collides with a game object that has a Health script, damage it and destroy the bullet
        PlayerController health = other.gameObject.GetComponent<PlayerController>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

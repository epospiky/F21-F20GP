using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    public float lifespan = 30f;
    private float lifeTimer = 0f;
    public AudioClip shootingSound;


    private void Start()
    {
        AudioSource.PlayClipAtPoint(shootingSound, transform.position);
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

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

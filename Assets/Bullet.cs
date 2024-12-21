using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        {
            // Get the bullet's position in screen space
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

            // Check if the bullet is outside the screen bounds
            if (screenPosition.x < 0 || screenPosition.x > Screen.width ||
                screenPosition.y < 0 || screenPosition.y > Screen.height)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnBecameInvisible()
    {
        // Destroy the bullet when it goes off-screen
        Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collides with another object tagged as "Bullet"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnBecameInvisible();
        }
    }
}

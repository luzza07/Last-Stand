using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    int health=2;
    [SerializeField]
    float movespeed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This method is called when the collider/trigger enters another collider
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collides with another object tagged as "Bullet"
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Reduce health by 1
            health -= GameManager.gameManager.PlayerDamage;

            // Check if the health reaches zero and destroy the enemy
            if (health <= 0)
            {
                Destroy(gameObject);  // Destroy the enemy object
            }
        }// Check if the enemy collides with another object tagged as "Bullet"
        
    }

}

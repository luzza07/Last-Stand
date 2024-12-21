using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;  // Reference to the player's position
    public float moveSpeed = 3f;  // Speed at which the enemy moves towards the player
    public float avoidRadius = 1f; // Radius in which the enemy will start avoiding other enemies
    public float avoidStrength = 2f; // How strongly the enemy will avoid other enemies

    private void Start()
    {
        // Automatically find the player object by tag and get its transform
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform; // Set the player reference
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure there is an object with the 'Player' tag.");
        }
    }

    private void Update()
    {
        // If the player exists, move the enemy towards the player's position
        if (player != null&&GameManager.gameManager.gameOver!=true)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Check for other enemies in the area
            Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, avoidRadius);

            Vector3 avoidance = Vector3.zero;

            // Loop through all nearby enemies and avoid them
            foreach (Collider2D enemy in nearbyEnemies)
            {
                if (enemy.CompareTag("Enemy") && enemy != GetComponent<Collider2D>())
                {
                    // Calculate avoidance vector (avoidance is the opposite direction)
                    Vector3 toEnemy = transform.position - enemy.transform.position;
                    avoidance += toEnemy.normalized / toEnemy.magnitude;
                }
            }

            // Apply avoidance to the original direction
            direction += avoidance * avoidStrength;

            // Normalize the direction to maintain consistent speed
            direction = direction.normalized;

            // Apply movement
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }


}

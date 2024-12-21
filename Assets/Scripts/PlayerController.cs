using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove = true;
    public bool canAim = true;
    public GameObject bulletPrefab; // Bullet prefab
    public Transform firePoint;     // The point where bullets are spawned
    public float bulletSpeed = 40f; // Speed of the bullet

    private bool isShooting = false;  // Flag to prevent multiple simultaneous shots

    public static PlayerController playerController = null;
    private void Awake()
    {
        if (!playerController)
        {
            playerController = this;
        }
    }

    void Start()
    {
        // Start the shooting coroutine immediately when the game starts
        StartCoroutine(AutoShoot());
    }

    void Update()
    {
        if (canMove)
        {
            Move();
        }
        if (canAim)
        {
            AimWithMouse();
        }
    }

    void Move()
    {
        // Get raw input for immediate response
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        // Combine inputs into a direction vector
        Vector3 direction = new Vector3(inputX, inputY, 0);

        // Normalize direction to maintain consistent speed
        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }

        // Apply movement
        transform.position += direction * GameManager.gameManager.PlayerMoveSpeed * Time.deltaTime;
    }

    void AimWithMouse()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse
        Vector3 direction = mousePosition - transform.position;

        // Ignore Z-axis
        direction.z = 0;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the player's Z-axis
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator AutoShoot()
    {
        while (canAim) // Continuously shoot as long as the game is running
        {
            Shoot();  // Call the shoot method

            // Wait for the specified interval before shooting again
            yield return new WaitForSeconds(GameManager.gameManager.ShootInterval);
        }
    }

    void Shoot()
    {
        // Instantiate the bullet at the fire point's position with the fire point's rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Calculate the direction based on the fire point's rotation
        Vector3 direction = firePoint.up; // Use the up direction of the firePoint for rotation

        // Apply force to the bullet to maintain consistent speed
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.gameManager.decreasePlayerHealth();
        }
    }
}

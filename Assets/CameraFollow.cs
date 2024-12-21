using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;      // The player that the camera will follow
    public float smoothSpeed = 0.125f;  // How smoothly the camera follows the player
    public Vector3 offset;        // The initial offset from the player
    public GameObject border;     // The Quad that defines the camera and player boundaries

    private Vector3 minBoundary;  // Minimum boundary (left, bottom)
    private Vector3 maxBoundary;  // Maximum boundary (right, top)
    private float initialZ;       // To store the initial Z position of the camera

    private Camera cam;           // The camera component
    private float camWidth;       // Camera width in world units
    private float camHeight;      // Camera height in world units

    void Start()
    {
        // Store the initial Z position of the camera to prevent it from moving on the Z axis
        initialZ = transform.position.z;

        // Get the camera component
        cam = Camera.main;

        if (border != null)
        {
            // Get the bounds of the "Border" quad
            Renderer borderRenderer = border.GetComponent<Renderer>();
            if (borderRenderer != null)
            {
                // Get the boundary corners in world space (min and max points)
                minBoundary = borderRenderer.bounds.min;
                maxBoundary = borderRenderer.bounds.max;
            }
        }

        // Calculate the camera's width and height in world space
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the target position of the camera based on the player's position
            Vector3 targetPosition = player.position + offset;

            // Clamp the camera's position to ensure it stays within the boundary
            // The camera needs to stay fully within the border, taking into account the camera's viewport size
            float clampedX = Mathf.Clamp(targetPosition.x, minBoundary.x + camWidth / 2, maxBoundary.x - camWidth / 2);
            float clampedY = Mathf.Clamp(targetPosition.y, minBoundary.y + camHeight / 2, maxBoundary.y - camHeight / 2);

            // Set the Z position to the initial value (no change on the Z axis)
            float clampedZ = initialZ;

            // Apply the smooth transition to the camera position
            Vector3 smoothedPosition = new Vector3(clampedX, clampedY, clampedZ);
            transform.position = Vector3.Lerp(transform.position, smoothedPosition, smoothSpeed);
        }
    }
}

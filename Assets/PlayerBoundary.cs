using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    public GameObject border; // The Quad that defines the boundary
    private Vector3 minBoundary; // Minimum boundary (left, bottom)
    private Vector3 maxBoundary; // Maximum boundary (right, top)

    void Start()
    {
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
    }

    void Update()
    {
        // Get the current position of the player
        Vector3 playerPosition = transform.position;

        // Clamp the player's position within the boundary
        float clampedX = Mathf.Clamp(playerPosition.x, minBoundary.x, maxBoundary.x);
        float clampedY = Mathf.Clamp(playerPosition.y, minBoundary.y, maxBoundary.y);

        // Apply the clamped position back to the player
        transform.position = new Vector3(clampedX, clampedY, playerPosition.z);
    }
}

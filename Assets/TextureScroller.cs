using UnityEngine;

public class TextureScroller : MonoBehaviour
{
    public Camera cameraToFollow; // Reference to the camera
    public float scrollSpeedX = 0.1f; // Speed of texture scrolling in the X direction
    public float scrollSpeedY = 0.1f; // Speed of texture scrolling in the Y direction

    private Material quadMaterial; // Material of the quad
    private Vector3 previousCameraPosition; // To track camera's previous position

    void Start()
    {
        if (cameraToFollow == null)
        {
            cameraToFollow = Camera.main; // Default to the main camera if not set
        }

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            quadMaterial = renderer.material; // Get the material of the quad
        }

        // Initialize the previous camera position
        previousCameraPosition = cameraToFollow.transform.position;
    }

    void LateUpdate()
    {
        if (quadMaterial != null && cameraToFollow != null)
        {
            // Calculate the camera's movement since the last frame
            Vector3 cameraMovement = cameraToFollow.transform.position - previousCameraPosition;

            // Update the texture offset based on camera movement and scroll speed
            Vector2 textureOffset = quadMaterial.mainTextureOffset;
            textureOffset.x += cameraMovement.x * scrollSpeedX;
            textureOffset.y += cameraMovement.y * scrollSpeedY;

            // Apply the updated offset to the material
            quadMaterial.mainTextureOffset = textureOffset;

            // Update the previous camera position
            previousCameraPosition = cameraToFollow.transform.position;
        }
    }
}

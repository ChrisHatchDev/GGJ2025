using UnityEngine;

public class SwordController : MonoBehaviour
{
    public float ZOffset = 8;
    
    void Update()
    {
        TrackMousePosition();
    }

    private void TrackMousePosition()
    {
        // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;

        // Convert the screen space position to world space
        mousePosition.z = Camera.main.nearClipPlane + ZOffset; // Set the z position further from the camera
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Update the game object's position to the world position
        transform.position = worldPosition;
    }
}

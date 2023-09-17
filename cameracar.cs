using UnityEngine;

public class CameraViewportController : MonoBehaviour
{
    public string targetCameraName; // Set this in the Inspector to specify the camera's name.

    public void Update()
    {
        // Find the camera by name or reference it directly if you have a reference.
        Camera targetCamera = GameObject.Find(targetCameraName).GetComponent<Camera>();

        if (targetCamera != null)
        {
            // Get the camera's current viewport values
            Rect viewportRect = targetCamera.rect;

            // Check for arrow key inputs
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                viewportRect.x = 0.5f;
                viewportRect.width = 0.5f;
                viewportRect.height = 1f;

                if ((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.E) ||
                    Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space)
                    || Input.GetKey(KeyCode.Tab)))
                {
                    viewportRect.x = 0.75f;
                    viewportRect.width = 0.25f;
                    viewportRect.height = 1f;

                }
            }
            
            



            // Ensure viewport values stay within valid range (0 to 1)
            viewportRect.x = Mathf.Clamp(viewportRect.x, 0f, 1f);
            viewportRect.y = Mathf.Clamp(viewportRect.y, 0f, 1f);

            // Update the camera's viewport values
            targetCamera.rect = viewportRect;
        }
        
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helil : MonoBehaviour
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
            if (Input.GetKey(KeyCode.K))
            {
                viewportRect.x = 0f;
                viewportRect.width = 0f;
                viewportRect.height = 0f;

            }
            else if (Input.GetKey(KeyCode.L))
            {
                viewportRect.x = 0f;
                viewportRect.width = 1f;
                viewportRect.height = 1f;

            }
            else if (Input.GetKeyUp(KeyCode.Y))
            {
                viewportRect.x = 0f;
                viewportRect.width = 0f;
                viewportRect.height = 0f;

            }
            // Ensure viewport values stay within valid range (0 to 1)
            viewportRect.x = Mathf.Clamp(viewportRect.x, 0f, 1f);
            viewportRect.y = Mathf.Clamp(viewportRect.y, 0f, 1f);

            // Update the camera's viewport values
            targetCamera.rect = viewportRect;
        }

    }
}


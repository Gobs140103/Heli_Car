using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public GameObject car;
    public GameObject helicopter;
    public GameObject carPanel;
    public GameObject helicopterPanel;
    public SkinnedMeshRenderer playerMeshRenderer;

    // The distance at which the panels should become active
    public float activationDistance = 3f;

    private bool isNearCar = false;
    private bool isNearHelicopter = false;

    private void Update()
    {
        // Link the "vertical" parameter in the animator to the player's vertical input
        anim.SetFloat("vertical", Input.GetAxis("MoveForward"));
        // Link the "horizontal" parameter in the animator to the player's horizontal input
        anim.SetFloat("horizontal", Input.GetAxis("Move"));

        // Calculate the distance between the player and the car
        float distanceToCar = Vector3.Distance(transform.position, car.transform.position);

        // Check if the player is near the car
        if (distanceToCar <= activationDistance)
        {
            // Set the flag to indicate that the player is near the car
            isNearCar = true;
            // Activate the car panel
            carPanel.SetActive(true);
        }
        else
        {
            // Set the flag to indicate that the player is not near the car
            isNearCar = false;
            // Deactivate the car panel
            carPanel.SetActive(false);
        }

        // Calculate the distance between the player and the helicopter
        float distanceToHelicopter = Vector3.Distance(transform.position, helicopter.transform.position);

        // Check if the player is near the helicopter
        if (distanceToHelicopter <= activationDistance)
        {
            // Set the flag to indicate that the player is near the helicopter
            isNearHelicopter = true;
            // Activate the helicopter panel
            helicopterPanel.SetActive(true);
           
            
        }
        else
        {
            // Set the flag to indicate that the player is not near the helicopter
            isNearHelicopter = false;
            // Deactivate the helicopter panel
            helicopterPanel.SetActive(false);
        }

        // Check for player input to interact with the car
        if (isNearCar && Input.GetKey(KeyCode.K))
        {
            // Move the player object to the car's hierarchy and set it inactive

            playerMeshRenderer.enabled = false;
            carPanel.SetActive(false);

            // Perform the interaction with the car (e.g., enter or interact with it)
            // You can add your car interaction logic here
        }

        // Check for player input to interact with the helicopter
        if (isNearHelicopter && Input.GetKey(KeyCode.L))
        {
            // Move the player object to the helicopter's hierarchy and set it inactive

            playerMeshRenderer.enabled = false;

            helicopterPanel.SetActive(false);
            
            // Perform the interaction with the helicopter (e.g., enter or interact with it)
            // You can add your helicopter interaction logic here
        }
        if (Input.GetKey(KeyCode.Y))
        {
            // Move the player object to the helicopter's hierarchy and set it inactive

            playerMeshRenderer.enabled = true;

   

            // Perform the interaction with the helicopter (e.g., enter or interact with it)
            // You can add your helicopter interaction logic here
        }


    }
}

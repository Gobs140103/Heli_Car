using UnityEngine;

public class GarageController : MonoBehaviour
{
    public Animator garageAnimator; // Reference to the garage's animator
    public float detectionRadius = 5f; // Radius to detect player proximity
    public Transform player; // Reference to the player's transform
    private bool isPlayerNear = false;

    private void Start()
    {
        garageAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is within the detection radius
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            if (!isPlayerNear)
            {
                // Player just entered the detection radius
                isPlayerNear = true;
                // Trigger the "Open" animation
                garageAnimator.SetTrigger("open");
            }
        }
        
    }
}

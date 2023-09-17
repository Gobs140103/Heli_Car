using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pause;
    public GameObject pauseMenu; // Reference to the entire pause menu panel
    
    public void ResumeGame()
    {
        // Turn off the pause menu (including panels)
        pause.SetActive(false);
        pauseMenu.SetActive(true);

        
        // You may also want to resume game logic here if it was paused.


    }
}

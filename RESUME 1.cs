using UnityEngine;
using UnityEngine.UI;

public class RESUME : MonoBehaviour
{
    public GameObject pause;
    public GameObject pauseMenu; // Reference to the entire pause menu panel
    public void ResumeGame()
    {
        // Turn off the pause menu (including panels)
        pauseMenu.SetActive(false);
        pause.SetActive(true);
        
        

    }
}



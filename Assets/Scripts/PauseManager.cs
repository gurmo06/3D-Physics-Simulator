using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the pause menu GameObject in the scene
    public StarterAssetsInputs playerInput; // Reference to the player's input script
    public GameObject escText; // Reference to the escape menu text

    // Start is called before the first frame update
    void Start()
    {
        // Ensure that the references are set in the Unity Editor
        if (pauseMenu == null)
        {
            Debug.LogError("Pause Menu reference is not set!");
            return;
        }

        if (playerInput == null)
        {
            Debug.LogError("Player Input reference is not set!");
            return;
        }

        if (escText == null)
        {
            Debug.LogError("Escape Menu Text Field reference is not set!");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the Escape key is pressed, toggle the pause state
            TogglePause();
        }
    }

    void TogglePause()
    {
        // If the game is paused
        if (Time.timeScale == 0)
        {
            // Resume the game
            Time.timeScale = 1;
            // Unlock the cursor to allow mouse movement
            Cursor.lockState = CursorLockMode.None;
            // Hide the cursor
            Cursor.visible = false;
            // Deactivate the pause menu
            pauseMenu.SetActive(false);
            // Activate the Escape Text
            escText.SetActive(true);
            // Enable the player's use of the cursor to look
            playerInput.cursorInputForLook = true;
        }
        else
        {
            // Pause the game
            Time.timeScale = 0;
            // Unlock the cursor to allow mouse movement
            Cursor.lockState = CursorLockMode.None;
            // Show the cursor
            Cursor.visible = true;
            // Deactivate the Escape Text
            escText.SetActive(false);
            // Activate the pause menu
            pauseMenu.SetActive(true);
            // Disable the player's use of the cursor to look
            playerInput.cursorInputForLook = false;
            //Set the look velocity to zero
            playerInput.look = new Vector2(0f, 0f);
        }
    }
}
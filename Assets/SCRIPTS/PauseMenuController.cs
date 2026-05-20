  using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("Canvas References")]
    [SerializeField] private GameObject menuCanvas;        // Your main HUD / Menu Canvas
    [SerializeField] private GameObject pauseOptionCanvas;  // The options canvas with Resume/Return

    private bool isPaused = false;

    void Update()
    {
        // Optional: Toggle pause menu if you press the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                OpenOptions();
        }
    }

    // Call this when the Options Button is clicked
    public void OpenOptions()
    {
        isPaused = true;
        
        // Hide standard HUD (if needed), show the Options canvas
        if (menuCanvas != null) menuCanvas.SetActive(false);
        if (pauseOptionCanvas != null) pauseOptionCanvas.SetActive(true);

        // Freeze game time
        Time.timeScale = 0f;
        
        // Unlock cursor so player can click buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Call this when the Resume Button is clicked
    public void ResumeGame()
    {
        isPaused = false;

        // Hide Options canvas, bring back the main gameplay HUD
        if (pauseOptionCanvas != null) pauseOptionCanvas.SetActive(false);
        if (menuCanvas != null) menuCanvas.SetActive(true);

        // Unfreeze game time
        Time.timeScale = 1f;

        // Re-lock cursor for FPS gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Call this when the Return Button is clicked
    public void ReturnToMainMenu()
    {
        // Crucial: Always reset time scale before switching scenes, 
        // otherwise the next scene might start completely frozen!
        Time.timeScale = 1f;
        
        // Replace "Main menu" with the exact name of your main menu scene asset
        SceneManager.LoadScene("Main menu"); 
    }
}

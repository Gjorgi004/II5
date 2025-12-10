using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    void Awake()
    {
        
        if (pauseMenu == null)
        {
      //      pauseMenu  GameObject.Find("PauseMenu"); 
        }

        if (pauseMenu == null)
        {
            Debug.LogWarning("[PauseMenu] pauseMenu reference is NULL. Assign the UI panel in Inspector.");
        }
        else
        {
           
            pauseMenu.SetActive(false);
            Debug.Log("[PauseMenu] Found pauseMenu: " + pauseMenu.name + " (start hidden).");
        }
    }

    public void Pause()
    {
        if (pauseMenu == null)
        {
            Debug.LogError("[PauseMenu][Pause] pauseMenu is NULL. The button call will do nothing.");
            return;
        }

        pauseMenu.SetActive(true);
        Time.timeScale = 0f; 
        Debug.Log("[PauseMenu] Paused. pauseMenu.SetActive(true). Time.timeScale = 0");
    }

    public void Resume()
    {
        if (pauseMenu == null)
        {
            Debug.LogError("[PauseMenu][Resume] pauseMenu is NULL.");
            return;
        }

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("[PauseMenu] Resumed. pauseMenu.SetActive(false). Time.timeScale = 1");
    }

    public void Home()
    {
        Debug.Log("[PauseMenu] Loading scene: Main Menu");
        
        SceneManager.LoadScene("Main Menu");
    }
}

using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject inventoryCanvas;

    void Update()
    {
        // Detect the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        bool isActive = !menuCanvas.activeSelf;
        menuCanvas.SetActive(isActive);

            // Close inventory too if the main menu is closed
            if (inventoryCanvas != null) inventoryCanvas.SetActive(false);
        }
    

    // This function will be called by your Button
    public void OpenInventory()
    {
        if (inventoryCanvas != null)
        {
            inventoryCanvas.SetActive(true);
            menuCanvas.SetActive(false); // Hide the main menu
        }
    }
}
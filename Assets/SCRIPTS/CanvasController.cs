using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public GameObject menuCanvas;
    public GameObject inventoryCanvas;
    public GameObject optionsIcon;
    public GameObject inventoryIcon;



    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        bool isActive = !menuCanvas.activeSelf;
        menuCanvas.SetActive(isActive);

        if (inventoryCanvas != null) inventoryCanvas.SetActive(false);
    }


    public void OpenInventory()
    {
        if (inventoryCanvas != null)
        {

            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);

        }
    }
}
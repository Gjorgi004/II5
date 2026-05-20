using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 5f;

    void Update()
    {
        // Check if the 'E' key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key was pressed!"); 
            CheckForDrawer();
        }
    }

    void CheckForDrawer()
    {
        RaycastHit hit;
        
        // Shoot the ray from the camera forward
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
            
            // Try to find the DrawerController on the object we hit
            DrawerController drawer = hit.collider.GetComponent<DrawerController>();
            
            if (drawer != null)
            {
                drawer.ToggleDrawer();
            }
            else
            {
                Debug.Log("Hit something, but it doesn't have a DrawerController script!");
            }
        }
        else 
        {
            Debug.Log("Raycast didn't hit anything. Move closer?");
        }
    }
} // This is the final bracket that ends the class
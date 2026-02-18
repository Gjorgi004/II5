using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float reachDistance = 3f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, reachDistance))
            {
               
                if (hit.collider.CompareTag("Door"))
                {
                    DoorController door = hit.collider.GetComponent<DoorController>();
                    if (door != null) {
                        door.ToggleDoor();
                    }
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kryz.CharacterStats.Examples;

interface IInteractable {
    public void Interact();
}

public class Interaction : MonoBehaviour
{

    public EquippableItem Gun;
    public Inventory inventory;

    public Transform InteractorSource;
    public float InteractRange;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E Pressed"); // Step 1
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                Debug.Log("Hit something: " + hitInfo.collider.name); // Step 2
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    Debug.Log("Found IInteractable!"); // Step 3
                    interactObj.Interact();
                }
            }
        }
    }
}


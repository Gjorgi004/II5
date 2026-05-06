using Kryz.CharacterStats.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar_Interact : MonoBehaviour, IInteractable
{

    [SerializeField] public GameObject leftDoor;
    [SerializeField] public GameObject rightDoor;
    [SerializeField] private Item keyitem;
    public Inventory inventory;

    [SerializeField] private Vector3 leftMoveDirection = new Vector3(-1, 0, 0);
    [SerializeField] private Vector3 rightMoveDirection = new Vector3(1, 0, 0);
    [SerializeField] private float distance = 3f;
    [SerializeField] private float speed = 2f;


    private Vector3 leftTargetPos;
    private Vector3 rightTargetPos;
    public bool shouldOpen;

    void Start()
    {
        if (leftDoor == null || rightDoor == null)
        {
            Debug.LogError($"<color=red>Missing Door Reference on {gameObject.name}!</color> Please drag the doors into the slots.", gameObject);
            return;
        }

        leftTargetPos = leftDoor.transform.position + leftDoor.transform.TransformDirection(leftMoveDirection) * distance;
        rightTargetPos = rightDoor.transform.position + rightDoor.transform.TransformDirection(rightMoveDirection) * distance;
    }

    void Update()
    {
        if (shouldOpen)
        {
            leftDoor.transform.position = Vector3.MoveTowards(leftDoor.transform.position, leftTargetPos, speed * Time.deltaTime);
            rightDoor.transform.position = Vector3.MoveTowards(rightDoor.transform.position, rightTargetPos, speed * Time.deltaTime);

            // Optimization: Stop running Update once doors are in place
            if (Vector3.Distance(leftDoor.transform.position, leftTargetPos) < 0.001f)
            {
                shouldOpen = false;
            }
        }
    }

    public void Interact()
    {

        if (inventory != null)
        {
            if (inventory.HasItem(keyitem))
            {
                Debug.Log("Opened");
                shouldOpen = true;
            }
        }
    }
}

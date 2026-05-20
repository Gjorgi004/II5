using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Door : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;

    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Coroutine currentCoroutine;

    void Start()
    {
        // Set up the rotations based on the starting position
        closedRotation = transform.localRotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);

        // Force Rigidbody settings so physics don't block the movement
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; 
        rb.useGravity = false;
    }

    // This is the specific method the player script MUST call
    public void ToggleDoor()
    {
        Debug.Log("ToggleDoor called on: " + gameObject.name);
        isOpen = !isOpen;
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(RotateDoor(isOpen ? openRotation : closedRotation));
    }

    IEnumerator RotateDoor(Quaternion targetRotation)
{
    Debug.Log("Moving to: " + targetRotation.eulerAngles); // ADD THIS LINE
    while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.1f)
    {
        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation, 
            targetRotation, 
            openSpeed * 100f * Time.deltaTime
        );
        yield return null;
    }
    transform.localRotation = targetRotation;
    currentCoroutine = null;
}
}
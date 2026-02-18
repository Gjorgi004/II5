using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public bool isOpen = false;
    public Vector3 openOffset = new Vector3(0, 3, 0); 
    public float autoCloseDelay = 3f; 
    
    private Vector3 closedPos;
    private Vector3 targetPos;

    void Start() {
        closedPos = transform.position;
        targetPos = closedPos;
    }

    void Update() {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 5f);
    }

    public void ToggleDoor() {
        if (!isOpen) {
            OpenDoor();
        } else {
            CloseDoor();
        }
    }

    public void OpenDoor() {
        isOpen = true;
        targetPos = closedPos + openOffset;
        
    
        StopAllCoroutines(); 
        StartCoroutine(AutoCloseTimer());
    }

    public void CloseDoor() {
        isOpen = false;
        targetPos = closedPos;
    }

    IEnumerator AutoCloseTimer() {
        yield return new WaitForSeconds(autoCloseDelay);
        CloseDoor();
    }
}

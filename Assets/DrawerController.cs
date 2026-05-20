using UnityEngine;

public class DrawerController : MonoBehaviour
{
    [Header("Settings")]
    public Vector3 openOffset = new Vector3(0, 0, 0.5f); // Distance to slide
    public float speed = 3f;

    private bool isOpen = false;
    private Vector3 closedPos;
    private Vector3 targetPos;

    void Start()
    {
        closedPos = transform.localPosition;
        targetPos = closedPos;
    }

    void Update()
    {
        // Smoothly move the drawer toward the target position
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * speed);
    }

    public void ToggleDrawer()
    {
        isOpen = !isOpen;
        targetPos = isOpen ? closedPos + openOffset : closedPos;
    }
}
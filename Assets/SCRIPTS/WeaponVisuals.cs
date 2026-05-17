using UnityEngine;

public class WeaponVisuals : MonoBehaviour
{
    [Header("Sway Settings")]
    public float swayAmount = 0.02f;
    public float maxSwayAmount = 0.06f;
    public float smoothAmount = 6f;

    [Header("ADS Settings")]
    public Vector3 hipPosition;
    public Vector3 aimPosition;
    public float adsSpeed = 10f;

    [Header("Bobbing Settings")]
    public float bobSpeed = 10f;
    public float bobAmount = 0.05f;
    private float timer = 0;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        
        Vector3 targetPos = Input.GetMouseButton(1) ? aimPosition : initialPosition;

        
        float moveX = -Input.GetAxis("Mouse X") * swayAmount;
        float moveY = -Input.GetAxis("Mouse Y") * swayAmount;
        moveX = Mathf.Clamp(moveX, -maxSwayAmount, maxSwayAmount);
        moveY = Mathf.Clamp(moveY, -maxSwayAmount, maxSwayAmount);
        Vector3 finalSway = new Vector3(moveX, moveY, 0);

        
        Vector3 bobOffset = Vector3.zero;
        
        
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
        {
            timer += Time.deltaTime * bobSpeed;
            bobOffset.x = Mathf.Cos(timer / 2) * bobAmount; 
            bobOffset.y = Mathf.Sin(timer) * bobAmount;     
        }
        else
        {
            timer = 0; 
        }

        
        Vector3 finalPos = targetPos + finalSway + bobOffset;
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPos, Time.deltaTime * smoothAmount);
    }
}

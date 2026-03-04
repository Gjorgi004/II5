using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [Header("Sway Settings")]
    public float amount = 0.02f;
    public float maxAmount = 0.06f;
    public float smoothAmount = 6f;

    [Header("ADS Settings")]
    public Vector3 hipPosition;
    public Vector3 aimPosition;
    public float adsSpeed = 10f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalSwayPos = new Vector3(movementX, movementY, 0);
        
        
        Vector3 targetPos = Input.GetMouseButton(1) ? aimPosition : initialPosition;

        
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos + finalSwayPos, Time.deltaTime * smoothAmount);
    }
}

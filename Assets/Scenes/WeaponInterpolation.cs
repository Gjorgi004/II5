using UnityEngine;

public class WeaponInterpolation : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.05f;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            target.position,
            ref velocity,
            smoothTime
        );

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            target.rotation,
            10f * Time.deltaTime
        );
    }
}

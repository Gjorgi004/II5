using UnityEngine;

public class WeaponInterpolation : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.05f;

    private Vector3 velocity = Vector3.zero;

  //  void FixedUpdate()
   // {
    //    transform.position = Vector3.SmoothDamp(
    //        transform.position,
     //       target.position,
    //        ref velocity,
      //      smoothTime
     //   );

      //  transform.rotation = Quaternion.Slerp(
       //     transform.rotation,
       //     target.rotation,
       //     10f * Time.deltaTime
       // );
  //  }

    void LateUpdate()
    {
        if (target == null) return;

        // Faster, smoother alternative to SmoothDamp
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 20f);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * 20f);
    }
}

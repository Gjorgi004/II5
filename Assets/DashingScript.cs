using UnityEngine;

public class DashingScript : MonoBehaviour
{

    public Transform cam;
    public float dashforce = 150f;
    public float dashCooldown = 1f;
    public bool isDashing;

    private Rigidbody rb;
    private float lastDash;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDash + dashCooldown)
        {
            isDashing = true;
            Vector3 dir = cam.forward;
            dir.y = 0f;
            dir.Normalize();

            rb.AddForce(dir * dashforce, ForceMode.Impulse);
            lastDash = Time.time;
        }

        if(Time.time > lastDash + 0.2f)
        {
            isDashing = false;
        }

    }


}

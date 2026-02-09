using UnityEngine;

public class Zoom : MonoBehaviour
{
    private Camera cam; 
    public float defaultFOV = 60f;
    public float maxZoomFOV = 15f;

    [Range(0f, 1f)]
    public float currentZoom;

    public float sensitivity = 1f;

    void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam != null)
        {
            defaultFOV = cam.fieldOfView;
        }
    }

    void Update()
    {
        currentZoom += Input.mouseScrollDelta.y * sensitivity * 0.2f;
        currentZoom = Mathf.Clamp01(currentZoom);
        
        if (cam != null)
        {
            cam.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
        }
    }
}
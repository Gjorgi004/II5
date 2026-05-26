using UnityEngine;

public class ScrollCredits : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 100f; // Speed of the scroll
    [SerializeField] private float stopPositionHeight = 1500f; // Y position where it stops/disappears

    private RectTransform rectTransform;

    void Start()
    {
        // Get the RectTransform component of the UI container
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Move the container upward over time
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        // Optional: Stop moving or load a new scene once it goes past the stop height
        if (rectTransform.anchoredPosition.y >= stopPositionHeight)
        {
            enabled = false; // Stops the script from updating
            Debug.Log("Credits Finished!");
            // You can add scene management here to go back to the main menu:
            // UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}

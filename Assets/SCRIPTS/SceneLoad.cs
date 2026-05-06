using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class SceneLoad : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the Player
        if (other.CompareTag(playerTag))
        {
            Debug.Log("Loading: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
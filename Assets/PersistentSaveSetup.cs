using UnityEngine;

public class PersistentSaveSetup : MonoBehaviour
{
    private static PersistentSaveSetup instance;

    void Awake()
    {
        // Singleton Pattern: Ensures only ONE save system exists, even if we reload the main menu
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object alive when changing scenes!
        }
        else
        {
            Destroy(gameObject); // Delete duplicates that spawn when coming back to this scene
        }
    }
}
using UnityEngine;

public class PersistentSaveSetup : MonoBehaviour
{
    private static PersistentSaveSetup instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // This keeps it alive!
        }
        else
        {
            // If we loop back to Scene 1, destroy the duplicate layout
            Destroy(gameObject);
        }
    }
}
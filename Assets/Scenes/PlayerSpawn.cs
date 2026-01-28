using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static Vector3 spawnPosition;

    public void LoadLab()
    {
        spawnPosition = Vector3.zero; 
        SceneManager.LoadScene("scene3");
    }
}

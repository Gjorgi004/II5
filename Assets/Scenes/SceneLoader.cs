using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static string spawnPointName;

    public void LoadForest()
    {
        spawnPointName = "ForestSpawnPoint";
        SceneManager.LoadScene("ForestScene");
    }

    public void LoadLab()
    {
        spawnPointName = "LabSpawnPoint";
        SceneManager.LoadScene("scene3");
    }
    public void RuinsRuins()
    {
        spawnPointName = "RuinsSpawnPoint";
        SceneManager.LoadScene("Ruins");
    }
}

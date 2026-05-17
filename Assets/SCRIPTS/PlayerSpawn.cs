using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Start()
    {
        if (!string.IsNullOrEmpty(SceneLoader.spawnPointName))
        {
            GameObject spawn =
                GameObject.Find(SceneLoader.spawnPointName);

            if (spawn != null)
            {
                transform.position = spawn.transform.position;
            }
        }
    }
}

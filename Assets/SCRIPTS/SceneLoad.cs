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

            SaveManager saveManager = FindObjectOfType<SaveManager>();
            saveManager.playerInventory = FindObjectOfType<Kryz.CharacterStats.Examples.Inventory>();
            saveManager.equipmentPanel = FindObjectOfType<Kryz.CharacterStats.Examples.EquipmentPanel>();
            saveManager.LoadGameProgress();

            Debug.Log("Loading: " + sceneToLoad);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
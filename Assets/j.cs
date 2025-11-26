using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButtonHandler : MonoBehaviour
{
    public Button startbutton; 
    public string h;   

    public void OnStartButtonClicked()
    {
        
        startbutton.gameObject.SetActive(false);

        
        SceneManager.LoadScene(h);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject optionCanvas;
     public void PlayGame()
     {
        Debug.Log("pressed");
        SceneManager.LoadSceneAsync(1);
     }
     public void OpenOptions()
    {
        mainCanvas.SetActive(false);
        optionCanvas.SetActive(true);
    }
    public void CloseOptions()
    {
        mainCanvas.SetActive(true);
        optionCanvas.SetActive(false);
    }



}

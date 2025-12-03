using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsSettings : MonoBehaviour
{
    public void GoSettings()
    {
       SceneManager.LoadSceneAsync(1);
    }
}

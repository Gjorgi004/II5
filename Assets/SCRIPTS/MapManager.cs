using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject mapCamera; 
    public GameObject playerIcon; 

    void Start()
    {
        
        mapCamera.SetActive(false);
        if(playerIcon != null) playerIcon.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleMap(true);
        }
        
        
        if (Input.GetKeyUp(KeyCode.Q))
        {
            ToggleMap(false);
        }
    }

    void ToggleMap(bool show)
    {
        mapCamera.SetActive(show);
        if(playerIcon != null) playerIcon.SetActive(show);

        
        Time.timeScale = show ? 0.2f : 1.0f;
    }
}

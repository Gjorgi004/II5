using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSouls : MonoBehaviour
{

    public int currentsouls = 0;
    public TextMeshProUGUI text;

    void Start()
    {
        UpdateSoulUI();
    }

    public void AddSouls(int amount)
    {
        currentsouls += amount;
        UpdateSoulUI();
        Debug.Log($"Collected {amount} souls! Total: {currentsouls}");
    }
  

    private void UpdateSoulUI()
    {
        if (text != null)
        {
            text.text = currentsouls.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextboxScript : MonoBehaviour
{

    [Header("UI Reference Elements")]
    public GameObject uiWindowBox;       
    public TextMeshProUGUI tutorialText;
    public GameObject hoverBox;

    [Header("Tutorial Content")]
    public string messageToShow = "Press [SHIFT] to dash through incoming attacks.\nTiming grants Invincibility Frames.";

    private bool isUiActive = false;
    private bool playerIsNearby = false;

    public void Start()
    {
        if (uiWindowBox != null) uiWindowBox.SetActive(false);
    }

    public void Update()
    {
        if (playerIsNearby)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleTutorialUI();
            }
        }
    }

    private void ToggleTutorialUI()
    {
        isUiActive = !isUiActive; // Flips the true/false switch

        if (uiWindowBox != null)
        {
            uiWindowBox.SetActive(isUiActive);
        }

        if (hoverBox != null)
        {
            hoverBox.SetActive(!isUiActive);
        }

        if (isUiActive && tutorialText != null)
        {
            // Set the unique message written in this specific bloodstain's inspector slot
            tutorialText.text = messageToShow;
        }
        hoverBox.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearby = true;
            hoverBox.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNearby = false;
            hoverBox.SetActive(false);

            // Auto-hide the tutorial UI if the player walks away while it's open
            isUiActive = false;
            if (uiWindowBox != null) uiWindowBox.SetActive(false);
        }
    }
}

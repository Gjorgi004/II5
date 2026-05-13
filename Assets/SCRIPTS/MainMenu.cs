using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Keeping the Options Canvas from Upstream
    public GameObject mainCanvas;
    public GameObject optionCanvas;

    // Keeping your Transition/Dialogue variables
    public GameObject whiteScreen;
    public GameObject blackScreen;
    public GameObject whiteScreen1;
    public GameObject Text;
    public Animator animator;
    public DialogueManager dialogueManager;
    public float timetowait = 0f;

    public void PlayGame()
    {
        Debug.Log("pressed");
        whiteScreen.SetActive(true);
        animator.SetTrigger("Transition");
        StartCoroutine(MyWaitRoutine());
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

    IEnumerator MyWaitRoutine()
    {
        yield return new WaitForSeconds(1f);
        whiteScreen1.SetActive(true);
        blackScreen.SetActive(true);
        animator.SetTrigger("BlackTransition");
        yield return new WaitForSeconds(timetowait);
        Text.SetActive(true);
        dialogueManager.StartDialogue();
    }
}
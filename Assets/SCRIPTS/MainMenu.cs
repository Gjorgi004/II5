using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject optionCanvas;

    public GameObject whiteScreen;
    public GameObject blackScreen;
    public GameObject whiteScreen1;
    public GameObject Text;
    public Animator animator;
    public DialogueManager dialogueManager;
    public float timetowait = 0f;
    public AudioSource audioSource;
    public AudioClip typeSound;

    public void PlayGame()
    {
        Debug.Log("pressed");
        whiteScreen.SetActive(true);
        animator.SetTrigger("Transition");
        audioSource.Stop();
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

    public void StartFinalFadeAndLoad()
    {
        StartCoroutine(Finalfaderoutine());
    }

    IEnumerator Finalfaderoutine()
    {
        blackScreen.SetActive(false);
        whiteScreen.SetActive(true);
        whiteScreen1.SetActive(false);
        animator.SetFloat("AnimSpeed", -1f);
        animator.SetTrigger("Transition");
        yield return new WaitForSeconds(1f);
        animator.SetFloat("AnimSpeed", 1f);
        animator.SetTrigger("Transition");
        SceneManager.LoadSceneAsync(1);
    }
}
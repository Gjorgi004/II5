using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text textComponent;
    public float typingSpeed = 0.05f;
    public string[] lines;  
    public AudioSource audioSource;
    public AudioClip typeSound;
    private int index;  
    private bool isTyping = false;
   

    public void StartDialogue()
    {
        if (lines.Length == 0)
        {
            Debug.LogError("You forgot to add lines in the Inspector!");
            return;
        }

        index = 0;
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = "";

        // Convert string to array and loop
        char[] charArray = lines[index].ToCharArray();

        for (int i = 0; i < charArray.Length; i++)
        {
            textComponent.text += charArray[i];
            audioSource.PlayOneShot(typeSound);
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            NextLine();
        }
    }

    public void NextLine()
    {
        if (isTyping) return; // Wait for it to finish typing before allowing next

        if (index < lines.Length - 1)
        {
            index++;
            StopAllCoroutines();
            StartCoroutine(TypeLine());
        }
        else
        {
            MainMenu mainMenu = FindFirstObjectByType<MainMenu>();
            if (mainMenu != null)
            {
                mainMenu.StartFinalFadeAndLoad();
            }
        }
    }
}

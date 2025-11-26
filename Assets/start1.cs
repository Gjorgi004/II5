using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButtonText : MonoBehaviour
{
    public TMP_Text buttonText;

    void Start()
    {
        buttonText.text = "Start";
    }
}

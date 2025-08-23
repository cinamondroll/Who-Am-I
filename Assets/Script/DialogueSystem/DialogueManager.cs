using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text dialogueText;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] GameObject choicePanel;
    [SerializeField] Button choiceButtonPrefabs;
    [SerializeField] Button progressButton;


    [Header("Image")]
    [SerializeField] Image dialogueImage;
    [SerializeField] bool deactivateImage;

    [Header("Audio")]
    [SerializeField] AudioSource voiceAudioSource;
    [SerializeField] AudioSource effectAudioSource;

    [Header("Settings")]
    [SerializeField] float typingSpeed = 0.05f;
    private Dialog currentDialog;
    private int currentNodeIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

}

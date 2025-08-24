using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;  // Untuk TMP_Text
using UnityEngine.UI;
using StarterAssets;  // Untuk Button
using Cinemachine;  // Untuk CinemachineVirtualCamera

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private InputActionReference nextDialogAction; // Referensi untuk NextDialog Action
    [SerializeField] private InputActionReference interactAction; // Referensi untuk Interact Action
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private Button progressButton;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private CinemachineVirtualCamera dialogCamera;
    [SerializeField] private Image dialogImage; 

    private bool isDialogueActive = false;
    private int currentNodeIndex = 0;
    private Dialog currentDialog;  // Menggunakan ScriptableObject Dialog
    private ThirdPersonController mcController;
    private StarterAssetsInputs starterAssetsInputs;
    private PlayerInput playerInput;

    private void Awake()
    {
        mcController = FindObjectOfType<ThirdPersonController>();
        starterAssetsInputs = FindObjectOfType<StarterAssetsInputs>();
        playerInput = FindObjectOfType<PlayerInput>();

        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        if (choicePanel != null) choicePanel.SetActive(false);
    }

    // Fungsi untuk memulai dialog menggunakan Dialog ScriptableObject
    public void StartDialog(Dialog dialog)
    {
        isDialogueActive = true;
        currentDialog = dialog;  // Set currentDialog ke ScriptableObject Dialog
        currentNodeIndex = 0;
        dialoguePanel.SetActive(true);
        DisplayNode(currentNodeIndex);

        // Freeze Input (disable movement & camera controls)
        if (mcController != null) mcController.enabled = false;
        if (starterAssetsInputs != null) starterAssetsInputs.SetInputActive(false);

        // Switch to UIDialog action map
        if (playerInput != null) playerInput.SwitchCurrentActionMap("UIDialog");

        // Enable NextDialog action
        if (nextDialogAction != null)
        {
            nextDialogAction.action.performed += OnNextDialogPerformed;
            nextDialogAction.action.Enable();
        }
    }

    // Fungsi yang dipanggil saat tombol interaksi ditekan (misalnya "Enter")
    private void OnInteractPressed(InputAction inputAction)
    {
        Debug.Log("Interact pressed");
        // Pastikan interaksi yang sesuai terjadi saat tombol ditekan
        if (!isDialogueActive) return;
        OnNextDialog();
    }

    private void OnNextDialogPerformed(InputAction.CallbackContext ctx)
    {
        OnNextDialog();
    }

    private void OnNextDialog()
    {
        if (currentNodeIndex < currentDialog.nodes.Count - 1)
        {
            currentNodeIndex++;
            DisplayNode(currentNodeIndex);
        }
        else
        {
            EndDialog();
        }
    }

    private void DisplayNode(int index)
    {
        if (currentDialog == null || currentDialog.nodes.Count == 0) return;
        var node = currentDialog.nodes[index];

        var hasChoices = node.choices != null && node.choices.Count > 0;

        nameText.text = node.speakerName;
        dialogueText.text = node.text;

         // Tampilkan gambar jika ada
        if (node.image != null)
        {
            //dialogImage.sprite = node.image.sprite;  // Atur gambar ke Image UI
            dialogImage.gameObject.SetActive(true);  // Tampilkan gambar
        }
        else
        {
            dialogImage.gameObject.SetActive(false); // Sembunyikan gambar jika tidak ada
        }

        // Handle UI visibility based on choices
        progressButton.gameObject.SetActive(!hasChoices);

        // Build choices if available
        BuildChoices(node);
    }

    private void BuildChoices(Dialog.DialogNode node)
    {
        foreach (Transform t in choicePanel.transform) Destroy(t.gameObject);

        if (node.choices == null || node.choices.Count == 0)
        {
            choicePanel.SetActive(false);
            return;
        }

        choicePanel.SetActive(true);
        foreach (var ch in node.choices)
        {
            var btn = Instantiate(progressButton, choicePanel.transform);
            btn.GetComponentInChildren<TMP_Text>().text = ch.choiceText;
            btn.onClick.AddListener(() => {
                if (ch.nextNodeIndex >= 0 && ch.nextNodeIndex < currentDialog.nodes.Count)
                {
                    currentNodeIndex = ch.nextNodeIndex;
                    DisplayNode(currentNodeIndex);
                }
                else
                {
                    EndDialog();
                }
            });
        }
    }

    private void EndDialog()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);

        // Disable the dialog camera
        if (dialogCamera != null)
        {
            dialogCamera.gameObject.SetActive(false); // Matikan kamera dialog
        }

        // Enable movement & camera controls again
        if (mcController != null) mcController.enabled = true;
        if (starterAssetsInputs != null) starterAssetsInputs.SetInputActive(true);

        // Switch back to Player action map
        if (playerInput != null) playerInput.SwitchCurrentActionMap("Player");

        // Disable NextDialog action
        if (nextDialogAction != null)
        {
            nextDialogAction.action.performed -= OnNextDialogPerformed;
            nextDialogAction.action.Disable();
        }
    }
}

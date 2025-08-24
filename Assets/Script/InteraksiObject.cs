using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteraksiObject : MonoBehaviour
{
    public Button interactButton;  // Tombol Interaksi UI
    [SerializeField] private InputActionReference interactAction; // Referensi untuk Interact Action
    [SerializeField] private GameObject choicePanel;
    public DialogueManager dialogueManager;  // Referensi ke DialogueManager
    public Dialog dialogToStart;  // Dialog yang akan dimulai saat berinteraksi
    private bool isPlayerNearby = false;  // Flag untuk mengecek apakah pemain dekat objek

    private void Start()
    {
        interactButton.gameObject.SetActive(false);  // Sembunyikan tombol interaksi di awal
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            choicePanel.gameObject.SetActive(true);
            interactButton.gameObject.SetActive(true);  // Menampilkan tombol interaksi saat dekat objek
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            choicePanel.gameObject.SetActive(false);
            interactButton.gameObject.SetActive(false);  // Menyembunyikan tombol interaksi saat keluar dari area objek
        }
    }

    // Fungsi yang dipanggil saat tombol interaksi ditekan (misalnya "Enter")
    private void OnInteractPressed(InputAction inputAction)
    {
        Debug.Log("Interact pressed");
        dialogueManager.StartDialog(dialogToStart);
    }

    private void Update()
    {
        // Jika tombol interaksi ditekan dan pemain berada di dekat objek
        if (isPlayerNearby && Input.GetButtonDown("Interact"))  // Misalnya, 'E' atau 'F'
        {
            TriggerInteraction();
        }
    }

    public static void OnClick()
    {
        
    }

    // Fungsi untuk memulai interaksi dan dialog
    private void TriggerInteraction()
    {
        // Set dialog yang akan dimulai
        dialogueManager.StartDialog(dialogToStart);
    }
}

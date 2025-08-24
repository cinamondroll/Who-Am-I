using UnityEngine;

public class AutoDialogTrigger : MonoBehaviour
{
    public Dialog firstDialog; // Seret file ScriptableObject dialog pertama di sini
    public float startDelay = 0.5f;

    void Start()
    {
        Invoke("TriggerDialog", startDelay);
    }

    void TriggerDialog()
    {
        DialogueManager dialogManager = FindObjectOfType<DialogueManager>();
        
        if (dialogManager != null && firstDialog != null)
        {
            dialogManager.StartDialog(firstDialog);
        }
    }
}
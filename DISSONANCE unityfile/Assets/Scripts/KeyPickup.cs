using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyName = "LivingRoomKey";
    public KeyManager keyManager;
    public GameObject promptUI;
    public DialogueManager dialogueManager;
    public TaskManager taskManager;
    

    private bool playerInRange = false;
    public bool hasPickedUpKey = false;
    public GameObject key;
    

    void Start()
    {
        promptUI.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !hasPickedUpKey && !dialogueManager.IsDialogueActive())
        {
            PickUpKey();
            taskManager.UpdateTask("What is this key for?");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPickedUpKey)
        {
            playerInRange = true;
            promptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            promptUI.SetActive(false);
        }
    }

    void PickUpKey()
    {
        hasPickedUpKey = true;
        keyManager.AddKey(keyName);
        promptUI.SetActive(false);
        key.SetActive(false);
        Debug.Log($"{keyName} picked up!");

        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue(new[] { "[ YOU'VE PICKED UP A KEY. ]" });
        }
    }
}

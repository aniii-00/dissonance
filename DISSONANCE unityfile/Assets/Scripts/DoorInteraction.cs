using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    [Header("Key Setup")]
    [Tooltip("Exact name of the key required to open this door")]
    public string requiredKey;

    [Header("UI & Audio")]
    public GameObject promptUI; 
    public AudioSource doorLockedSound;
    public DialogueManager dialogueManager;

    [Header("Animation")]
    public Animator doorAnimator;

    private KeyManager keyManager;
    private bool playerInRange = false;
    private bool isOpen = false;
    public AudioSource doorOpenSound;

    void Start()
    {
        // Cache references
        keyManager = GameObject.FindWithTag("Player").GetComponentInChildren<KeyManager>();
        if (promptUI) promptUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[Door] Player entered range of '{gameObject.name}'");
            playerInRange = true;
            if (promptUI) promptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptUI) promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TryOpenDoor();
        }
    }

    void TryOpenDoor()
    {
        Debug.Log($"[Door] Attempting open '{gameObject.name}' with key '{requiredKey}'");
        if (keyManager == null)
        {
            Debug.LogError("[Door] KeyManager not found on Player!");
            return;
        }

        if (keyManager.HasKey(requiredKey))
        {
            OpenDoor();
        }
        else
        {
            LockedDoor();
        }
    }

    void OpenDoor()
    {
        if (isOpen) return;

        isOpen = true;

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }

        if (promptUI)
        {
            promptUI.SetActive(false);
        }

        if (doorOpenSound != null)
        {
            doorOpenSound.Play();
        }



        Debug.Log($"[Door] '{gameObject.name}' opened!");
    }

    void LockedDoor()
    {
        doorLockedSound.Play();
        
        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue(new[] { "The door is locked." });
        }

        Debug.Log($"[Door] '{gameObject.name}' is locked—no key '{requiredKey}' in inventory.");
    }
}

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
    
    [Header("Entry Dialogue")]
    public bool isFirstEntryDoor = false;
    [TextArea]
    public string[] entryDialogueLines;

    private bool entryDialogueTriggered = false;



    void Start()
    {

        keyManager = GameObject.FindWithTag("Player").GetComponentInChildren<KeyManager>();
        if (promptUI) promptUI.SetActive(false);
    }
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TryOpenDoor();
        }


        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (!isOpen && promptUI)
            {
                promptUI.SetActive(true);
            }

             
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (promptUI)
            {
                promptUI.SetActive(false); 
            }
        }
    }


    void TryOpenDoor()
    {
   
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

        if (isFirstEntryDoor && !entryDialogueTriggered && isOpen)
            {
                entryDialogueTriggered = true;

                if (dialogueManager != null && entryDialogueLines.Length > 0)
                {
                    dialogueManager.StartDialogue(entryDialogueLines);
                }
            }

    }

    void LockedDoor()
    {
        doorLockedSound.Play();
        
        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue(new[] { "The door is locked." });
        }


    }
}

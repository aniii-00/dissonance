using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public string requiredKey;
    public GameObject promptUI; 
    public DialogueManager dialogueManager;
    public AudioSource doorLocked;
    private bool inRange = false;
    private KeyManager keyManager;
    public Animator DoorAnimation;
    private bool isOpen = false;


    void Start()
    {
        keyManager = GameObject.FindWithTag("Player").GetComponent<KeyManager>();
        promptUI.SetActive(false);
    }

    void Update()
    {

        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (keyManager != null && keyManager.HasKey(requiredKey))
            {
                OpenDoor();
            }
            else
            {
                if (doorLocked != null)
                    doorLocked.Play();

                if (dialogueManager != null)
                    dialogueManager.StartDialogue(new string[] { "The door is locked." });
            }
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered");
            inRange = true;
            promptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            promptUI.SetActive(false);
        }
    }

    void OpenDoor()
    {
        if (!isOpen)
        {
            DoorAnimation.SetTrigger("Open");
            isOpen = true;
            promptUI.SetActive(false);
        }
    }
}

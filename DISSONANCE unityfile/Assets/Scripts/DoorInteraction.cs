using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public string requiredKey;
    public GameObject promptUI; 
    public DialogueManager dialogueManager;
    public AudioSource doorLocked;
    private bool inRange = false;
    private KeyManager keyManager;

    void Start()
    {
        keyManager = GameObject.FindWithTag("Player").GetComponent<KeyManager>();
        promptUI.SetActive(false);
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            doorLocked.Play();
            dialogueManager.StartDialogue(new string[] { "The door is locked." });
            

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
        // You'll implement this later
        Debug.Log("Door opened!");
    }
}

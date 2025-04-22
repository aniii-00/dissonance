using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyName = "LivingRoomKey";
    public KeyManager keyManager;
    public GameObject promptUI;
    public DialogueManager dialogueManager;

    private bool playerInRange = false;
    private bool hasPickedUpKey = false;
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
    }
}
